using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    class ProgressBarText : ProgressBar
    {
        public string DisplayText = "";

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x000F)
            {
                var flags = TextFormatFlags.VerticalCenter |
                            TextFormatFlags.HorizontalCenter |
                            TextFormatFlags.SingleLine |
                            TextFormatFlags.WordEllipsis;

                TextRenderer.DrawText(CreateGraphics(),
                                      DisplayText,
                                      Font,
                                      new Rectangle(0, 0, Width, Height),
                                      Color.Black,
                                      flags);
            }
        }
    }
}
