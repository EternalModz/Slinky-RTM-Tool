// Decompiled with JetBrains decompiler
// Type: PS3ManagerAPI.LogDialog
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PS3ManagerAPI
{
  public class LogDialog : Form
  {
    private PS3MAPI PS3MAPI;
    private IContainer components;
    protected internal TextBox tB_Log;
    private Button btnRefresh;
    private Button button1;

    public LogDialog()
    {
      this.InitializeComponent();
    }

    public LogDialog(PS3MAPI MyPS3MAPI)
      : this()
    {
      this.PS3MAPI = MyPS3MAPI;
    }

    private void LogDialog_Refresh(object sender, EventArgs e)
    {
      if (this.PS3MAPI == null)
        return;
      this.tB_Log.Text = this.PS3MAPI.Log;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LogDialog));
      this.tB_Log = new TextBox();
      this.btnRefresh = new Button();
      this.button1 = new Button();
      this.SuspendLayout();
      this.tB_Log.BackColor = Color.White;
      this.tB_Log.Location = new Point(12, 12);
      this.tB_Log.MaxLength = 16;
      this.tB_Log.Multiline = true;
      this.tB_Log.Name = "tB_Log";
      this.tB_Log.ReadOnly = true;
      this.tB_Log.ScrollBars = ScrollBars.Both;
      this.tB_Log.Size = new Size(429, 327);
      this.tB_Log.TabIndex = 10;
      this.btnRefresh.Location = new Point(290, 345);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new Size(71, 21);
      this.btnRefresh.TabIndex = 1;
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new EventHandler(this.LogDialog_Refresh);
      this.button1.Location = new Point(370, 345);
      this.button1.Name = "button1";
      this.button1.Size = new Size(71, 21);
      this.button1.TabIndex = 2;
      this.button1.Text = "Close";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
//      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(453, 378);
      this.ControlBox = false;
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.btnRefresh);
      this.Controls.Add((Control) this.tB_Log);
//      this.FormBorderStyle = FormBorderStyle.FixedDialog;
//      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "LogDialog";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "PS3 Manager API Log";
      this.Activated += new EventHandler(this.LogDialog_Refresh);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
