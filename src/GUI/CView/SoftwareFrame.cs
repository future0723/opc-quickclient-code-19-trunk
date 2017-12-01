/*
 *  SoftwareFrame.cs
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
using System.ComponentModel;
using System.Windows.Forms;

using GUI.CController;
using GUI.CErrorLog;

namespace GUI.CView
{
    partial class SoftwareFrame : Form, ISoftwareView
    {
        private readonly ISoftwareController _softwareController;

        private string _currentActionDescription;
        private int _currentActionErrorCount;
        private bool _isActionRunning;
        private string SelectedMachineName { get; set; }
        private string SelectedServerId { get; set; }


        private UpdateServerListDelegate _updateServerListDelegate;
        private delegate void UpdateServerListDelegate(ListView listView, string[] serverInfo);

        private UpdateTabListDelegate _updateTabListDelegate;
        private delegate void UpdateTabListDelegate(TabControl tabControl, TabPage serverTab);

        private UpdateReportDelegate _updateReportDelegate;
        private delegate void UpdateReportDelegate(RichTextBox textBox, string message);

        private UpdateStatusDelegate _updateStatusDelegate;
        private delegate void UpdateStatusDelegate(ToolStripStatusLabel statusLabel, string message);

        public SoftwareFrame(ISoftwareController softwareController)
        {
            InitializeComponent();

            _softwareController = softwareController;
        }

        public void Display()
        {
            Application.Run(this);
        }

        public void Shutdown()
        {
            Dispose();
        }

        public int CloseServerTabView(IServerView serverView)
        {
            int index = m_ServersTabControl.TabPages.IndexOf(serverView.GetTabPage());

            m_ServersTabControl.SelectedIndex = m_ServersTabControl.SelectedIndex - 1;
            m_ServersTabControl.TabPages.Remove(serverView.GetTabPage());

            return index;
        }

        private void UpdateServerListFunc(ListView listView, string[] serverInfo)
        {
            listView.Items.Add(new ListViewItem(serverInfo));
        }

        private void UpdateTabListFunc(TabControl tabControl, TabPage serverTab)
        {
            tabControl.Controls.Add(serverTab);
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
        }

        private void UpdateReportFunc(RichTextBox textBox, string message)
        {
            textBox.AppendText(message);
            textBox.ScrollToCaret();
        }

        private void UpdateStatusFunc(ToolStripStatusLabel statusLabel, string message)
        {
            statusLabel.Text = message;
        }

        private void UpdateServerListView(string[] serverInfo)
        {
            Invoke(_updateServerListDelegate, new object[] { ServerList, serverInfo });
        }

        private void UpdateServerTabList(TabPage serverTab)
        {
            Invoke(_updateTabListDelegate, m_ServersTabControl, serverTab);
        }

        private void DisplayMessageInReport(string message)
        {
            Invoke(_updateReportDelegate, new object[] { m_ConnectionReport, message });
        }

        private void DisplayMessageInActionStatus(string message)
        {
            Invoke(_updateStatusDelegate, new object[] { m_ActionStatusLabel, message });
        }

        private void DisplayMessageInStateStatus(string message)
        {
            Invoke(_updateStatusDelegate, new object[] { m_StateStatusLabel, message });
        }

        /// <summary>
        /// 连接到OPCServer
        /// </summary>
        private void ConnectToOpcServer()
        {
            if (ServerList.SelectedItems.Count > 0)
            {
                if (_isActionRunning)
                {
                    MessageBox.Show(this,@"An action is already running. You have to wait until it is finished.", @"Action on OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var selectedItem = ServerList.SelectedItems[0];
                    SelectedMachineName = selectedItem.SubItems[0].Text;
                    SelectedServerId = selectedItem.SubItems[2].Text;

                    m_ConnectSrvBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void SoftwareFrame_Load(object sender, EventArgs eventArgs)
        {
            _currentActionDescription = "OPC-QuickClient load";
            _currentActionErrorCount = 0;
            _isActionRunning = false;
            SelectedMachineName = "";
            SelectedServerId = "";

            _updateServerListDelegate = UpdateServerListFunc;
            _updateTabListDelegate = UpdateTabListFunc;
            _updateReportDelegate = UpdateReportFunc;
            _updateStatusDelegate = UpdateStatusFunc;

            m_LocalSearchCheckBox.Checked = true;
            m_RemoteSearchCheckBox.Checked = false;

            m_RemoteMachineText.Text = "";
            m_RemoteMachineText.Enabled = false;

            DisplayMessageInActionStatus(_currentActionDescription + " finished");
            DisplayMessageInStateStatus(Convert.ToString(_currentActionErrorCount) + " error(s)");
        }

        private void SoftwareFrame_FormClosed(object sender, FormClosedEventArgs eventArgs)
        {
            _softwareController.DisconnectFromAllOpcServers();
        }

        private void OpenMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        private void SaveMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        private void SaveAsMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        private void QuitMenuItem_Click(object sender, EventArgs eventArgs)
        {
            _softwareController.DisposeView();
            _softwareController.DisconnectFromAllOpcServers();
        }

        private void ListServersButton_Click(object sender, EventArgs eventArgs)
        {
            if (m_RemoteSearchCheckBox.Checked && (m_RemoteMachineText.Text == ""))
            {
                MessageBox.Show(this, @"You have to specify an IP address or a machine name.", @"Search of OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_isActionRunning)
                {
                    MessageBox.Show(this, @"An action is already running. You have to wait until it is finished.", @"Action on OPC servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ServerList.Items.Clear();
                    m_ListSrvBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void ListSrvBackgroundWorker_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            _isActionRunning = true;
            m_ListSrvBackgroundWorker.ReportProgress(0);

            try
            {
                List<string[]> serverList;

                _currentActionErrorCount = 0;
                m_ListSrvBackgroundWorker.ReportProgress(50);

                if (m_LocalSearchCheckBox.Checked)
                {
                    _currentActionDescription = "Local search of OPC servers";

                    DisplayMessageInReport("Search of local OPC servers\n");
                    DisplayMessageInActionStatus(_currentActionDescription);
                    DisplayMessageInStateStatus("Running");

                    serverList = _softwareController.SearchOpcServers("Localhost");
                }
                else
                {
                    string remoteMachineName = m_RemoteMachineText.Text;
                    _currentActionDescription = "Remote search of OPC servers";
                    
                    DisplayMessageInReport("Search of remote OPC servers on the machine : " + remoteMachineName + "\n");
                    DisplayMessageInActionStatus(_currentActionDescription);
                    DisplayMessageInStateStatus("Running");
                    
                    serverList = _softwareController.SearchOpcServers(remoteMachineName);
                }

                if (serverList != null)
                {
                    foreach (string[] server in serverList)
                    {
                        UpdateServerListView(server);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = ErrorLog.GetInstance();
                errorLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error during OPC server search");
                
                _currentActionErrorCount++;

                DisplayMessageInReport(ex.Message + "\n");
            }

            m_ListSrvBackgroundWorker.ReportProgress(100);
        }

        /// <summary>
        /// m_ConnectSrvBackgroundWorker.RunWorkerAsync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ConnectSrvBackgroundWorker_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            _isActionRunning = true;
            m_ConnectSrvBackgroundWorker.ReportProgress(0);
            _currentActionErrorCount = 0;

            _currentActionDescription = "Connection to an OPC server";

            DisplayMessageInReport("Connection to the server : " + SelectedServerId + "\n");
            DisplayMessageInActionStatus(_currentActionDescription);
            DisplayMessageInStateStatus("Running");

            try
            {
                m_ConnectSrvBackgroundWorker.ReportProgress(50);

                IServerView serverView = _softwareController.ConnectToOpcServer(SelectedMachineName, SelectedServerId);
                UpdateServerTabList(serverView.GetTabPage());
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = ErrorLog.GetInstance();
                errorLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error during OPC server connection");
                
                _currentActionErrorCount++;

                DisplayMessageInReport(ex.Message + "\n");
            }

            m_ConnectSrvBackgroundWorker.ReportProgress(100);
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs eventArgs)
        {
            m_CurrentActionProgressBar.Value = eventArgs.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            DisplayMessageInActionStatus(_currentActionDescription + " finished");
            DisplayMessageInStateStatus(Convert.ToString(_currentActionErrorCount) + " error(s)");

            _isActionRunning = false;
        }

        private void LocalSearchCheckBox_Click(object sender, EventArgs eventArgs)
        {
            m_LocalSearchCheckBox.Checked = true;
            m_RemoteSearchCheckBox.Checked = false;
            
            m_RemoteMachineText.Enabled = false;
        }

        private void RemoteSearchCheckBox_Click(object sender, EventArgs eventArgs)
        {
            m_RemoteSearchCheckBox.Checked = true;
            m_LocalSearchCheckBox.Checked = false;

            m_RemoteMachineText.Enabled = true;
        }

        private void QuitButton_Click(object sender, EventArgs eventArgs)
        {
            _softwareController.DisposeView();
            _softwareController.DisconnectFromAllOpcServers();
        }

        private void ServerList_DoubleClick(object sender, EventArgs eventArgs)
        {
            ConnectToOpcServer();
        }

        private void ConnectToMenuItem_Click(object sender, EventArgs eventArgs)
        {
            ConnectToOpcServer();
        }

        private void LoadLogMenuItem_Click(object sender, EventArgs eventArgs)
        {
            string l_ErrorLogContent;
            ErrorLog l_ErrorLog = ErrorLog.GetInstance();

            l_ErrorLogContent = l_ErrorLog.ReadErrorLog();

            if (l_ErrorLogContent == "")
            {
                DisplayMessageInReport("The error log is empty\n");
            }
            else
            {
                DisplayMessageInReport("\n\n" + l_ErrorLogContent);
            }
        }

        private void ClearLogMenuItem_Click(object sender, EventArgs eventArgs)
        {
            var errorLog = ErrorLog.GetInstance();
            errorLog.ClearErrorLog();
        }

        private void ClearEntriesMenuItem_Click(object sender, EventArgs eventArgs)
        {
            m_ConnectionReport.Clear();
        }
    }
}
