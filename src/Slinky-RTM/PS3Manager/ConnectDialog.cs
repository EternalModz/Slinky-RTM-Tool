// Decompiled with JetBrains decompiler
// Type: PS3ManagerAPI.ConnectDialog
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
  public class ConnectDialog : Form
  {
    private IContainer components;
    private Label label1;
    private Button btnOK;
    private Button btnCancel;
    protected internal TextBox txtIp;
    protected internal TextBox txtPort;
    private Label label2;

    public ConnectDialog()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConnectDialog));
      this.label1 = new Label();
      this.txtIp = new TextBox();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.txtPort = new TextBox();
      this.label2 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(16, 26);
      this.label1.Name = "label1";
      this.label1.Size = new Size(23, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "IP: ";
      this.txtIp.Location = new Point(45, 23);
      this.txtIp.MaxLength = 16;
      this.txtIp.Name = "txtIp";
      this.txtIp.Size = new Size(116, 20);
      this.txtIp.TabIndex = 1;
      this.txtIp.Text = "127.0.0.1";
      this.txtIp.TextAlign = HorizontalAlignment.Center;
//      this.btnOK.DialogResult = DialogResult.OK;
      this.btnOK.Location = new Point(118, 58);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 21);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "Connect";
      this.btnOK.UseVisualStyleBackColor = true;
//      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(203, 58);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 21);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.txtPort.Enabled = false;
      this.txtPort.Location = new Point(223, 23);
      this.txtPort.MaxLength = 5;
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new Size(55, 20);
      this.txtPort.TabIndex = 5;
      this.txtPort.Text = "7887";
      this.txtPort.TextAlign = HorizontalAlignment.Center;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(174, 26);
      this.label2.Name = "label2";
      this.label2.Size = new Size(43, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "PORT: ";
      this.AcceptButton = (IButtonControl) this.btnOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
//      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(292, 85);
      this.ControlBox = false;
      this.Controls.Add((Control) this.txtPort);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.txtIp);
      this.Controls.Add((Control) this.label1);
//      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      //this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConnectDialog";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Connection with PS3 Manager API";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
