/*
 *  ServerTabUserControl.cs
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using GUI.CController;
using GUI.CModel;
using GUI.CErrorLog;
using GUI.CUtility;

namespace GUI.CView
{
    partial class ServerTabUserControl : UserControl, IServerView
    {
        private TabPage _serverTabPage;
        private DataTable _itemsDataTable;

        private readonly Dictionary<string, Color> _itemsBackColorDictionary;
        private readonly IServerController _serverController;

        public delegate void UpdateRateEventHandler(object sender, UpdateRateEventArgs eventArgs);
        public event UpdateRateEventHandler OnUpdateRate;

        public ServerTabUserControl(IServerController serverController, string serverName)
        {
            InitializeComponent();

            InitializeServerTabPage(serverName);
            InitializeServerDataTable();

            m_ItemsTreeView.Nodes.Add(serverName);
            _ItemsBindingSource.DataSource = _itemsDataTable;

            _itemsBackColorDictionary = new Dictionary<string, Color>();

            _serverController = serverController;
        }

        private void InitializeServerTabPage(string serverName)
        {
            _serverTabPage = new TabPage();

            this.Dock = DockStyle.Fill;

            _serverTabPage.Text = serverName;
            _serverTabPage.Controls.Add(this);
            _serverTabPage.UseVisualStyleBackColor = true;
            _serverTabPage.Padding = new Padding(3);
        }

        private void InitializeServerDataTable()
        {
            _itemsDataTable = new DataTable("Items");

            var column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "ID",
                ReadOnly = true
            };
            _itemsDataTable.Columns.Add(column);
            var keys = new DataColumn[1];
            keys[0] = column;
            _itemsDataTable.PrimaryKey = keys;

            column = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Type",
                ReadOnly = true
            };
            _itemsDataTable.Columns.Add(column);

            column = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Value",
                ReadOnly = false
            };
            _itemsDataTable.Columns.Add(column);

            column = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Quality",
                ReadOnly = false
            };
            _itemsDataTable.Columns.Add(column);

            column = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Timestamp",
                ReadOnly = false
            };
            _itemsDataTable.Columns.Add(column);
        }

        public TabPage GetTabPage()
        {
            return _serverTabPage;
        }

        public void ServerModelChange(object sender, ModelChangeEventArgs eventArgs)
        {
            if (eventArgs.EventType == ModelChangeEventType.Add)
            {
                var dataRow = _itemsDataTable.NewRow();
                dataRow["ID"] = eventArgs.Id;
                dataRow["Type"] = eventArgs.Type;
                dataRow["Value"] = eventArgs.Value;
                dataRow["Quality"] = eventArgs.Quality;
                dataRow["Timestamp"] = eventArgs.Timestamp;
                _itemsDataTable.Rows.Add(dataRow);

                Color backColor;
                if (eventArgs.Quality.Equals(OpcUtility.ITEM_QUALITY_BAD))
                {
                    backColor = Color.LightGray;
                }
                else
                {
                    backColor = Color.White;
                }
                _itemsBackColorDictionary.Add(eventArgs.Id, backColor);

                m_ItemsCountLabelValue.Text = _itemsDataTable.Rows.Count.ToString();
                UpdateTreeView(eventArgs.Id);
            }
            else
            {
                var dataRow = _itemsDataTable.Rows.Find(eventArgs.Id);
                if (dataRow != null)
                {
                    if (!dataRow.ItemArray[2].Equals(eventArgs.Value) ||
                        !dataRow.ItemArray[3].Equals(eventArgs.Quality) ||
                        !dataRow.ItemArray[4].Equals(eventArgs.Timestamp))
                    {
                        dataRow["Value"] = eventArgs.Value;
                        dataRow["Quality"] = eventArgs.Quality;
                        dataRow["Timestamp"] = eventArgs.Timestamp;

                        _itemsBackColorDictionary.Remove(eventArgs.Id);
                        _itemsBackColorDictionary.Add(eventArgs.Id, Color.LightGreen);
                    }
                }
            }
        }

        private void UpdateTreeView(string pItemId)
        {
            string[] path = pItemId.Split(OpcUtility.ITEM_PATH_SEPARATOR);
            TreeNode parent = m_ItemsTreeView.Nodes[0];

            for (int i = 0; i < path.Length - 1; ++i)
            {
                TreeNode node = null;

                for (int j = 0; j < parent.Nodes.Count; ++j)
                {
                    if (parent.Nodes[j].Text.Equals(path[i]))
                    {
                        node = parent.Nodes[j];
                        break;
                    }
                }

                if (node == null)
                {
                    node = new TreeNode(path[i]);
                    parent.Nodes.Add(node);
                }
                parent = node;
            }

            m_ItemsTreeView.ExpandAll();
        }

        /// <summary>
        /// 更新频率（毫秒）
        /// </summary>
        /// <returns></returns>
        public int GetUpdateRate()
        {
            return m_UpdateRateTrackBar.Value * 1000;//* 60;  //分 --> 毫秒
        }

        private void QuitServerButton_Click(object sender, EventArgs eventArgs)
        {
            _serverController.Disconnect();
            _serverController.CloseView();
        }

        private void CaseSensitiveCheckBox_Click(object sender, EventArgs eventArgs)
        {

        }

        private void FilterTypeComboBox_TextChanged(object sender, EventArgs eventArgs)
        {

        }

        private void FilterValueTextBox_TextChanged(object sender, EventArgs eventArgs)
        {

        }

        private void ClearFilterButton_Click(object sender, EventArgs eventArgs)
        {

        }

        private void AddItemsButton_Click(object sender, EventArgs eventArgs)
        {

        }

        private void ItemsTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs eventArgs)
        {

        }

        private void ItemsDataGridView_DoubleClick(object sender, EventArgs eventArgs)
        {

        }

        private void ItemsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs eventArgs)
        {
            try
            {
                var s1 = m_ItemsDataGridView.Rows[eventArgs.RowIndex].Cells[0].Value.ToString();

                if (_itemsBackColorDictionary.TryGetValue(s1, out var backColor))
                {
                    m_ItemsDataGridView.Rows[eventArgs.RowIndex].DefaultCellStyle.BackColor = backColor;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void WriteSelectedItemsMenuItem_Click(object sender, EventArgs eventArgs)
        {
            if (m_ItemsDataGridView.SelectedRows.Count > 0)
            {
                SetValueFrame valueDlg = new SetValueFrame();
                valueDlg.ShowDialog();

                string valueToWrite = valueDlg.GetValue();

                if (!valueToWrite.Equals(""))
                {
                    List<string> l_ItemsId = new List<string>();

                    foreach (DataGridViewRow row in m_ItemsDataGridView.SelectedRows)
                    {
                        l_ItemsId.Add(row.Cells[0].Value.ToString());
                    }

                    try
                    {
                        _serverController.WriteItems(l_ItemsId, valueToWrite);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog errorLog = ErrorLog.GetInstance();
                        errorLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error during write items");

                        // TODO Display message
                    }
                }
            }
        }

        private void AcquitSelectedItemsMenuItem_Click(object sender, EventArgs eventArgs)
        {
            foreach (DataGridViewRow l_Row in m_ItemsDataGridView.SelectedRows)
            {
                if (l_Row.DefaultCellStyle.BackColor == System.Drawing.Color.LightGreen)
                {
                    string l_ID = l_Row.Cells[0].Value.ToString();

                    _itemsBackColorDictionary.Remove(l_ID);
                    _itemsBackColorDictionary.Add(l_ID, System.Drawing.Color.White);

                    l_Row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void m_UpdateRateTrackBar_ValueChanged(object sender, EventArgs e)
        {
            LabelUpdateValue.Text = m_UpdateRateTrackBar.Value + @" 分钟";

            var args = new UpdateRateEventArgs {StateUpdateRate = GetUpdateRate()};

            OnUpdateRate?.Invoke(this, args);
        }

        private void m_ItemsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }

        private void ServerTabUserControl_Load(object sender, EventArgs e)
        {
            LabelUpdateValue.Text = m_UpdateRateTrackBar.Value + @" 分钟";
        }
    }

    public class UpdateRateEventArgs
    {
        /// <summary>
        /// 数据更新间隔，单位毫秒
        /// </summary>
        public int StateUpdateRate { get; set; }
    }
}
