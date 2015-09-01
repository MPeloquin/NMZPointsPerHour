using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NmzExpHour.ImageProcessing;

namespace NmzExpHour.UI
{
    [System.ComponentModel.DesignerCategory(@"Code")]
    public class LabelDropShadow : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            StringFormat style = GenerateStyle();

            Rectangle shadowRect = ClientRectangle;
            shadowRect.X += 1;
            shadowRect.Y += 1;

            e.Graphics.DrawString(Text, Font, new SolidBrush(Color.Black), shadowRect, style);

            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle, style);

            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(125, 70, 15)), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }

        private StringFormat GenerateStyle()
        {
            StringFormat style = new StringFormat();;
            style.Alignment = StringAlignment.Near;

            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                    style.Alignment = StringAlignment.Near;
                    style.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    style.Alignment = StringAlignment.Center;
                    style.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    style.Alignment = StringAlignment.Far;
                    style.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    style.Alignment = StringAlignment.Near;
                    style.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    style.Alignment = StringAlignment.Far;
                    style.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    style.Alignment = StringAlignment.Center;
                    style.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    style.Alignment = StringAlignment.Near;
                    style.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    style.Alignment = StringAlignment.Center;
                    style.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    style.Alignment = StringAlignment.Far;
                    style.LineAlignment = StringAlignment.Far;
                    break;
            }
            return style;
        }
    }
}