using System;
using System.Drawing;
using System.Windows.Forms;

namespace CBH.Controls
{
    public class CustomTabPage : TabControl
    {
        private Color _squareColor = Color.FromArgb(250, 36, 38);
        private bool _showOuterBorders = false;

        public CustomTabPage()
        {
            InitializeControlStyles();
            InitializeAppearance();
        }

        private void InitializeControlStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitializeAppearance()
        {
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(30, 115);
            Alignment = TabAlignment.Left;
        }

        public Color SquareColor
        {
            get => _squareColor;
            set
            {
                _squareColor = value;
                Invalidate();
            }
        }

        public bool ShowOuterBorders
        {
            get => _showOuterBorders;
            set
            {
                _showOuterBorders = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var bitmap = new Bitmap(Width, Height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.FromArgb(20, 20, 20));

                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle tabRect = GetTabRect(i);

                    if (i == SelectedIndex)
                    {
                        graphics.FillRectangle(new SolidBrush(_squareColor), tabRect.Left, tabRect.Top, 9, tabRect.Height);
                    }

                    if (ImageList != null && ImageList.Images.Count > i && ImageList.Images[i] != null)
                    {
                        graphics.DrawImage(ImageList.Images[i], tabRect.Left + 8, tabRect.Top + 6);
                    }

                    string tabText = TabPages[i].Text;
                    var textRectangle = new Rectangle(tabRect.Left + 20, tabRect.Top, tabRect.Width - 20, tabRect.Height);
                    if (i == SelectedIndex)
                    {
                        tabText = "      " + tabText;
                    }

                    graphics.DrawString(tabText, Font, Brushes.White, textRectangle, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    });

                    if (_showOuterBorders)
                    {
                        graphics.DrawRectangle(Pens.White, tabRect);
                    }
                }

                e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }
    }
}
