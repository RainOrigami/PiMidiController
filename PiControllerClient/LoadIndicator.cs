using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerClient;

public class LoadIndicator : Gtk.DrawingArea
{
    public readonly string Label;
    private readonly int minValue;
    private readonly int maxValue;
    public readonly string Unit;

    public LoadIndicator(string label, int minValue, int maxValue, string unit)
    {
        this.Label = label;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.Unit = unit;
    }

    private double value;

    public double Value
    {
        get { return value; }
        set { this.SetValue(value); }
    }

    public void SetValue(double value)
    {
        if (value < minValue)
        {
            value = minValue;
        }
        else if (value > maxValue)
        {
            value = maxValue;
        }

        if (this.value != value)
        {
            this.value = value;
            this.QueueDraw();
        }
    }

    protected override bool OnDrawn(Context cr)
    {
        int width = this.AllocatedWidth;
        int height = this.AllocatedHeight;

        cr.SelectFontFace("Arial", FontSlant.Normal, FontWeight.Normal);
        cr.SetFontSize(12);

        var (r, g) = this.ColorValue;
        cr.SetSourceRGB(r, g, 0.0);
        cr.Rectangle(0, 0, width, height);
        cr.Fill();

        cr.SetSourceRGB(0.0, 0.0, 0.0);
        cr.MoveTo(5, 15);
        cr.ShowText(Label);

        // center text
        var extends = cr.TextExtents($"{value} {Unit}");
        cr.MoveTo((width / 2) - (extends.Width / 2), (height / 2) + (extends.Height / 2));
        cr.ShowText($"{value} {Unit}");

        return true;
    }

    private (double r, double g) ColorValue
    {
        get
        {
            // Calculate the percentage
            double percentage = (value - minValue) / (double)(maxValue - minValue);

            // Interpolate between red and green
            double r = Math.Min(1.0, 2.0 * percentage);
            double g = Math.Min(1.0, 2.0 * (1.0 - percentage));

            return (r, g);
        }
    }
}
