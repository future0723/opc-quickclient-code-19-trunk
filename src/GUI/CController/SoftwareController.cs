/*
 *  SoftwareController.cs
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

using System.Collections.Generic;
using System.Linq;

using GUI.CView;
using OPCLib;

namespace GUI.CController
{
    class SoftwareController : ISoftwareController
    {
        private const string ClientName = "OPC-QuickClient";
        private const string GroupName = "Group";

        private AbstractViewFactory m_Factory;
        private ISoftwareView m_SoftwareView;

        private List<IServerController> m_ServerControllerList;

        public SoftwareController(AbstractViewFactory p_Factory)
        {
            m_Factory = p_Factory;
            m_SoftwareView = m_Factory.CreateSoftwareView(this);

            m_ServerControllerList = new List<IServerController>();
        }

        public void ShowView()
        {
            m_SoftwareView.Display();
        }

        public void DisposeView()
        {
            m_SoftwareView.Shutdown();
        }

        public void CloseServerTabView(IServerView p_ServerView)
        {
            int l_Index = m_SoftwareView.CloseServerTabView(p_ServerView);

            m_ServerControllerList.RemoveAt(l_Index - 1);
        }

        public List<string[]> SearchOpcServers(string p_MachineName)
        {
            OpcServerLister l_ServerLister = new OpcServerLister();
            List<OpcServerInfo> l_GlobalServerList = new List<OpcServerInfo>();
            OpcServerInfo[] l_ServerListV1 = null;
            OpcServerInfo[] l_ServerListV2 = null;
            OpcServerInfo[] l_ServerListV3 = null;

            l_ServerLister.ListAllServersOnMachineV1(p_MachineName, out l_ServerListV1);
            l_ServerLister.ListAllServersOnMachineV2(p_MachineName, out l_ServerListV2);
            l_ServerLister.ListAllServersOnMachineV3(p_MachineName, out l_ServerListV3);

            if (l_ServerListV1 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV1);
            }
            if (l_ServerListV2 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV2);
            }
            if (l_ServerListV3 != null)
            {
                l_GlobalServerList.AddRange(l_ServerListV3);
            }

            return FromOPCServerInfoListToStringTabList(p_MachineName, l_GlobalServerList.Distinct()); 
        }

        public IServerView ConnectToOpcServer(string machineName, string serverId)
        {
            IServerController controller = new ServerController(this, m_Factory, machineName, serverId);

            controller.Connect(ClientName);
            controller.CreateGroup(GroupName);
            controller.BrowseServerAndAddItems();
            controller.ReadItems();
            controller.ActivateItems();

            m_ServerControllerList.Add(controller);

            return controller.GetView();
        }

        public void DisconnectFromAllOpcServers()
        {
            foreach (IServerController l_ServerController in m_ServerControllerList)
            {
                l_ServerController.Disconnect();
            }
        }

        private List<string[]> FromOPCServerInfoListToStringTabList(string machineName, IEnumerable<OpcServerInfo> serverList)
        {
            var result = new List<string[]>();

            foreach (OpcServerInfo server in serverList)
            {
                string[] l_ServerTabInfo = new string[4];

                l_ServerTabInfo[0] = machineName;
                l_ServerTabInfo[1] = server.ServerName;
                l_ServerTabInfo[2] = server.ProgID;
                l_ServerTabInfo[3] = server.ClsID.ToString().ToUpper();

                result.Add(l_ServerTabInfo);
            }

            return result;
        }
    }
}
