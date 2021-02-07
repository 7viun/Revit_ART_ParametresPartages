namespace Revit_ART_ParametresPartages
{
    partial class RemovePPGForm
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
            //FamilyChoice
            this.labelFamilles = new System.Windows.Forms.Label();
            this.buttonAjoutFamilles = new System.Windows.Forms.Button();
            this.buttonSupprimer = new System.Windows.Forms.Button();
            this.buttonToutSupprimer = new System.Windows.Forms.Button();
            this.listBoxFamilles = new System.Windows.Forms.ListBox();
            this.groupFamilles = new System.Windows.Forms.GroupBox();
            //ParamChoice
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.radioButtonShared = new System.Windows.Forms.RadioButton();
            this.checkedListBoxPPGChoice = new System.Windows.Forms.CheckedListBox();
            this.groupPPGChoice = new System.Windows.Forms.GroupBox();
            //ReplaceChoice
            this.listBoxReplace = new System.Windows.Forms.ListBox();
            this.groupReplaceChoice = new System.Windows.Forms.GroupBox();
            //Utils
            this.buttonLog = new System.Windows.Forms.Button();
            this.buttonTransaction = new System.Windows.Forms.Button();
            this.logoArtelia = new System.Windows.Forms.PictureBox();
            this.linkConnect = new System.Windows.Forms.LinkLabel();
            //Suspends
            this.groupFamilles.SuspendLayout();
            this.groupPPGChoice.SuspendLayout();
            this.groupReplaceChoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoArtelia)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFamilles
            // 
            this.labelFamilles.AutoSize = true;
            this.labelFamilles.Location = new System.Drawing.Point(26, 40);
            this.labelFamilles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFamilles.Name = "labelFamilles";
            this.labelFamilles.Size = new System.Drawing.Size(204, 20);
            this.labelFamilles.TabIndex = 0;
            this.labelFamilles.Text = Application.displayableText[appLang]["rFormLabelFamilyText"];
            // 
            // buttonAjoutFamilles
            // 
            this.buttonAjoutFamilles.Location = new System.Drawing.Point(268, 32);
            this.buttonAjoutFamilles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAjoutFamilles.Name = "buttonAjoutFamilles";
            this.buttonAjoutFamilles.Size = new System.Drawing.Size(183, 35);
            this.buttonAjoutFamilles.TabIndex = 1;
            this.buttonAjoutFamilles.Text = Application.displayableText[appLang]["rFormButtonAddFamilyText"];
            this.buttonAjoutFamilles.UseVisualStyleBackColor = true;
            this.buttonAjoutFamilles.Click += new System.EventHandler(this.ButtonAjoutFamilles_Click);
            // 
            // buttonSupprimer
            // 
            this.buttonSupprimer.Location = new System.Drawing.Point(30, 243);
            this.buttonSupprimer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSupprimer.Name = "buttonSupprimer";
            this.buttonSupprimer.Size = new System.Drawing.Size(183, 35);
            this.buttonSupprimer.TabIndex = 5;
            this.buttonSupprimer.Text = Application.displayableText[appLang]["rFormButtonRemoveText"];
            this.buttonSupprimer.UseVisualStyleBackColor = true;
            this.buttonSupprimer.Click += new System.EventHandler(this.ButtonSupprimer_Click);
            // 
            // buttonToutSupprimer
            // 
            this.buttonToutSupprimer.Location = new System.Drawing.Point(268, 243);
            this.buttonToutSupprimer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonToutSupprimer.Name = "buttonToutSupprimer";
            this.buttonToutSupprimer.Size = new System.Drawing.Size(183, 35);
            this.buttonToutSupprimer.TabIndex = 5;
            this.buttonToutSupprimer.Text = Application.displayableText[appLang]["rFormButtonRemoveAllText"];
            this.buttonToutSupprimer.UseVisualStyleBackColor = true;
            this.buttonToutSupprimer.Click += new System.EventHandler(this.ButtonToutSupprimer_Click);
            // 
            // listBoxFamilles
            // 
            this.listBoxFamilles.FormattingEnabled = true;
            this.listBoxFamilles.ItemHeight = 20;
            this.listBoxFamilles.Location = new System.Drawing.Point(30, 82);
            this.listBoxFamilles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxFamilles.Name = "listBoxFamilles";
            this.listBoxFamilles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFamilles.Size = new System.Drawing.Size(420, 144);
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
            this.groupFamilles.Location = new System.Drawing.Point(38, 38);
            this.groupFamilles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupFamilles.Name = "groupFamilles";
            this.groupFamilles.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupFamilles.Size = new System.Drawing.Size(484, 306);
            this.groupFamilles.TabIndex = 38;
            this.groupFamilles.TabStop = false;
            this.groupFamilles.Text = Application.displayableText[appLang]["rFormGroupFamiliesText"];
            /// 
            /// Choice Group ( checkedListBox )
            /// 
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Location = new System.Drawing.Point(30, 38);
            this.radioButtonAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(178, 24);
            this.radioButtonAll.TabIndex = 41;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = Application.displayableText[appLang]["rFormRadioButtonAllText"];
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButtonShared
            // 
            this.radioButtonShared.AutoSize = true;
            this.radioButtonShared.Location = new System.Drawing.Point(255, 38);
            this.radioButtonShared.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonShared.Name = "radioButtonShared";
            this.radioButtonShared.Size = new System.Drawing.Size(189, 24);
            this.radioButtonShared.TabIndex = 41;
            this.radioButtonShared.Text = Application.displayableText[appLang]["rFormRadioButtonSharedText"];
            this.radioButtonShared.UseVisualStyleBackColor = true;
            this.radioButtonShared.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // checkedListBoxPPGChoice
            // 
            this.checkedListBoxPPGChoice.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxPPGChoice.FormattingEnabled = true;
            this.checkedListBoxPPGChoice.Location = new System.Drawing.Point(30, 82);
            this.checkedListBoxPPGChoice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkedListBoxPPGChoice.Name = "checkedListBoxPPGChoice";
            this.checkedListBoxPPGChoice.Size = new System.Drawing.Size(420, 220);
            this.checkedListBoxPPGChoice.TabIndex = 26;
            this.checkedListBoxPPGChoice.CheckOnClick = true;
            this.checkedListBoxPPGChoice.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ItemCheckChanged_UpdateParamToChangeList);
            // 
            // groupPPGChoice
            // 
            this.groupPPGChoice.Controls.Add(this.checkedListBoxPPGChoice);
            this.groupPPGChoice.Controls.Add(this.radioButtonAll);
            this.groupPPGChoice.Controls.Add(this.radioButtonShared);
            this.groupPPGChoice.Location = new System.Drawing.Point(540, 38);
            this.groupPPGChoice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupPPGChoice.Name = "groupPPGChoice";
            this.groupPPGChoice.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupPPGChoice.Size = new System.Drawing.Size(484, 306);
            this.groupPPGChoice.TabIndex = 38;
            this.groupPPGChoice.TabStop = false;
            this.groupPPGChoice.Text = Application.displayableText[appLang]["rmFormGroupPPGChoiceText"];
            ///
            /// groupReplaceChoice
            ///
            // 
            // listBoxReplace
            // 
            this.listBoxReplace.FormattingEnabled = true;
            this.listBoxReplace.ItemHeight = 20;
            this.listBoxReplace.Location = new System.Drawing.Point(30, 30);
            this.listBoxReplace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxReplace.Name = "listBoxReplace";
            this.listBoxReplace.HorizontalScrollbar = true;
            this.listBoxReplace.Size = new System.Drawing.Size(740, 254);
            this.listBoxReplace.TabIndex = 26;
            // 
            // groupReplaceChoice
            // 
            this.groupReplaceChoice.Controls.Add(this.listBoxReplace);
            this.groupReplaceChoice.Location = new System.Drawing.Point(38, 380);
            this.groupReplaceChoice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupReplaceChoice.Name = "groupReplaceChoice";
            this.groupReplaceChoice.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupReplaceChoice.Size = new System.Drawing.Size(800, 306);
            this.groupReplaceChoice.TabIndex = 38;
            this.groupReplaceChoice.TabStop = false;
            this.groupReplaceChoice.Text = Application.displayableText[appLang]["rmFormGroupReplaceChoiceText"];
            /// 
            /// Other Buttons
            ///
            //
            // buttonLog
            //
            this.buttonLog.Location = new System.Drawing.Point(860, 570);
            this.buttonLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(183, 70);
            this.buttonLog.TabIndex = 2;
            this.buttonLog.Text = Application.displayableText[appLang]["rFormButtonLogText"];
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.ButtonLog_Click);
            // 
            // buttonTransaction
            // 
            this.buttonTransaction.Location = new System.Drawing.Point(860, 477);
            this.buttonTransaction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTransaction.Name = "buttonTransaction";
            this.buttonTransaction.Size = new System.Drawing.Size(183, 70);
            this.buttonTransaction.TabIndex = 2;
            this.buttonTransaction.Text = Application.displayableText[appLang]["rmFormButtonTransactionText"];
            this.buttonTransaction.UseVisualStyleBackColor = true;
            this.buttonTransaction.Click += new System.EventHandler(this.ButtonTransaction_Click);
            // 
            // logoArtelia
            // 
            this.logoArtelia.Image = global::Revit_ART_ParametresPartages.Properties.Resources.Atixis;
            this.logoArtelia.Location = new System.Drawing.Point(860, 360);
            this.logoArtelia.Name = "logoArtelia";
            this.logoArtelia.Size = new System.Drawing.Size(183, 80);
            this.logoArtelia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoArtelia.TabIndex = 41;
            this.logoArtelia.TabStop = false;
            // 
            // linkConnect
            // 
            this.linkConnect.AutoSize = true;
            this.linkConnect.Location = new System.Drawing.Point(380, 690);
            this.linkConnect.Name = "linkConnect";
            this.linkConnect.Size = new System.Drawing.Size(197, 13);
            this.linkConnect.TabIndex = 42;
            this.linkConnect.TabStop = true;
            this.linkConnect.Text = Application.displayableText[appLang]["rFormLinkConnectText"];
            this.linkConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkConnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkConnect_LinkClicked);
            // 
            // ReplacePPGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.Controls.Add(this.groupFamilles);
            this.Controls.Add(this.groupPPGChoice);
            this.Controls.Add(this.groupReplaceChoice);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.buttonTransaction);
            this.Controls.Add(this.logoArtelia);
            this.Controls.Add(this.linkConnect);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ReplacePPGForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = Application.displayableText[appLang]["rmFormText"];
            this.groupFamilles.ResumeLayout(false);
            this.groupFamilles.PerformLayout();
            this.groupPPGChoice.ResumeLayout(false);
            this.groupPPGChoice.PerformLayout();
            this.groupReplaceChoice.ResumeLayout(false);
            this.groupReplaceChoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoArtelia)).EndInit();
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
        private System.Windows.Forms.ListBox listBoxReplace;
        private System.Windows.Forms.GroupBox groupReplaceChoice;
        //
        private System.Windows.Forms.Button buttonLog;
        private System.Windows.Forms.Button buttonTransaction;
        private System.Windows.Forms.PictureBox logoArtelia;
        private System.Windows.Forms.LinkLabel linkConnect;
    }
}