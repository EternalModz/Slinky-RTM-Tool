using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CBH.Controls
{
    public class CrEaTiiOn_BarTabControl : TabControl
    {
        public CrEaTiiOn_BarTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(44, 136);
            Alignment = TabAlignment.Left;

            // Set the background color of the selected tab
            if (SelectedTab != null)
            {
                SelectedTab.BackColor = Color.FromArgb(20, 20, 20);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Bitmap B = new Bitmap(Width, Height))
            using (Graphics G = Graphics.FromImage(B))
            {
                G.Clear(Color.FromArgb(20, 20, 20));
                G.FillRectangle(new SolidBrush(Color.FromArgb(15, 15, 15)), new Rectangle(0, 0, ItemSize.Height + 4, Height));
                G.DrawLine(new Pen(Color.FromArgb(10, 10, 10)), new Point(ItemSize.Height + 3, 0), new Point(ItemSize.Height + 3, Height));

                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle tabRect = GetTabRect(i);
                    Rectangle tabBounds = new Rectangle(tabRect.Left - 2, tabRect.Top - 2, tabRect.Width + 3, tabRect.Height - 1);
                    Color fillColor = (i == SelectedIndex) ? Color.Orange : Color.FromArgb(15, 15, 15);

                    using (SolidBrush brush = new SolidBrush(fillColor))
                    {
                        G.FillRectangle(brush, tabBounds);
                    }

                    if (ImageList != null && ImageList.Images.Count > 0 && ImageList.Images[i] != null)
                    {
                        G.DrawImage(ImageList.Images[i], tabRect.Left + 8, tabRect.Top + 6);
                    }

                    using (StringFormat format = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
                    {
                        G.DrawString(TabPages[i].Text, Font, (i == SelectedIndex) ? Brushes.Black : Brushes.DimGray, tabBounds, format);
                    }

                    if (i == SelectedIndex)
                    {
                        using (Pen pen = new Pen(Color.Orange))
                        {
                            G.DrawRectangle(pen, tabBounds);
                            G.DrawLine(pen, tabBounds.Left - 1, tabBounds.Top - 1, tabBounds.Left, tabBounds.Top);
                            G.DrawLine(pen, tabBounds.Left - 1, tabBounds.Bottom - 1, tabBounds.Left, tabBounds.Bottom);
                        }
                    }
                }

                e.Graphics.DrawImage(B, 0, 0);
            }
        }
    }
}