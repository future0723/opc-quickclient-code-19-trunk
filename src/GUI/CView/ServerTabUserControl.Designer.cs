/*
 *  ServerTabUserControl.Designer.cs
 *
 *  Copyright (C) 2010 Stephane Delapierre <stephane.delapierre@gmail.com>
 *
 *  Contributors : Julien Hannier <julien.hannier@gmail.com>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

namespace GUI.CView
{
    partial class ServerTabUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_ItemsTabControl = new System.Windows.Forms.TabControl();
            this.m_ItemsTab = new System.Windows.Forms.TabPage();
            this.m_ItemsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.m_ItemsCountLabelValue = new System.Windows.Forms.Label();
            this.m_ItemsCountLabel = new System.Windows.Forms.Label();
            this.m_ItemsTreeView = new System.Windows.Forms.TreeView();
            this.m_ItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.m_IDColumnDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_TypeColumnDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_ValueColumnDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_QualityColumnDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_TimestampColumnDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._RightMenuItemsList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_WriteSelectedItemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_AcquitSelectedItemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.m_ItemsTabsGroupBox = new System.Windows.Forms.GroupBox();
            this.m_ItemsTabNameComboBox = new System.Windows.Forms.ComboBox();
            this.m_AddItemsButton = new System.Windows.Forms.Button();
            this.m_ItemsFilterGroupBox = new System.Windows.Forms.GroupBox();
            this.m_ClearFilterButton = new System.Windows.Forms.Button();
            this.m_FilterValueTextBox = new System.Windows.Forms.TextBox();
            this.m_FilterTypeComboBox = new System.Windows.Forms.ComboBox();
            this.m_CaseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.m_QuitServerButton = new System.Windows.Forms.Button();
            this.m_UpdateRateLabel = new System.Windows.Forms.Label();
            this.m_UpdateRateTrackBar = new System.Windows.Forms.TrackBar();
            this.LabelUpdateValue = new System.Windows.Forms.Label();
            this.m_ItemsTabControl.SuspendLayout();
            this.m_ItemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ItemsSplitContainer)).BeginInit();
            this.m_ItemsSplitContainer.Panel1.SuspendLayout();
            this.m_ItemsSplitContainer.Panel2.SuspendLayout();
            this.m_ItemsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ItemsDataGridView)).BeginInit();
            this._RightMenuItemsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ItemsBindingSource)).BeginInit();
            this.m_ItemsTabsGroupBox.SuspendLayout();
            this.m_ItemsFilterGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_UpdateRateTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ItemsTabControl
            // 
            this.m_ItemsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ItemsTabControl.Controls.Add(this.m_ItemsTab);
            this.m_ItemsTabControl.Location = new System.Drawing.Point(3, 3);
            this.m_ItemsTabControl.Multiline = true;
            this.m_ItemsTabControl.Name = "m_ItemsTabControl";
            this.m_ItemsTabControl.SelectedIndex = 0;
            this.m_ItemsTabControl.Size = new System.Drawing.Size(971, 465);
            this.m_ItemsTabControl.TabIndex = 0;
            // 
            // m_ItemsTab
            // 
            this.m_ItemsTab.Controls.Add(this.m_ItemsSplitContainer);
            this.m_ItemsTab.Location = new System.Drawing.Point(4, 22);
            this.m_ItemsTab.Name = "m_ItemsTab";
            this.m_ItemsTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_ItemsTab.Size = new System.Drawing.Size(963, 439);
            this.m_ItemsTab.TabIndex = 0;
            this.m_ItemsTab.Text = "Items";
            this.m_ItemsTab.UseVisualStyleBackColor = true;
            // 
            // m_ItemsSplitContainer
            // 
            this.m_ItemsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ItemsSplitContainer.IsSplitterFixed = true;
            this.m_ItemsSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.m_ItemsSplitContainer.Name = "m_ItemsSplitContainer";
            // 
            // m_ItemsSplitContainer.Panel1
            // 
            this.m_ItemsSplitContainer.Panel1.Controls.Add(this.m_ItemsCountLabelValue);
            this.m_ItemsSplitContainer.Panel1.Controls.Add(this.m_ItemsCountLabel);
            this.m_ItemsSplitContainer.Panel1.Controls.Add(this.m_ItemsTreeView);
            // 
            // m_ItemsSplitContainer.Panel2
            // 
            this.m_ItemsSplitContainer.Panel2.Controls.Add(this.m_ItemsDataGridView);
            this.m_ItemsSplitContainer.Panel2.Controls.Add(this.m_ItemsTabsGroupBox);
            this.m_ItemsSplitContainer.Panel2.Controls.Add(this.m_ItemsFilterGroupBox);
            this.m_ItemsSplitContainer.Size = new System.Drawing.Size(957, 433);
            this.m_ItemsSplitContainer.SplitterDistance = 219;
            this.m_ItemsSplitContainer.TabIndex = 0;
            // 
            // m_ItemsCountLabelValue
            // 
            this.m_ItemsCountLabelValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ItemsCountLabelValue.AutoSize = true;
            this.m_ItemsCountLabelValue.Location = new System.Drawing.Point(87, 422);
            this.m_ItemsCountLabelValue.Name = "m_ItemsCountLabelValue";
            this.m_ItemsCountLabelValue.Size = new System.Drawing.Size(11, 12);
            this.m_ItemsCountLabelValue.TabIndex = 2;
            this.m_ItemsCountLabelValue.Text = "0";
            // 
            // m_ItemsCountLabel
            // 
            this.m_ItemsCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ItemsCountLabel.AutoSize = true;
            this.m_ItemsCountLabel.Location = new System.Drawing.Point(13, 422);
            this.m_ItemsCountLabel.Name = "m_ItemsCountLabel";
            this.m_ItemsCountLabel.Size = new System.Drawing.Size(83, 12);
            this.m_ItemsCountLabel.TabIndex = 1;
            this.m_ItemsCountLabel.Text = "Items count :";
            // 
            // m_ItemsTreeView
            // 
            this.m_ItemsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ItemsTreeView.Location = new System.Drawing.Point(3, 3);
            this.m_ItemsTreeView.Name = "m_ItemsTreeView";
            this.m_ItemsTreeView.PathSeparator = "/";
            this.m_ItemsTreeView.Size = new System.Drawing.Size(213, 410);
            this.m_ItemsTreeView.TabIndex = 0;
            this.m_ItemsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ItemsTreeView_NodeMouseClick);
            // 
            // m_ItemsDataGridView
            // 
            this.m_ItemsDataGridView.AllowUserToAddRows = false;
            this.m_ItemsDataGridView.AllowUserToDeleteRows = false;
            this.m_ItemsDataGridView.AllowUserToResizeRows = false;
            this.m_ItemsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ItemsDataGridView.AutoGenerateColumns = false;
            this.m_ItemsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_ItemsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_ItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_IDColumnDataGrid,
            this.m_TypeColumnDataGrid,
            this.m_ValueColumnDataGrid,
            this.m_QualityColumnDataGrid,
            this.m_TimestampColumnDataGrid});
            this.m_ItemsDataGridView.ContextMenuStrip = this._RightMenuItemsList;
            this.m_ItemsDataGridView.DataSource = this._ItemsBindingSource;
            this.m_ItemsDataGridView.Location = new System.Drawing.Point(3, 59);
            this.m_ItemsDataGridView.Name = "m_ItemsDataGridView";
            this.m_ItemsDataGridView.RowHeadersVisible = false;
            this.m_ItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_ItemsDataGridView.Size = new System.Drawing.Size(728, 383);
            this.m_ItemsDataGridView.TabIndex = 0;
            this.m_ItemsDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.m_ItemsDataGridView_DataError);
            this.m_ItemsDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.ItemsDataGridView_RowPrePaint);
            this.m_ItemsDataGridView.DoubleClick += new System.EventHandler(this.ItemsDataGridView_DoubleClick);
            // 
            // m_IDColumnDataGrid
            // 
            this.m_IDColumnDataGrid.DataPropertyName = "ID";
            this.m_IDColumnDataGrid.HeaderText = "ID";
            this.m_IDColumnDataGrid.Name = "m_IDColumnDataGrid";
            this.m_IDColumnDataGrid.ReadOnly = true;
            this.m_IDColumnDataGrid.Width = 280;
            // 
            // m_TypeColumnDataGrid
            // 
            this.m_TypeColumnDataGrid.DataPropertyName = "Type";
            this.m_TypeColumnDataGrid.HeaderText = "Type";
            this.m_TypeColumnDataGrid.Name = "m_TypeColumnDataGrid";
            this.m_TypeColumnDataGrid.ReadOnly = true;
            // 
            // m_ValueColumnDataGrid
            // 
            this.m_ValueColumnDataGrid.DataPropertyName = "Value";
            this.m_ValueColumnDataGrid.HeaderText = "Value";
            this.m_ValueColumnDataGrid.Name = "m_ValueColumnDataGrid";
            this.m_ValueColumnDataGrid.ReadOnly = true;
            this.m_ValueColumnDataGrid.Width = 120;
            // 
            // m_QualityColumnDataGrid
            // 
            this.m_QualityColumnDataGrid.DataPropertyName = "Quality";
            this.m_QualityColumnDataGrid.HeaderText = "Quality";
            this.m_QualityColumnDataGrid.Name = "m_QualityColumnDataGrid";
            this.m_QualityColumnDataGrid.ReadOnly = true;
            this.m_QualityColumnDataGrid.Width = 110;
            // 
            // m_TimestampColumnDataGrid
            // 
            this.m_TimestampColumnDataGrid.DataPropertyName = "Timestamp";
            this.m_TimestampColumnDataGrid.HeaderText = "Timestamp";
            this.m_TimestampColumnDataGrid.Name = "m_TimestampColumnDataGrid";
            this.m_TimestampColumnDataGrid.ReadOnly = true;
            this.m_TimestampColumnDataGrid.Width = 130;
            // 
            // _RightMenuItemsList
            // 
            this._RightMenuItemsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_WriteSelectedItemsMenuItem,
            this.m_AcquitSelectedItemsMenuItem});
            this._RightMenuItemsList.Name = "m_rightMenuItemsList";
            this._RightMenuItemsList.Size = new System.Drawing.Size(200, 48);
            // 
            // m_WriteSelectedItemsMenuItem
            // 
            this.m_WriteSelectedItemsMenuItem.Name = "m_WriteSelectedItemsMenuItem";
            this.m_WriteSelectedItemsMenuItem.Size = new System.Drawing.Size(199, 22);
            this.m_WriteSelectedItemsMenuItem.Text = "Write selected items";
            this.m_WriteSelectedItemsMenuItem.Click += new System.EventHandler(this.WriteSelectedItemsMenuItem_Click);
            // 
            // m_AcquitSelectedItemsMenuItem
            // 
            this.m_AcquitSelectedItemsMenuItem.Name = "m_AcquitSelectedItemsMenuItem";
            this.m_AcquitSelectedItemsMenuItem.Size = new System.Drawing.Size(199, 22);
            this.m_AcquitSelectedItemsMenuItem.Text = "Acquit selected items";
            this.m_AcquitSelectedItemsMenuItem.Click += new System.EventHandler(this.AcquitSelectedItemsMenuItem_Click);
            // 
            // m_ItemsTabsGroupBox
            // 
            this.m_ItemsTabsGroupBox.Controls.Add(this.m_ItemsTabNameComboBox);
            this.m_ItemsTabsGroupBox.Controls.Add(this.m_AddItemsButton);
            this.m_ItemsTabsGroupBox.Location = new System.Drawing.Point(409, 3);
            this.m_ItemsTabsGroupBox.Name = "m_ItemsTabsGroupBox";
            this.m_ItemsTabsGroupBox.Size = new System.Drawing.Size(322, 51);
            this.m_ItemsTabsGroupBox.TabIndex = 1;
            this.m_ItemsTabsGroupBox.TabStop = false;
            this.m_ItemsTabsGroupBox.Text = "Items tabs";
            // 
            // m_ItemsTabNameComboBox
            // 
            this.m_ItemsTabNameComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_ItemsTabNameComboBox.FormattingEnabled = true;
            this.m_ItemsTabNameComboBox.Location = new System.Drawing.Point(157, 19);
            this.m_ItemsTabNameComboBox.Name = "m_ItemsTabNameComboBox";
            this.m_ItemsTabNameComboBox.Size = new System.Drawing.Size(156, 20);
            this.m_ItemsTabNameComboBox.TabIndex = 1;
            // 
            // m_AddItemsButton
            // 
            this.m_AddItemsButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_AddItemsButton.Location = new System.Drawing.Point(8, 18);
            this.m_AddItemsButton.Name = "m_AddItemsButton";
            this.m_AddItemsButton.Size = new System.Drawing.Size(140, 23);
            this.m_AddItemsButton.TabIndex = 0;
            this.m_AddItemsButton.Text = "Add selected items to :";
            this.m_AddItemsButton.UseVisualStyleBackColor = false;
            this.m_AddItemsButton.Click += new System.EventHandler(this.AddItemsButton_Click);
            // 
            // m_ItemsFilterGroupBox
            // 
            this.m_ItemsFilterGroupBox.Controls.Add(this.m_ClearFilterButton);
            this.m_ItemsFilterGroupBox.Controls.Add(this.m_FilterValueTextBox);
            this.m_ItemsFilterGroupBox.Controls.Add(this.m_FilterTypeComboBox);
            this.m_ItemsFilterGroupBox.Controls.Add(this.m_CaseSensitiveCheckBox);
            this.m_ItemsFilterGroupBox.Location = new System.Drawing.Point(3, 3);
            this.m_ItemsFilterGroupBox.Name = "m_ItemsFilterGroupBox";
            this.m_ItemsFilterGroupBox.Size = new System.Drawing.Size(400, 51);
            this.m_ItemsFilterGroupBox.TabIndex = 0;
            this.m_ItemsFilterGroupBox.TabStop = false;
            this.m_ItemsFilterGroupBox.Text = "Items filter";
            // 
            // m_ClearFilterButton
            // 
            this.m_ClearFilterButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_ClearFilterButton.Location = new System.Drawing.Point(341, 18);
            this.m_ClearFilterButton.Name = "m_ClearFilterButton";
            this.m_ClearFilterButton.Size = new System.Drawing.Size(50, 23);
            this.m_ClearFilterButton.TabIndex = 3;
            this.m_ClearFilterButton.Text = "Clear";
            this.m_ClearFilterButton.UseVisualStyleBackColor = false;
            this.m_ClearFilterButton.Click += new System.EventHandler(this.ClearFilterButton_Click);
            // 
            // m_FilterValueTextBox
            // 
            this.m_FilterValueTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_FilterValueTextBox.Location = new System.Drawing.Point(248, 20);
            this.m_FilterValueTextBox.Name = "m_FilterValueTextBox";
            this.m_FilterValueTextBox.Size = new System.Drawing.Size(80, 21);
            this.m_FilterValueTextBox.TabIndex = 2;
            this.m_FilterValueTextBox.TextChanged += new System.EventHandler(this.FilterValueTextBox_TextChanged);
            // 
            // m_FilterTypeComboBox
            // 
            this.m_FilterTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_FilterTypeComboBox.FormattingEnabled = true;
            this.m_FilterTypeComboBox.Location = new System.Drawing.Point(118, 20);
            this.m_FilterTypeComboBox.Name = "m_FilterTypeComboBox";
            this.m_FilterTypeComboBox.Size = new System.Drawing.Size(126, 20);
            this.m_FilterTypeComboBox.TabIndex = 1;
            this.m_FilterTypeComboBox.TextChanged += new System.EventHandler(this.FilterTypeComboBox_TextChanged);
            // 
            // m_CaseSensitiveCheckBox
            // 
            this.m_CaseSensitiveCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_CaseSensitiveCheckBox.AutoSize = true;
            this.m_CaseSensitiveCheckBox.Location = new System.Drawing.Point(9, 22);
            this.m_CaseSensitiveCheckBox.Name = "m_CaseSensitiveCheckBox";
            this.m_CaseSensitiveCheckBox.Size = new System.Drawing.Size(108, 16);
            this.m_CaseSensitiveCheckBox.TabIndex = 0;
            this.m_CaseSensitiveCheckBox.Text = "Case sensitive";
            this.m_CaseSensitiveCheckBox.UseVisualStyleBackColor = true;
            this.m_CaseSensitiveCheckBox.Click += new System.EventHandler(this.CaseSensitiveCheckBox_Click);
            // 
            // m_QuitServerButton
            // 
            this.m_QuitServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_QuitServerButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_QuitServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_QuitServerButton.Location = new System.Drawing.Point(774, 474);
            this.m_QuitServerButton.Name = "m_QuitServerButton";
            this.m_QuitServerButton.Size = new System.Drawing.Size(200, 28);
            this.m_QuitServerButton.TabIndex = 1;
            this.m_QuitServerButton.Text = "Quit OPC Server";
            this.m_QuitServerButton.UseVisualStyleBackColor = false;
            this.m_QuitServerButton.Click += new System.EventHandler(this.QuitServerButton_Click);
            // 
            // m_UpdateRateLabel
            // 
            this.m_UpdateRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_UpdateRateLabel.AutoSize = true;
            this.m_UpdateRateLabel.Location = new System.Drawing.Point(23, 482);
            this.m_UpdateRateLabel.Name = "m_UpdateRateLabel";
            this.m_UpdateRateLabel.Size = new System.Drawing.Size(107, 12);
            this.m_UpdateRateLabel.TabIndex = 2;
            this.m_UpdateRateLabel.Text = "更新速度(0 - 10):";
            // 
            // m_UpdateRateTrackBar
            // 
            this.m_UpdateRateTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_UpdateRateTrackBar.LargeChange = 1;
            this.m_UpdateRateTrackBar.Location = new System.Drawing.Point(154, 477);
            this.m_UpdateRateTrackBar.Name = "m_UpdateRateTrackBar";
            this.m_UpdateRateTrackBar.Size = new System.Drawing.Size(200, 45);
            this.m_UpdateRateTrackBar.TabIndex = 3;
            this.m_UpdateRateTrackBar.Tag = "";
            this.m_UpdateRateTrackBar.Value = 1;
            this.m_UpdateRateTrackBar.ValueChanged += new System.EventHandler(this.m_UpdateRateTrackBar_ValueChanged);
            // 
            // LabelUpdateValue
            // 
            this.LabelUpdateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelUpdateValue.AutoSize = true;
            this.LabelUpdateValue.Location = new System.Drawing.Point(360, 482);
            this.LabelUpdateValue.Name = "LabelUpdateValue";
            this.LabelUpdateValue.Size = new System.Drawing.Size(41, 12);
            this.LabelUpdateValue.TabIndex = 4;
            this.LabelUpdateValue.Text = "5 分钟";
            // 
            // ServerTabUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelUpdateValue);
            this.Controls.Add(this.m_UpdateRateTrackBar);
            this.Controls.Add(this.m_UpdateRateLabel);
            this.Controls.Add(this.m_QuitServerButton);
            this.Controls.Add(this.m_ItemsTabControl);
            this.Name = "ServerTabUserControl";
            this.Size = new System.Drawing.Size(977, 504);
            this.Load += new System.EventHandler(this.ServerTabUserControl_Load);
            this.m_ItemsTabControl.ResumeLayout(false);
            this.m_ItemsTab.ResumeLayout(false);
            this.m_ItemsSplitContainer.Panel1.ResumeLayout(false);
            this.m_ItemsSplitContainer.Panel1.PerformLayout();
            this.m_ItemsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_ItemsSplitContainer)).EndInit();
            this.m_ItemsSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_ItemsDataGridView)).EndInit();
            this._RightMenuItemsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._ItemsBindingSource)).EndInit();
            this.m_ItemsTabsGroupBox.ResumeLayout(false);
            this.m_ItemsFilterGroupBox.ResumeLayout(false);
            this.m_ItemsFilterGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_UpdateRateTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl m_ItemsTabControl;
        private System.Windows.Forms.TabPage m_ItemsTab;
        private System.Windows.Forms.SplitContainer m_ItemsSplitContainer;
        private System.Windows.Forms.TreeView m_ItemsTreeView;
        private System.Windows.Forms.GroupBox m_ItemsTabsGroupBox;
        private System.Windows.Forms.GroupBox m_ItemsFilterGroupBox;
        private System.Windows.Forms.Button m_QuitServerButton;
        private System.Windows.Forms.Label m_UpdateRateLabel;
        private System.Windows.Forms.TrackBar m_UpdateRateTrackBar;
        private System.Windows.Forms.Label m_ItemsCountLabelValue;
        private System.Windows.Forms.Label m_ItemsCountLabel;
        private System.Windows.Forms.Button m_ClearFilterButton;
        private System.Windows.Forms.TextBox m_FilterValueTextBox;
        private System.Windows.Forms.ComboBox m_FilterTypeComboBox;
        private System.Windows.Forms.CheckBox m_CaseSensitiveCheckBox;
        private System.Windows.Forms.ComboBox m_ItemsTabNameComboBox;
        private System.Windows.Forms.Button m_AddItemsButton;
        private System.Windows.Forms.DataGridView m_ItemsDataGridView;
        private System.Windows.Forms.ContextMenuStrip _RightMenuItemsList;
        private System.Windows.Forms.ToolStripMenuItem m_WriteSelectedItemsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_AcquitSelectedItemsMenuItem;
        private System.Windows.Forms.BindingSource _ItemsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_IDColumnDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_TypeColumnDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_ValueColumnDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_QualityColumnDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_TimestampColumnDataGrid;
        private System.Windows.Forms.Label LabelUpdateValue;
    }
}
