// Decompiled with JetBrains decompiler
// Type: PS3ManagerAPI.AttachDialog
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
  public class AttachDialog : Form
  {
    private IContainer components;
    private Label label1;
    private Button btnOK;
    private Button btnCancel;
    private Button btnRefresh;
    protected internal ComboBox comboBox1;

    public AttachDialog()
    {
      this.InitializeComponent();
    }

    public AttachDialog(PS3MAPI MyPS3MAPI)
      : this()
    {
      this.comboBox1.Items.Clear();
      foreach (uint pidProcess in MyPS3MAPI.Process.GetPidProcesses())
      {
        if (pidProcess != 0U)
          this.comboBox1.Items.Add((object) MyPS3MAPI.Process.GetName(pidProcess));
        else
          break;
      }
      this.comboBox1.SelectedIndex = 0;
    }

    private void BtnOK_Click(object sender, EventArgs e)
    {
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void BtnRefresh_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.comboBox1 = new ComboBox();
      this.btnRefresh = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(9, 21);
      this.label1.Name = "label1";
      this.label1.Size = new Size(64, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "PROCESS: ";
//      this.btnOK.DialogResult = DialogResult.OK;
      this.btnOK.Location = new Point(89, 58);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(108, 21);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "Attach selected";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.BtnOK_Click);
//      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(203, 58);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 21);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(89, 18);
      this.comboBox1.MaxDropDownItems = 16;
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(189, 21);
      this.comboBox1.TabIndex = 4;
//      this.btnRefresh.DialogResult = DialogResult.Retry;
      this.btnRefresh.Location = new Point(12, 58);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new Size(71, 21);
      this.btnRefresh.TabIndex = 5;
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new EventHandler(this.BtnRefresh_Click);
      this.AcceptButton = (IButtonControl) this.btnOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
//      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(292, 85);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnRefresh);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.label1);
//      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AttachDialog";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Attach process with PS3 Manager API";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
