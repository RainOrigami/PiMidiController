using Gdk;
using GLib;
using Gtk;

public class TurnKnob : Gtk.DrawingArea
{
    private const int LineLength = 10;
    private const int LineThickness = 3;
    private const int ValueTextSize = 20;
    private const int AutoSoftStopPercentageRange = 10;
    private const int pixelsPerValue = 2;
    private const int pixelPerValueAutoSoft = 15;
    private const int pixelsPerValueSoft = 50;
    private const double angle_min = -135;
    private const double angle_max = 270;
    private const double angle_overdrive = 90;

    private int knobSize;
    private int knobCenterX;
    private int knobCenterY;
    private double angle;
    private readonly string label;
    private int minValue;
    private int maxValue;
    private int overdriveValue;
    private int value;
    private bool isDragging;
    private int dragStartY;
    private int dragStartValue;
    private int[] softStops;
    private readonly bool centered;

    public int Value
    {
        get { return value; }
        set { SetValue(value); }
    }

    public event EventHandler? ValueChanged;

    public TurnKnob(string label, int minValue, int maxValue, int overdriveValue, int[] softStops, bool centered)
    {
        this.StyleContext.AddClass("turn-knob");
        this.label = label;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.overdriveValue = overdriveValue;
        this.softStops = softStops;
        this.centered = centered;
        value = minValue;
        angle = GetAngleFromValue(value);
        SetSizeRequest(AllocatedWidth, AllocatedHeight);
        AddEvents((int)Gdk.EventMask.ButtonPressMask | (int)Gdk.EventMask.ButtonReleaseMask | (int)Gdk.EventMask.PointerMotionMask);
    }

    protected override bool OnButtonPressEvent(Gdk.EventButton ev)
    {
        if (ev.Button == 1 && !isDragging)
        {
            isDragging = true;
            dragStartY = (int)ev.Y;
            dragStartValue = value;
        }

        return true;
    }

    protected override bool OnButtonReleaseEvent(Gdk.EventButton ev)
    {
        if (ev.Button == 1 && isDragging)
        {
            isDragging = false;
            return true;
        }

        return false;
    }

    protected override bool OnMotionNotifyEvent(Gdk.EventMotion ev)
    {
        if (ev.State.HasFlag(Gdk.ModifierType.Button1Mask) && isDragging)
        {
            int diffY = (int)ev.Y - dragStartY;
            int newValue = dragStartValue;

            while (Math.Abs(diffY) > 0)
            {
                int currentValuePixels = pixelsPerValue;
                if (softStops.Contains(newValue))
                {
                    currentValuePixels = pixelsPerValueSoft;
                }
                else if (newValue % AutoSoftStopPercentageRange == 0)
                {
                    currentValuePixels = pixelPerValueAutoSoft;
                }

                if (Math.Abs(diffY) < currentValuePixels)
                {
                    break;
                }

                newValue += -1 * Math.Sign(diffY);
                diffY -= currentValuePixels * Math.Sign(diffY);
                dragStartY = (int)ev.Y - diffY;
            }

            dragStartValue = newValue;

            SetValue(Math.Max(minValue, Math.Min(overdriveValue == 0 ? maxValue : overdriveValue, newValue)));
        }

        return true;
    }

    private double GetAngleFromValue(int value)
    {
        if (value > maxValue)
        {
            double percentage = (value - maxValue) / (double)(overdriveValue - maxValue);
            double angle = DegToRad(angle_max) - DegToRad(angle_min) + (percentage * DegToRad(45));
            return angle;
        }
        else
        {
            double percentage = (value - minValue) / (double)(maxValue - minValue);
            double angle = Math.Floor(percentage * 100) / 100 * DegToRad(angle_max) - DegToRad(angle_min);
            return angle;
        }

    }

    private static double DegToRad(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    private void SetValue(int value)
    {
        if (this.value != value)
        {
            this.value = value;
            this.angle = this.GetAngleFromValue(value);
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        QueueDraw();
    }

    protected override bool OnDrawn(Cairo.Context cr)
    {
        cr.Save();

        StyleContext.RenderBackground(cr, 0, 0, Allocation.Width, Allocation.Height);

        RGBA foregroundColor = StyleContext.GetColor(StateFlags.Normal);
        cr.SetSourceRGB(foregroundColor.Red, foregroundColor.Green, foregroundColor.Blue);

        cr.LineWidth = LineThickness;
        cr.Arc(knobCenterX, knobCenterY, knobSize / 2, 0, 2 * Math.PI);
        cr.Stroke();

        if (!this.centered)
        {
            cr.SetSourceRGB(1, 0, 0);
            cr.LineWidth = LineThickness * 2;
            cr.Arc(knobCenterX, knobCenterY, knobSize / 2, DegToRad(45), DegToRad(90));
            cr.Stroke();
        }

        if (this.value <= maxValue || this.centered)
        {
            cr.SetSourceRGB(foregroundColor.Red, foregroundColor.Green, foregroundColor.Blue);
        }

        // Draw value line
        cr.LineWidth = LineThickness;

        cr.MoveTo(knobCenterX, knobCenterY);
        cr.LineTo(knobCenterX + (knobSize / 2 - LineLength) * Math.Cos(angle),
                  knobCenterY + (knobSize / 2 - LineLength) * Math.Sin(angle));
        cr.Stroke();

        cr.SetSourceRGB(foregroundColor.Red, foregroundColor.Green, foregroundColor.Blue);

        // Draw name text
        cr.SetFontSize(ValueTextSize * 1.35);
        var extends = cr.TextExtents(this.label);
        cr.MoveTo(knobCenterX - extends.Width / 2, knobSize / 4 * 2);
        cr.ShowText(this.label);

        // Draw value text
        string text = Math.Floor((((double)value / maxValue * 100.0) - (this.centered ? 50 : 0)) * (this.centered ? 2 : 1)).ToString() + "%";
        cr.SetFontSize(ValueTextSize);
        var extents = cr.TextExtents(text);
        cr.MoveTo(knobCenterX - extents.Width / 2, knobSize / 4 * 3);
        cr.ShowText(text);

        // Draw lines
        cr.LineWidth = 1.0;

        double angleStep = 1.5 * Math.PI / 10;

        for (int i = 0; i <= 10; i++)
        {
            double angle = i * angleStep + Math.PI * 0.75;
            double x1 = knobCenterX + (knobSize / 2 - LineLength) * Math.Cos(angle);
            double y1 = knobCenterY + (knobSize / 2 - LineLength) * Math.Sin(angle);
            double x2 = knobCenterX + knobSize / 2 * Math.Cos(angle);
            double y2 = knobCenterY + knobSize / 2 * Math.Sin(angle);
            cr.MoveTo(x1, y1);
            cr.LineTo(x2, y2);
            cr.Stroke();
        }

        if (overdriveValue > 0)
        {
            cr.SetSourceRGB(1, 0, 0);
            cr.MoveTo(knobCenterX + (knobSize / 2 - LineLength) * Math.Cos(DegToRad(angle_overdrive)), knobCenterY + (knobSize / 2 - LineLength) * Math.Sin(DegToRad(angle_overdrive)));
            cr.LineTo(knobCenterX + knobSize / 2 * Math.Cos(DegToRad(angle_overdrive)), knobCenterY + knobSize / 2 * Math.Sin(DegToRad(angle_overdrive)));
            cr.Stroke();
        }

        cr.Restore();

        return true;
    }

    protected override void OnSizeAllocated(Gdk.Rectangle allocation)
    {
        base.OnSizeAllocated(allocation);

        knobCenterX = allocation.Width / 2;
        knobCenterY = allocation.Height / 2;
        knobSize = Math.Min(allocation.Width - 5, allocation.Height - 5);
    }

}