using System;
using System.Drawing;
using System.Windows.Forms;
using Dark.Net;

namespace Slinky_RTM
{
    public class AdaptiveForm : Form
    {
        public AdaptiveForm()
        {
            // Apply dark mode theme to the form
            DarkNet.Instance.SetWindowThemeForms(this, Theme.Dark);

            // Customize other dark mode settings as needed
            // Example: Change background color, text color, etc.
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;

            // Set the form's start position to manual
            this.StartPosition = FormStartPosition.Manual;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Center the form on the screen only during runtime (not in design mode)
            if (!DesignMode)
            {
                // Calculate the center of the screen
                int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
                int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

                // Set the form's location to the center of the screen
                this.Location = new Point(centerX, centerY);
            }
        }
    }
}
