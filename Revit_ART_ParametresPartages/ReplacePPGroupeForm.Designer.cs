using System.Windows.Forms;

namespace Revit_ART_ParametresPartages
{
    partial class ReplacePPGroupeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTipFamilles = new System.Windows.Forms.ToolTip(this.components);
            this.labelFamilles = new System.Windows.Forms.Label();
            this.buttonAjoutFamilles = new System.Windows.Forms.Button();
            this.buttonSupprimer = new System.Windows.Forms.Button();
            this.buttonToutSupprimer = new System.Windows.Forms.Button();
            this.listBoxFamilles = new System.Windows.Forms.ListBox();
            this.groupFamilles = new System.Windows.Forms.GroupBox();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.radioButtonShared = new System.Windows.Forms.RadioButton();
            this.checkedListBoxPPGChoice = new System.Windows.Forms.CheckedListBox();
            this.groupPPGChoice = new System.Windows.Forms.GroupBox();
            this.comboBoxPPGReplaceChoice = new System.Windows.Forms.ComboBox();
            this.listBoxReplace = new System.Windows.Forms.ListBox();
            this.groupReplaceChoice = new System.Windows.Forms.GroupBox();
            this.buttonEraseChoice = new System.Windows.Forms.Button();
            this.buttonLog = new System.Windows.Forms.Button();
            this.buttonTransaction = new System.Windows.Forms.Button();
            this.logoArtelia = new System.Windows.Forms.PictureBox();
            this.linkConnect = new System.Windows.Forms.LinkLabel();
            this.groupFamilles.SuspendLayout();
            this.groupPPGChoice.SuspendLayout();
            this.groupReplaceChoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoArtelia)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFamilles
            // 
            this.labelFamilles.AutoSize = true;
            this.labelFamilles.Location = new System.Drawing.Point(17, 26);
            this.labelFamilles.Name = "labelFamilles";
            this.labelFamilles.Size = new System.Drawing.Size(0, 13);
            this.labelFamilles.TabIndex = 0;
            this.labelFamilles.Text = Application.displayableText[appLang]["rFormLabelFamilyText"];
            // 
            // buttonAjoutFamilles
            // 
            this.buttonAjoutFamilles.Location = new System.Drawing.Point(179, 21);
            this.buttonAjoutFamilles.Name = "buttonAjoutFamilles";
            this.buttonAjoutFamilles.Size = new System.Drawing.Size(122, 23);
            this.buttonAjoutFamilles.TabIndex = 1;
            this.buttonAjoutFamilles.UseVisualStyleBackColor = true;
            this.buttonAjoutFamilles.Click += new System.EventHandler(this.ButtonAjoutFamilles_Click);
            this.buttonAjoutFamilles.Text = Application.displayableText[appLang]["rFormButtonAddFamilyText"];

            // 
            // buttonSupprimer
            // 
            this.buttonSupprimer.Location = new System.Drawing.Point(20, 158);
            this.buttonSupprimer.Name = "buttonSupprimer";
            this.buttonSupprimer.Size = new System.Drawing.Size(122, 23);
            this.buttonSupprimer.TabIndex = 5;
            this.buttonSupprimer.UseVisualStyleBackColor = true;
            this.buttonSupprimer.Click += new System.EventHandler(this.ButtonSupprimer_Click);
            this.buttonSupprimer.Text = Application.displayableText[appLang]["rFormButtonRemoveText"];
            // 
            // buttonToutSupprimer
            // 
            this.buttonToutSupprimer.Location = new System.Drawing.Point(179, 158);
            this.buttonToutSupprimer.Name = "buttonToutSupprimer";
            this.buttonToutSupprimer.Size = new System.Drawing.Size(122, 23);
            this.buttonToutSupprimer.TabIndex = 5;
            this.buttonToutSupprimer.UseVisualStyleBackColor = true;
            this.buttonToutSupprimer.Text = Application.displayableText[appLang]["rFormButtonRemoveAllText"];
            this.buttonToutSupprimer.Click += new System.EventHandler(this.ButtonToutSupprimer_Click);
            // 
            // listBoxFamilles
            // 
            this.listBoxFamilles.FormattingEnabled = true;
            this.listBoxFamilles.Location = new System.Drawing.Point(20, 53);
            this.listBoxFamilles.Name = "listBoxFamilles";
            this.listBoxFamilles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFamilles.Size = new System.Drawing.Size(281, 95);
            this.listBoxFamilles.TabIndex = 26;
            this.listBoxFamilles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListBoxFamilles_MouseMove);
            // 
            // groupFamilles
            // 
            this.groupFamilles.Controls.Add(this.labelFamilles);
            this.groupFamilles.Controls.Add(this.buttonAjoutFamilles);
            this.groupFamilles.Controls.Add(this.buttonSupprimer);
            this.groupFamilles.Controls.Add(this.buttonToutSupprimer);
            this.groupFamilles.Controls.Add(this.listBoxFamilles);
            this.groupFamilles.Location = new System.Drawing.Point(25, 25);
            this.groupFamilles.Name = "groupFamilles";
            this.groupFamilles.Size = new System.Drawing.Size(323, 199);
            this.groupFamilles.TabIndex = 38;
            this.groupFamilles.TabStop = false;
            this.groupFamilles.Text = Revit_ART_ParametresPartages.Application.displayableText[appLang]["rFormGroupFamiliesText"];
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Location = new System.Drawing.Point(20, 25);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(14, 13);
            this.radioButtonAll.TabIndex = 41;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.radioButtonAll.Text = Application.displayableText[appLang]["rFormRadioButtonAllText"];
            // 
            // radioButtonShared
            // 
            this.radioButtonShared.AutoSize = true;
            this.radioButtonShared.Location = new System.Drawing.Point(170, 25);
            this.radioButtonShared.Name = "radioButtonShared";
            this.radioButtonShared.Size = new System.Drawing.Size(14, 13);
            this.radioButtonShared.TabIndex = 41;
            this.radioButtonShared.UseVisualStyleBackColor = true;
            this.radioButtonShared.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.radioButtonShared.Text = Application.displayableText[appLang]["rFormRadioButtonSharedText"];
            // 
            // checkedListBoxPPGChoice
            // 
            this.checkedListBoxPPGChoice.CheckOnClick = true;
            this.checkedListBoxPPGChoice.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxPPGChoice.FormattingEnabled = true;
            this.checkedListBoxPPGChoice.Location = new System.Drawing.Point(20, 53);
            this.checkedListBoxPPGChoice.Name = "checkedListBoxPPGChoice";
            this.checkedListBoxPPGChoice.Size = new System.Drawing.Size(281, 139);
            this.checkedListBoxPPGChoice.TabIndex = 26;
            this.checkedListBoxPPGChoice.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ItemCheckChanged_UpdateParamToChangeList);
            // 
            // groupPPGChoice
            // 
            this.groupPPGChoice.Controls.Add(this.checkedListBoxPPGChoice);
            this.groupPPGChoice.Controls.Add(this.radioButtonAll);
            this.groupPPGChoice.Controls.Add(this.radioButtonShared);
            this.groupPPGChoice.Location = new System.Drawing.Point(360, 25);
            this.groupPPGChoice.Name = "groupPPGChoice";
            this.groupPPGChoice.Size = new System.Drawing.Size(323, 199);
            this.groupPPGChoice.TabIndex = 38;
            this.groupPPGChoice.TabStop = false;
            this.groupPPGChoice.Text = Application.displayableText[appLang]["rFormGroupPPGChoiceText"];
            // 
            // comboBoxPPGReplaceChoice
            // 
            this.comboBoxPPGReplaceChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPPGReplaceChoice.FormattingEnabled = true;
            this.comboBoxPPGReplaceChoice.Location = new System.Drawing.Point(80, 19);
            this.comboBoxPPGReplaceChoice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxPPGReplaceChoice.Name = "comboBoxPPGReplaceChoice";
            this.comboBoxPPGReplaceChoice.Size = new System.Drawing.Size(369, 21);
            this.comboBoxPPGReplaceChoice.Sorted = true;
            this.comboBoxPPGReplaceChoice.TabIndex = 29;
            this.comboBoxPPGReplaceChoice.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPPGReplaceChoice_SelectedIndexChanged);
            // 
            // listBoxReplace
            // 
            this.listBoxReplace.FormattingEnabled = true;
            this.listBoxReplace.HorizontalScrollbar = true;
            this.listBoxReplace.Location = new System.Drawing.Point(20, 52);
            this.listBoxReplace.Name = "listBoxReplace";
            this.listBoxReplace.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxReplace.Size = new System.Drawing.Size(495, 134);
            this.listBoxReplace.TabIndex = 26;
            this.listBoxReplace.SelectedIndexChanged += new System.EventHandler(this.ListBoxReplace_SelectedIndexChanged);
            // 
            // groupReplaceChoice
            // 
            this.groupReplaceChoice.Controls.Add(this.listBoxReplace);
            this.groupReplaceChoice.Controls.Add(this.comboBoxPPGReplaceChoice);
            this.groupReplaceChoice.Location = new System.Drawing.Point(25, 247);
            this.groupReplaceChoice.Name = "groupReplaceChoice";
            this.groupReplaceChoice.Size = new System.Drawing.Size(533, 199);
            this.groupReplaceChoice.TabIndex = 38;
            this.groupReplaceChoice.TabStop = false;
            this.groupReplaceChoice.Text = Application.displayableText[appLang]["rFormGroupReplaceChoiceText"];
            // 
            // buttonEraseChoice
            // 
            this.buttonEraseChoice.Location = new System.Drawing.Point(573, 305);
            this.buttonEraseChoice.Name = "buttonEraseChoice";
            this.buttonEraseChoice.Size = new System.Drawing.Size(122, 45);
            this.buttonEraseChoice.TabIndex = 3;
            this.buttonEraseChoice.UseVisualStyleBackColor = true;
            this.buttonEraseChoice.Click += new System.EventHandler(this.ButtonEraseChoice_Click);
            this.buttonEraseChoice.Text = Application.displayableText[appLang]["rFormButtonEraseChoiceText"];
            // 
            // buttonLog
            // 
            this.buttonLog.Location = new System.Drawing.Point(573, 403);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(122, 45);
            this.buttonLog.TabIndex = 2;
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.ButtonLog_Click);
            this.buttonLog.Text = Application.displayableText[appLang]["rFormButtonLogText"];
            // 
            // buttonTransaction
            // 
            this.buttonTransaction.Location = new System.Drawing.Point(573, 365);
            this.buttonTransaction.Name = "buttonTransaction";
            this.buttonTransaction.Size = new System.Drawing.Size(122, 23);
            this.buttonTransaction.TabIndex = 2;
            this.buttonTransaction.UseVisualStyleBackColor = true;
            this.buttonTransaction.Click += new System.EventHandler(this.ButtonTransaction_Click);
            this.buttonTransaction.Text = Application.displayableText[appLang]["rFormButtonTransactionText"];
            // 
            // logoArtelia
            // 
            this.logoArtelia.Image = global::Revit_ART_ParametresPartages.Properties.Resources.Atixis;
            this.logoArtelia.Location = new System.Drawing.Point(573, 234);
            this.logoArtelia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logoArtelia.Name = "logoArtelia";
            this.logoArtelia.Size = new System.Drawing.Size(122, 52);
            this.logoArtelia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoArtelia.TabIndex = 41;
            this.logoArtelia.TabStop = false;
            // 
            // linkConnect
            // 
            this.linkConnect.AutoSize = true;
            this.linkConnect.Location = new System.Drawing.Point(253, 448);
            this.linkConnect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkConnect.Name = "linkConnect";
            this.linkConnect.Size = new System.Drawing.Size(0, 13);
            this.linkConnect.TabIndex = 42;
            this.linkConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkConnect.Text = Application.displayableText[appLang]["rFormLinkConnectText"];
            this.linkConnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkConnect_LinkClicked);
            // 
            // ReplacePPGroupeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 468);
            this.Controls.Add(this.groupFamilles);
            this.Controls.Add(this.groupPPGChoice);
            this.Controls.Add(this.groupReplaceChoice);
            this.Controls.Add(this.buttonEraseChoice);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.buttonTransaction);
            this.Controls.Add(this.logoArtelia);
            this.Controls.Add(this.linkConnect);
            this.Name = "ReplacePPGroupeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupFamilles.ResumeLayout(false);
            this.groupFamilles.PerformLayout();
            this.groupPPGChoice.ResumeLayout(false);
            this.groupPPGChoice.PerformLayout();
            this.groupReplaceChoice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoArtelia)).EndInit();
            this.Text = Application.displayableText[appLang]["rFormText"];
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipFamilles;
        private System.Windows.Forms.Label labelFamilles;
        private System.Windows.Forms.Button buttonAjoutFamilles;
        private System.Windows.Forms.Button buttonSupprimer;
        private System.Windows.Forms.Button buttonToutSupprimer;
        private System.Windows.Forms.ListBox listBoxFamilles;
        private System.Windows.Forms.GroupBox groupFamilles;
        //
        private System.Windows.Forms.RadioButton radioButtonAll;
        private System.Windows.Forms.RadioButton radioButtonShared;
        private System.Windows.Forms.CheckedListBox checkedListBoxPPGChoice;
        private System.Windows.Forms.GroupBox groupPPGChoice;
        //
        private System.Windows.Forms.ComboBox comboBoxPPGReplaceChoice;
        private System.Windows.Forms.ListBox listBoxReplace;
        private System.Windows.Forms.GroupBox groupReplaceChoice;
        //
        private System.Windows.Forms.Button buttonEraseChoice;
        private System.Windows.Forms.Button buttonLog;
        private System.Windows.Forms.Button buttonTransaction;
        private System.Windows.Forms.PictureBox logoArtelia;
        private System.Windows.Forms.LinkLabel linkConnect;
    }
}