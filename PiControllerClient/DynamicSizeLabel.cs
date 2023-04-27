using Cairo;
using Gdk;
using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace PiControllerClient
{
    public class DynamicSizeLabel : Label
    {
        public int MaximumFontSize { get; set; } = 24;

        public DynamicSizeLabel(string str) : base(str)
        {
            this.SizeAllocate(new Gdk.Rectangle(0, 0, 1, 1));
        }

        private int fontSize = 0;
        private int lastWidth = 0;
        private int lastHeight = 0;

        private void recalculateSize(Context cr)
        {
            if (this.AllocatedWidth == lastWidth && this.AllocatedHeight == lastHeight && this.fontSize > 0)
            {
                return;
            }

            this.lastWidth = this.AllocatedWidth;
            this.lastHeight = this.AllocatedHeight;

            int targetFontSize = this.MaximumFontSize;
            TextExtents extents;
            do
            {
                targetFontSize /= 2;
                cr.SetFontSize(targetFontSize);
                extents = cr.TextExtents(this.Text);
            } while ((extents.Width > this.AllocatedWidth || extents.Height > this.AllocatedHeight) && targetFontSize > 5);

            if (targetFontSize < 5)
            {
                this.fontSize = 6;
            }
            else
            {

                while (extents.Width < this.AllocatedWidth && extents.Height < this.AllocatedHeight && targetFontSize < this.MaximumFontSize)
                {
                    targetFontSize++;
                    cr.SetFontSize(targetFontSize);
                    extents = cr.TextExtents(this.Text);
                }

                this.fontSize = targetFontSize - 1;
            }
        }

        protected override bool OnDrawn(Context cr)
        {
            recalculateSize(cr);


            cr.Save();

            RGBA foregroundColor = StyleContext.GetColor(StateFlags.Normal);
            cr.SetSourceRGB(foregroundColor.Red, foregroundColor.Green, foregroundColor.Blue);

            cr.Antialias = Antialias.Subpixel;

            cr.SetFontSize(this.fontSize);

            TextExtents extents = cr.TextExtents(this.Text);

            cr.MoveTo(Math.Max(this.AllocatedWidth / 2 - extents.Width / 2, 0), Math.Min(this.AllocatedHeight / 2 + extents.Height / 2, this.AllocatedHeight));
            cr.ShowText(this.Text);

            cr.Restore();
            return true;
        }
    }
}
