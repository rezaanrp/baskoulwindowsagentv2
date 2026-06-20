using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWinformsApp.Classes
{
    public class SevenSegmentRenderer
    {
        private readonly Dictionary<char, bool[]> digitMap;


        public Color SegmentOnColor { get; set; } = Color.Red;
        public Color SegmentOffColor { get; set; } = Color.FromArgb(40, 0, 0);


        public int DigitSpacing { get; set; } = 10;


        public SevenSegmentRenderer()
        {
            digitMap = new Dictionary<char, bool[]>
            {
                { '0', new[]{ true,true,true,true,true,true,false } },
                { '1', new[]{ false,true,true,false,false,false,false } },
                { '2', new[]{ true,true,false,true,true,false,true } },
                { '3', new[]{ true,true,true,true,false,false,true } },
                { '4', new[]{ false,true,true,false,false,true,true } },
                { '5', new[]{ true,false,true,true,false,true,true } },
                { '6', new[]{ true,false,true,true,true,true,true } },
                { '7', new[]{ true,true,true,false,false,false,false } },
                { '8', new[]{ true,true,true,true,true,true,true } },
                { '9', new[]{ true,true,true,true,false,true,true } }
            };
        }


        public Bitmap Render(string number, int width, int height)
        {
            number = number.PadLeft(6, '0');


            Bitmap bmp = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bmp);


            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Black);


            int totalSpacing = (number.Length - 1) * DigitSpacing;
            int digitWidth = (width - totalSpacing) / number.Length;
            int digitHeight = height;


            int firstNonZero = number.TakeWhile(c => c == '0').Count();
            if (firstNonZero == number.Length)
                firstNonZero = number.Length - 1;


            int x = 0;
            for (int i = 0; i < number.Length; i++)
            {
                char c = number[i];


                if (i < firstNonZero)
                {
                    DrawDigit(g, new bool[7], x, 0, digitWidth, digitHeight);
                }
                else
                {
                    if (digitMap.ContainsKey(c))
                        DrawDigit(g, digitMap[c], x, 0, digitWidth, digitHeight);
                }


                x += digitWidth + DigitSpacing;
            }


            return bmp;
        }


        private void DrawDigit(Graphics g, bool[] seg, int x, int y, int w, int h)
        {
            int thickness = w / 7;
            int length = w - 2 * thickness;


            Brush on = new SolidBrush(SegmentOnColor);
            Brush off = new SolidBrush(SegmentOffColor);


            g.FillEllipse(seg[0] ? on : off, x + thickness, y, length, thickness);
            g.FillEllipse(seg[1] ? on : off, x + thickness + length, y + thickness, thickness, (h / 2) - thickness);
            g.FillEllipse(seg[2] ? on : off, x + thickness + length, y + (h / 2), thickness, (h / 2) - thickness);
            g.FillEllipse(seg[3] ? on : off, x + thickness, y + h - thickness, length, thickness);
            g.FillEllipse(seg[4] ? on : off, x, y + (h / 2), thickness, (h / 2) - thickness);
            g.FillEllipse(seg[5] ? on : off, x, y + thickness, thickness, (h / 2) - thickness);
            g.FillEllipse(seg[6] ? on : off, x + thickness, (h / 2) - (thickness / 2), length, thickness);
        }
    }
}
