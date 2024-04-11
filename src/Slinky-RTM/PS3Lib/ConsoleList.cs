// Decompiled with JetBrains decompiler
// Type: PS3Lib.ConsoleList
// Assembly: HEN RTM Tool by zFxbixn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C687F458-A7E2-4DD3-B516-C68C6A7F95BF
// Assembly location: C:\Users\Kelly\Desktop\Nouveau dossier (2)\AcuraRTM_HEN_Version old.exe


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PS3Lib
{
    public class ConsoleList
    {
        private PS3API Api;
        private List<CCAPI.ConsoleInfo> data;
    
        public ConsoleList(PS3API f)
        {
            this.Api = f;
            this.data = this.Api.CCAPI.GetConsoleList();
        }
    
        private ConsoleList.Lang getSysLanguage()
        {
            if (ConsoleList.SetLang.defaultLang != ConsoleList.Lang.Null)
                return ConsoleList.SetLang.defaultLang;
            return CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName.StartsWith("FRA") ? ConsoleList.Lang.French : ConsoleList.Lang.English;
        }
    
        private string strTraduction(string keyword)
        {
            if (this.getSysLanguage() == ConsoleList.Lang.French)
            {
                string str = keyword;
                if (str != null)
                {
                    switch (str)
                    {
                        case "btnConnect":
                            return "Connexion";
                        case "btnRefresh":
                            return "Rafraîchir";
                        case "errorSelect":
                            return "Vous devez d'abord sélectionner une console.";
                        case "errorSelectTitle":
                            return "Sélectionnez une console.";
                        case "formTitle":
                            return "Choisissez une console...";
                        case "noConsole":
                            return "Aucune console disponible, démarrez CCAPI Manager (v2.5) et ajoutez une nouvelle console.";
                        case "noConsoleTitle":
                            return "Aucune console disponible.";
                        case "selectGrid":
                            return "Sélectionnez une console dans la grille.";
                        case "selectedLbl":
                            return "Sélection :";
                    }
                }
            }
            else
            {
                string str = keyword;
                if (str != null)
                {
                    switch (str)
                    {
                        case "btnConnect":
                            return "Connection";
                        case "btnRefresh":
                            return "Refresh";
                        case "errorSelect":
                            return "You need to select a console first.";
                        case "errorSelectTitle":
                            return "Select a console.";
                        case "formTitle":
                            return "Select a console...";
                        case "noConsole":
                            return "None consoles available, run CCAPI Manager (v2.5) and add a new console.";
                        case "noConsoleTitle":
                            return "None console available.";
                        case "selectGrid":
                            return "Select a console within this grid.";
                        case "selectedLbl":
                            return "Selected :";
                    }
                }
            }
            return "?";
        }
    
        public bool Show()
        {
            bool Result = false;
            int tNum = -1;
            Label lblInfo = new Label();
            Button btnConnect = new Button();
            Button button = new Button();
            ListViewGroup listViewGroup = new ListViewGroup("Consoles", HorizontalAlignment.Left);
            ListView listView = new ListView();
            Form formList = new Form();
            btnConnect.Location = new Point(12, 254);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(198, 23);
            btnConnect.TabIndex = 1;
            btnConnect.Text = this.strTraduction("btnConnect");
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Enabled = false;
            btnConnect.Click += (EventHandler)((sender, e) =>
            {
                if (tNum > -1)
                {
                    if (this.Api.ConnectTarget(this.data[tNum].Ip))
                    {
                        this.Api.setTargetName(this.data[tNum].Name);
                        Result = true;
                    }
                    else
                        Result = false;
                    formList.Close();
                }
                else
                {
                    int num = (int)MessageBox.Show(this.strTraduction("errorSelect"), this.strTraduction("errorSelectTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            });
            button.Location = new Point(216, 254);
            button.Name = "btnRefresh";
            button.Size = new Size(86, 23);
            button.TabIndex = 1;
            button.Text = this.strTraduction("btnRefresh");
            button.UseVisualStyleBackColor = true;
            button.Click += (EventHandler)((sender, e) =>
            {
                tNum = -1;
                listView.Clear();
                lblInfo.Text = this.strTraduction("selectGrid");
                btnConnect.Enabled = false;
                this.data = this.Api.CCAPI.GetConsoleList();
                int num = this.data.Count<CCAPI.ConsoleInfo>();
                for (int index = 0; index < num; ++index)
                    listView.Items.Add(new ListViewItem(" " + this.data[index].Name + " - " + this.data[index].Ip)
                    {
                        ImageIndex = 0
                    });
            });
            listView.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            listViewGroup.Header = "Consoles";
            listViewGroup.Name = "consoleGroup";
            listView.Groups.AddRange(new ListViewGroup[1]
            {
        listViewGroup
            });
            listView.HideSelection = false;
            listView.Location = new Point(12, 12);
            listView.MultiSelect = false;
            listView.Name = "ConsoleList";
            listView.ShowGroups = false;
            listView.Size = new Size(290, 215);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.List;
            listView.ItemSelectionChanged += (ListViewItemSelectionChangedEventHandler)((sender, e) =>
            {
                tNum = e.ItemIndex;
                btnConnect.Enabled = true;
                string str1 = this.data[tNum].Name.Length <= 18 ? this.data[tNum].Name : this.data[tNum].Name.Substring(0, 17) + "...";
                string str2 = this.data[tNum].Ip.Length <= 16 ? this.data[tNum].Ip : this.data[tNum].Name.Substring(0, 16) + "...";
                lblInfo.Text = this.strTraduction("selectedLbl") + " " + str1 + " / " + str2;
            });
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(12, 234);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(158, 13);
            lblInfo.TabIndex = 3;
            lblInfo.Text = this.strTraduction("selectGrid");
            formList.MinimizeBox = false;
            formList.MaximizeBox = false;
            formList.ClientSize = new Size(314, 285);
            formList.AutoScaleDimensions = new SizeF(6f, 13f);
            formList.AutoScaleMode = AutoScaleMode.Font;
            formList.FormBorderStyle = FormBorderStyle.FixedSingle;
            formList.StartPosition = FormStartPosition.CenterScreen;
            formList.Text = this.strTraduction("formTitle");
            formList.Controls.Add((Control)listView);
            formList.Controls.Add((Control)lblInfo);
            formList.Controls.Add((Control)btnConnect);
            formList.Controls.Add((Control)button);
            listView.SmallImageList = new ImageList()
            {
    
            };
            int num1 = this.data.Count<CCAPI.ConsoleInfo>();
            for (int index = 0; index < num1; ++index)
                listView.Items.Add(new ListViewItem(" " + this.data[index].Name + " - " + this.data[index].Ip)
                {
                    ImageIndex = 0
                });
            if (num1 > 0)
            {
                int num2 = (int)formList.ShowDialog();
            }
            else
            {
                Result = false;
                formList.Close();
                int num3 = (int)MessageBox.Show(this.strTraduction("noConsole"), this.strTraduction("noConsoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return Result;
        }
    
        public enum Lang
        {
            Null,
            French,
            English,
        }
    
        public class SetLang
        {
            public static ConsoleList.Lang defaultLang = ConsoleList.Lang.Null;
        }
    }
}
