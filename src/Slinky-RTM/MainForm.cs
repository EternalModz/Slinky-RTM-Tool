using PS3Lib;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Slinky_RTM
{
    public partial class MainForm : AdaptiveForm
    {
        public static PS3API PS3 = new PS3API();
        public static PS3ManagerAPI.PS3MAPI PS3M_API = new PS3ManagerAPI.PS3MAPI();
        private bool IsConnected = false;

        #region Setting Things Up
        public MainForm()
        {
            InitializeComponent();
            // Register KeyDown event handler when the form is loaded
            this.KeyPreview = true; // This allows the form to receive key events before the control that has focus
            this.KeyDown += MainForm_KeyDown;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if CTRL + W is pressed
            if (e.Control && e.KeyCode == Keys.W)
            {
                // Close the application
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Colorize the logo to bright orange (255, 165, 0)
            ColorizeLogo(Color.FromArgb(255, 165, 0));
        }
        #endregion

        #region PS3 Connection
        private void ConnectBtn_Click_1(object sender, EventArgs e)
        {
            if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole)
            {
                try
                {
                    MainForm.PS3.ConnectTarget(this.IPtextBox.Text);
                    this.ConnectionStatusLbl.Text = "Connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Green;
                    IsConnected = true;
                }
                catch
                {
                    this.ConnectionStatusLbl.Text = "Not connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Red;
                }
            }
            else if (PS3.GetCurrentAPI() == SelectAPI.TargetManager)
            {
                try
                {
                    MainForm.PS3.ConnectTarget();
                    this.ConnectionStatusLbl.Text = "Connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Green;
                    IsConnected = true;
                }
                catch
                {
                    this.ConnectionStatusLbl.Text = "Not connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Red;
                }
            }
            else if (PS3.GetCurrentAPI() == SelectAPI.PS3Manager)
            {
                try
                {
                    PS3M_API.ConnectTarget(this.IPtextBox.Text, Convert.ToInt32(7887));
                    if (PS3M_API.IsConnected)
                        foreach (uint pidProcess in MainForm.PS3M_API.Process.GetPidProcesses())
                        {
                            if (pidProcess != 0U)
                            {
                                PS3M_API.Process.GetName(pidProcess);
                            }
                        }
                    this.ConnectionStatusLbl.Text = "Connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Green;
                    IsConnected = true;
                }
                catch
                {
                    this.ConnectionStatusLbl.Text = "Not connected";
                    this.ConnectionStatusLbl.ForeColor = Color.Red;
                }
            }
        }

        private void AttachBtn_Click_1(object sender, EventArgs e)
        {
            if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole || PS3.GetCurrentAPI() == SelectAPI.TargetManager)
            {
                try
                {
                    PS3.AttachProcess();
                    this.ConnectionStatusLbl.Text = "Connected + Attached";
                    this.ConnectionStatusLbl.ForeColor = Color.Green;
                }
                catch
                {
                    this.ConnectionStatusLbl.Text = "Not attached";
                    this.ConnectionStatusLbl.ForeColor = Color.Red;
                }
            }
            else if (PS3.GetCurrentAPI() == SelectAPI.PS3Manager)
            {
                try
                {
                    MainForm.PS3M_API.AttachProcess(MainForm.PS3M_API.Process.Processes_Pid[0]);
                    if (MainForm.PS3M_API.IsAttached)
                    {
                        //mods
                        this.ConnectionStatusLbl.Text = "Connected + Attached";
                        this.ConnectionStatusLbl.ForeColor = Color.Green;
                    }
                }
                catch
                {
                    this.ConnectionStatusLbl.Text = "Not attached";
                    this.ConnectionStatusLbl.ForeColor = Color.Red;
                }
            }
        }

        private void APISelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = APISelectorComboBox.SelectedItem.ToString();

            // Do something based on the selected option
            switch (selectedOption)
            {
                case "Target Manager API":
                    // Handles TMAPI
                    PS3.ChangeAPI(SelectAPI.TargetManager);
                    MessageBox.Show("Target Manager API for DEX PS3 Systems has been selected!");
                    break;
                case "Control Console API":
                    // Handles CCAPI
                    PS3.ChangeAPI(SelectAPI.ControlConsole);
                    MessageBox.Show("Control Console API for CFW PS3 Systems has been selected!");
                    break;
                case "PS3 Manager API":
                    // Handles PS3 Manager API
                    PS3.ChangeAPI(SelectAPI.PS3Manager);
                    MessageBox.Show("PS3 Manager API for HEN PS3 Systems has been selected!");
                    break;
                default:
                    // Handle any other case (if needed)
                    break;
            }
        }

        #endregion

        #region Ready Checkbox
        // The functionality for the ready checkbox. It enalbes / disables the tabs.
        private void ReadyCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ReadyCBox.Checked == true)
            {
                ModTabControl.Enabled = true;
            }
            else
                ModTabControl.Enabled = false;
        }
        #endregion

        #region Logo
        private void ColorizeLogo(Color color)
        {
            // Get the original image from the PictureBox
            Image originalImage = Properties.Resources.SlinkyLogo;

            // Create a new image with the same size
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Create a Graphics object from the new image
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // Set the color map
                ColorMap[] colorMap = {
            new ColorMap
            {
                OldColor = Color.Black,
                NewColor = color
            }
        };

                // Create an ImageAttributes object and set the color map
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetRemapTable(colorMap);

                // Draw the original image onto the new image using the color map
                g.DrawImage(originalImage,
                    new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                    0, 0, originalImage.Width, originalImage.Height,
                    GraphicsUnit.Pixel, imageAttributes);
            }

            // Set the PictureBox's Image to the new colorized image
            SlinkyLogoPictureBox.Image = newImage;
        }

        #endregion

        #region Mods
        private void FlyCBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is not CheckBox checkBox)
                    throw new Exception("sender was not a checkbox");

                bool toggleState = checkBox.Checked;

                CanFly(toggleState, false);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Credit to MayhemModding for finding this and also credit to LordVirus/Trent for helping with setting this portion of the code up.
        private void CanFly
        (
            bool enable, // This might be to just allow this user to double tab jump to toggle fly?
            bool fly // This might be to immediatly turn flying on?
        )
        {
            if (!IsConnected)
                return;

            uint theMc = PS3.Extension.ReadUInt32(0x011CCBC8); // User array index 0?
            uint localUser = PS3.Extension.ReadUInt32(theMc + 0x30);  //0x30/0x40/0xBC Increment user indexs?
            uint flyOffsetForLocalUserStruct = localUser + 0x409; // Fly struct index

            PS3.Extension.WriteBool(flyOffsetForLocalUserStruct + 1, enable); // This user has fly privlages?
            PS3.Extension.WriteBool(flyOffsetForLocalUserStruct, fly); // Enable fly for this user.
        }

        private void SkyBrightnessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = SkyBrightnessComboBox.SelectedItem.ToString();

            uint offset = 0x0007EDA8U;
            byte[] moddedBytes = new byte[8]
            {
                0x3F, 0x00, 0x00, 0x00, 0x3D, 0x4C, 0xCC, 0xCD
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void StartFlyCBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is not CheckBox checkBox)
                    throw new Exception("sender was not a checkbox");

                bool toggleState = checkBox.Checked;

                CanFly(toggleState, toggleState);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FOVComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = FOVComboBox.SelectedItem.ToString();

            uint offset = 0x008A1164U;
            byte[] moddedBytes = new byte[8]
            {
                0x3F, 0x00, 0x00, 0x00, 0x3F, 0xC9, 0x0F, 0xDB
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void ZoomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = ZoomComboBox.SelectedItem.ToString();

            uint offset = 0x004FCB98U;
            byte[] moddedBytes = new byte[8]
            {
                0x3F, 0x80, 0x00, 0x00, 0x3F, 0x8C, 0xCC, 0xCD
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void ZoomV2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedOption = ZoomV2ComboBox.SelectedItem.ToString();

            uint offset = 0x011DA6C8U;
            byte[] moddedBytes = new byte[8]
            {
                0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void SuperSpeedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedOption = SuperSpeedComboBox.SelectedItem.ToString();

            uint offset = 0x002D1990U;
            byte[] moddedBytes = new byte[4]
            {
                0x3F, 0x80, 0x00, 0x00
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void InfiniteResourcesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedOption = InfiniteResourcesComboBox.SelectedItem.ToString();

            uint offset = 0x0048AA94U;
            byte[] moddedBytes = new byte[4]
            {
                0x30, 0x84, 0x00, 0x00
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[3] += 0x10;
                    break;
                case "X2":
                    moddedBytes[3] += 0x20;
                    break;
                case "X3":
                    moddedBytes[3] += 0x30;
                    break;
                case "X4":
                    moddedBytes[3] += 0x40;
                    break;
                case "X5":
                    moddedBytes[3] += 0x50;
                    break;
                case "X6":
                    moddedBytes[3] += 0x60;
                    break;
                case "X7":
                    moddedBytes[3] += 0x70;
                    break;
                case "X8":
                    moddedBytes[3] += 0x80;
                    break;
                case "X9":
                    moddedBytes[3] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[3] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[3] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[3] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[3] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[3] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[3] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void TimeOfDayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = TimeOfDayComboBox.SelectedItem.ToString();

            uint offset = 0x011C8828U;
            byte[] moddedBytes = new byte[8]
            {
                0x3F, 0x80, 0x00, 0x00, 0x01, 0x1E, 0x26, 0x60
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void ZoomOutwardCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZoomOutwardCBox.Checked)
                PS3.Extension.WriteBytes(5229464U, new byte[4]
                {
                    (byte) 79,
                    byte.MaxValue,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(5229464U, new byte[4]
                {
                    (byte) 63,
                    (byte) 128,
                    (byte) 0,
                    (byte) 0
                });
        }

        private void PickUp64CBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PickUp64CBox.Checked)
                PS3.Extension.WriteBytes(2416688U, new byte[4]
                {
                    (byte) 56,
                    (byte) 128,
                    (byte) 0,
                    (byte) 64
                });
            else
                PS3.Extension.WriteBytes(2416688U, new byte[4]
                {
                    (byte) 124,
                    (byte) 132,
                    (byte) 224,
                    (byte) 20
                });
        }

        private void SuperSpeedCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SuperSpeedCBox.Checked)
                PS3.Extension.WriteBytes(2955664U, new byte[4]
                {
                    (byte) 63,
                    (byte) 0,
                    (byte) 245,
                    (byte) 195
                });
            else
                PS3.Extension.WriteBytes(2955664U, new byte[4]
                {
                    (byte) 63,
                    (byte) 104,
                    (byte) 245,
                    (byte) 195
                });
        }

        private void AlwaysSprintingCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AlwaysSprintingCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x004FCBA8, new byte[8]
                {
                    0x3F, 0x00, 0x00, 0x00, 0x3D, 0x4C, 0xCC, 0xCD
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x004FCBA8, new byte[8]
                {
                    0x3F, 0x80, 0x00, 0x00, 0x3D, 0x4C, 0xCC, 0xCD
                });
            }
        }

        private void InfiniteArrowsCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InfiniteArrowsCBox.Checked)
                PS3.Extension.WriteBytes(2417724U, new byte[4]
                {
                    (byte) 48,
                    (byte) 165,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(2417724U, new byte[4]
                {
                    (byte) 48,
                    (byte) 165,
                    byte.MaxValue,
                    byte.MaxValue
                });
        }

        private void InfiniteBlocksCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InfiniteBlocksCBox.Checked)
                PS3.Extension.WriteBytes(4762260U, new byte[4]
                {
                    (byte) 48,
                    (byte) 132,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(4762260U, new byte[4]
                {
                    (byte) 48,
                    (byte) 132,
                     byte.MaxValue,
                    byte.MaxValue
                });
        }

        private void InfiniteHungerCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InfiniteHungerCBox.Checked)
                PS3.Extension.WriteBytes(2122704U, new byte[4]
                {
                    (byte) 56,
                    (byte) 96,
                    (byte) 15,
                    byte.MaxValue
                });
            else
                PS3.Extension.WriteBytes(2122704U, new byte[4]
                {
                    (byte) 124,
                    (byte) 99,
                    (byte) 32,
                    (byte) 56
                });
        }

        private void ZoomInwardCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZoomInwardCBox.Checked)
                PS3.Extension.WriteBytes(5229464U, new byte[4]
                {
                    (byte) 47,
                    (byte) 0,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(5229464U, new byte[4]
                {
                    (byte) 63,
                    (byte) 128,
                    (byte) 0,
                    (byte) 0
                });
        }

        private void BrightAtNightCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BrightAtNightCBox.Checked)
                PS3.Extension.WriteBytes(2732252U, new byte[4]
                {
                    (byte) 127,
                    (byte) 128,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(2732252U, new byte[4]
                {
                    (byte) 63,
                    (byte) 128,
                    (byte) 0,
                    (byte) 0
                });
        }

        private void ClearScreenCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ClearScreenCBox.Checked)
                PS3.Extension.WriteBytes(2245432U, new byte[4]
                {
                    (byte) 48,
                    (byte) 0,
                    (byte) 0,
                    (byte) 0
                });
            else
                PS3.Extension.WriteBytes(2245432U, new byte[4]
                {
                    (byte) 63,
                    (byte) 128,
                    (byte) 0,
                    (byte) 0
                });
        }

        private void MakeItDarkCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MakeItDarkCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0015FE8C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0015FE8C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void LargeGUICBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.LargeGUICBox.Checked)
            {
                PS3.Extension.WriteBytes(0x00224334, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x00224334, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void UndergroundDamageCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.UndergroundDamageCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x001B000C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x001B000C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void BigHitCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BigHitCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x002CD238, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x002CD238, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void PVPFOVCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PVPFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x004FCBA8, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x004FCBA8, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void HighBrightnessCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.HighBrightnessCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0089E6C0, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0089E6C0, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void ExpertPVPFOVCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ExpertPVPFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A1164, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A1164, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void WeirdZoomCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.WeirdZoomCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A2A20, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A2A20, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void FasterMiningCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PVPFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x00733C38, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x00733C38, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
        }

        private void BigHitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = BigHitComboBox.SelectedItem.ToString();

            uint offset = 0x002CD238;
            byte[] moddedBytes = new byte[4]
            {
                0x3F, 0x80, 0x00, 0x00
            };

            // Adjust the second byte based on the selected option
            switch (selectedOption)
            {
                case "Default":
                    moddedBytes[1] += 0x80;
                    moddedBytes[1] += 0x00;
                    break;
                case "X1":
                    moddedBytes[1] += 0x10;
                    break;
                case "X2":
                    moddedBytes[1] += 0x20;
                    break;
                case "X3":
                    moddedBytes[1] += 0x30;
                    break;
                case "X4":
                    moddedBytes[1] += 0x40;
                    break;
                case "X5":
                    moddedBytes[1] += 0x50;
                    break;
                case "X6":
                    moddedBytes[1] += 0x60;
                    break;
                case "X7":
                    moddedBytes[1] += 0x70;
                    break;
                case "X8":
                    moddedBytes[1] += 0x80;
                    break;
                case "X9":
                    moddedBytes[1] += 0x90;
                    break;
                case "XAA":
                    moddedBytes[1] += 0xAA;
                    break;
                case "XBB":
                    moddedBytes[1] += 0xBB;
                    break;
                case "XCC":
                    moddedBytes[1] += 0xCC;
                    break;
                case "XDD":
                    moddedBytes[1] += 0xDD;
                    break;
                case "XEE":
                    moddedBytes[1] += 0xEE;
                    break;
                case "XFF":
                    moddedBytes[1] += 0xFF;
                    break;
                default:
                    // Handle any other cases
                    break;
            }

            // Finally, write the modified bytes to the specified offset
            PS3.Extension.WriteBytes(offset, moddedBytes);
        }

        private void BecomeShortCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BecomeShortCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0007F194, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0007F194, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void LessFogCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.LessFogCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0007FD48, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0007FD48, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void NoGUICBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NoGUICBox.Checked)
            {
                PS3.Extension.WriteBytes(0x00224338, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x00224338, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void EarlyNightCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EarlyNightCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0029B0D4, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0029B0D4, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void DarkCloudsCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DarkCloudsCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0029B724, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0029B724, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void BetterSkyCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PVPFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0033305C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0033305C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void DarkLightingCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DarkLightingCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x006BCD3C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x006BCD3C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void SuperXRayZoomCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SuperXRayZoomCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A0B48, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A0B48, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void PowerHitCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PowerHitCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x002CD238, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x002CD238, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void SonicFOVCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SonicFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A1164, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A1164, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
        }

        private void DisableMovementCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DisableMovementCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x013C7604, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x013C7604, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
        }

        private void MineForXRayCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MineForXRayCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0121E9E0, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0121E9E0, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void FastHitCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FastHitCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x011C8828, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x011C8828, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void BlueVisionCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BlueVisionCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A1878, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A1878, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void StrangeRenderingCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.StrangeRenderingCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A0D3C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A0D3C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void CrazyXRayModeCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CrazyXRayModeCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A0C94, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A0C94, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void ZoomedXRayCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ZoomedXRayCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A0B48, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A0B48, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void CrazyViewCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CrazyViewCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A0B34, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A0B34, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void SunGlassesCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SunGlassesCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0089F094, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0089F094, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void BrightLightCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BrightLightCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x0089E6C4, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x0089E6C4, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void PancakeFOVCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PancakeFOVCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x006F70F4, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x006F70F4, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void MoveCloudsCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MoveCloudsCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x006F32A0, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x006F32A0, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void SpecialNightTimeCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SpecialNightTimeCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x011C8828, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x011C8828, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
        }

        private void GoInvisibleCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GoInvisibleCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A26D0, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A26D0, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
        }

        private void PartialXRayCBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PartialXRayCBox.Checked)
            {
                PS3.Extension.WriteBytes(0x008A116C, new byte[4]
                {
                    0x3F, 0x00, 0x00, 0x00
                });
            }
            else
            {
                PS3.Extension.WriteBytes(0x008A116C, new byte[4]
                {
                    0x3F, 0x80, 0x00, 0x00
                });
            }
        }
        #endregion

        #region Messages
        private void AboutBtn_Click(object sender, EventArgs e)
        {
            string message = ("Slinky RTM Tool\n\nThe Slinky RTM Tool is a powerful real-time modding / real-time editing tool made for Minecraft PS3 Edition versions 1.20 - 1.28. The tool is open-source and can be found on GitHub.\n\nTool version: 1.1.0 (Build #11000B)\n\nCredits:\nDeveloper: EternalModz\nHelper: MayhemModding\nHelper: LordVirus (Trent)\nOffset Credits: SirJakey\nOffset Credits: Eddie Mac / NeverSwitchUp\n\nTool created for: Seve");
            MessageBox.Show(message, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HowToUseCBoxsBtn_Click(object sender, EventArgs e)
        {
            // Print out a message to tell the user how the mods work in the comboboxes
            string message = "Mod Amplifier\n\n" +
                             "X1   =  1\n" +
                             "X2   =  2\n" +
                             "X3   =  3\n" +
                             "X4   =  4\n" +
                             "X5   =  5\n" +
                             "X6   =  6\n" +
                             "X7   =  7\n" +
                             "X8   =  8\n" +
                             "X9   =  9\n" +
                             "XAA  = 170\n" +
                             "XBB  = 187\n" +
                             "XCC  = 204\n" +
                             "XDD  = 221\n" +
                             "XEE  = 238\n" +
                             "XFF  = 255" +
                             "\n\nHigher values make the mods more powerful.";

            MessageBox.Show(message, "Mod Amplifier Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Links
        private void YouTubeLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify the URL you want to open
            string url = "https://youtube.com/@EternalModzLive";

            // Open the URL in the default browser
            Process.Start(url);

            // Mark the link as visited
            YouTubeLinkLbl.LinkVisited = true;
        }

        private void SocialsLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify the URL you want to open
            string url = "https://bio.link/EternalModz";

            // Open the URL in the default browser
            Process.Start(url);

            // Mark the link as visited
            SocialsLinkLbl.LinkVisited = true;
        }
        #endregion
    }
}