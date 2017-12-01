/*
 *  ServerController.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

using GUI.CView;
using GUI.CModel;
using GUI.CErrorLog;
using GUI.CUtility;
using OPCLib;

namespace GUI.CController
{
    class ServerController : IServerController
    {
        private readonly ISoftwareController _softwareController;

        private readonly IServerModel _serverModel;
        private readonly IServerView _serverView;

        private readonly string _machineName;
        private readonly string _serverId;

        private readonly Mutex _itemAccessMutex;
        private OpcServer _server;
        private OpcGroup _group;

        public ServerController(ISoftwareController softwareController, 
                                AbstractViewFactory factory, 
                                string machineName, 
                                string serverId)
        {
            _softwareController = softwareController;

            _serverView = factory.CreateServerView(this, serverId);

            if (_serverView is ServerTabUserControl serverTabUserControl)
                serverTabUserControl.OnUpdateRate += (sender, args) =>
                {
                    Console.WriteLine(@"The new update rage is {0} millisecond", args.StateUpdateRate);
                    _group.UpdateRate = args.StateUpdateRate;
                };

            _serverModel = new ServerModel();
            _serverModel.ModelChanged += _serverView.ServerModelChange;


            _machineName = machineName;
            _serverId = serverId;

            _itemAccessMutex = new Mutex();
            _server = null;
            _group = null;
        }

        public IServerView GetView()
        {
            return _serverView;
        }

        public void CloseView()
        {
            _softwareController.CloseServerTabView(_serverView);
        }

        public void Connect(string p_ClientName)
        {
            _server = new OpcServer();

            _server.RemoteConnect(_serverId, _machineName);

            try
            {
                _server.SetClientName(p_ClientName);

                _server.ShutdownRequested += new ShutdownRequestEventHandler(Server_ShutdownRequest);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
        }

        public void Disconnect()
        {
            List<int> l_ServerHandleList = _serverModel.GetServerHandleList();

            if (l_ServerHandleList.Count > 0)
            {
                try
                {
                    int[] l_RemoveRes;
                    _group.RemoveItems(l_ServerHandleList.ToArray(), out l_RemoveRes);
                }
                catch (Exception)
                {
                }
            }

            if (_group != null)
            {
                _group.DataChanged -= new DataChangeEventHandler(Group_DataChange);

                try
                {
                    _group.Remove(true);
                }
                catch (Exception)
                {
                }
            }

            if (_server != null)
            {
                _server.ShutdownRequested -= new ShutdownRequestEventHandler(Server_ShutdownRequest);

                try
                {
                    _server.Disconnect();
                }
                catch (Exception)
                {
                }
            }
        }

        public void CreateGroup(string groupName)
        {
            try
            {
                _group = _server.AddGroup(groupName, true, _serverView.GetUpdateRate());
                _group.DataChanged += Group_DataChange;
            }
            catch (Exception ex)
            {
                Disconnect();

                throw ex;
            }
        }

        public void BrowseServerAndAddItems()
        {
            _itemAccessMutex.WaitOne();

            try
            {
                List<string> l_ItemIds = new List<string>();
                OpcNamespaceType l_ServerOrganization = _server.QueryOrganization();

                // Browse to root
                _server.ChangeBrowsePosition(OpcBrowseDirection.OPC_BROWSE_TO, "");

                if (l_ServerOrganization == OpcNamespaceType.OPC_NS_HIERARCHIAL)
                {
                    RecursiveServerHierarchialBrowse(ref l_ItemIds);
                }
                else
                {
                    RecursiveServerFlatBrowse(ref l_ItemIds);
                }

                ValidateItemIds(ref l_ItemIds);
                AddItems(l_ItemIds);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                _itemAccessMutex.ReleaseMutex();
            }
        }

        public void ReadItems()
        {
            _itemAccessMutex.WaitOne();

            try
            {
                int l_Index = 0;
                OpcItemState[] l_ItemStateArray;
                List<int> l_ServerHandleList = _serverModel.GetServerHandleList();

                _group.Read(OpcDataSource.OPC_DS_DEVICE, l_ServerHandleList.ToArray(), out l_ItemStateArray);

                foreach (int l_ServerHandle in l_ServerHandleList)
                {
                    if (l_ItemStateArray[l_Index].Error == HResults.S_OK)
                    {
                        int l_ClientHandle = _serverModel.GetClientHandleFromServerHandle(l_ServerHandle);
                        Dictionary<string, string> l_ItemProps = new Dictionary<string, string>();
                        IOPCItem l_Item = _serverModel.GetItemFromClientHandle(l_ClientHandle);

                        l_ItemProps.Add(CModel.ServerModel.ItemPropValueKey, OpcUtility.ValueToString(l_Item.Type, l_ItemStateArray[l_Index].DataValue));
                        l_ItemProps.Add(CModel.ServerModel.ItemPropQualityKey, OpcUtility.QualityToString(l_ItemStateArray[l_Index].Quality));
                        l_ItemProps.Add(CModel.ServerModel.ItemPropTimestampKey, OpcUtility.TimeStampToString(l_ItemStateArray[l_Index].Timestamp));

                        _serverModel.UpdateItem(l_ClientHandle, l_ItemProps);
                    }

                    l_Index++;
                }
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                _itemAccessMutex.ReleaseMutex();
            }
        }

        public void ActivateItems()
        {
            _itemAccessMutex.WaitOne();

            try
            {
                int[] l_SetActiveResult;
                _group.SetActiveState(_serverModel.GetServerHandleList().ToArray(), true, out l_SetActiveResult);
            }
            catch (Exception l_Ex)
            {
                Disconnect();

                throw l_Ex;
            }
            finally
            {
                _itemAccessMutex.ReleaseMutex();
            }
        }

        public void WriteItems(List<string> p_ItemsId, string p_Value)
        {
            _itemAccessMutex.WaitOne();

            try
            {
                int l_ItemsToWriteCount = p_ItemsId.Count;

                int[] l_ServerHandleArray = new int[l_ItemsToWriteCount];
                object[] l_ValueArray = new object[l_ItemsToWriteCount];
                int[] l_ErrorArray;
                
                for (int i = 0; i < l_ItemsToWriteCount; ++i)
                {
                    l_ServerHandleArray[i] = _serverModel.GetServerHandleFromItemId(p_ItemsId[i]);

                    int l_ClientHandle = _serverModel.GetClientHandleFromServerHandle(l_ServerHandleArray[i]);
                    IOPCItem l_Item = _serverModel.GetItemFromClientHandle(l_ClientHandle);
                    l_ValueArray[i] = OpcUtility.StringToValue(l_Item.Type, p_Value);
                }
                _group.Write(l_ServerHandleArray, l_ValueArray, out l_ErrorArray);

                // TODO Check the error
            }
            catch (Exception l_Ex)
            {
                throw l_Ex;
            }
            finally
            {
                _itemAccessMutex.ReleaseMutex();
            }
        }

        private void Server_ShutdownRequest(object sender, ShutdownRequestEventArgs eventArgs)
        {
            Disconnect();

            // TODO Display message and close server tab
        }

        private void Group_DataChange(object sender, DataChangeEventArgs eventArgs)
        {
            _itemAccessMutex.WaitOne();

            for (int i = 0; i < eventArgs.Status.Length; ++i)
            {
                try
                {
                    var itemProps = new Dictionary<string, string>();
                    var item = _serverModel.GetItemFromClientHandle(eventArgs.Status[i].HandleClient);

                    itemProps.Add(ServerModel.ItemPropValueKey, OpcUtility.ValueToString(item.Type, eventArgs.Status[i].DataValue));
                    itemProps.Add(ServerModel.ItemPropQualityKey, OpcUtility.QualityToString(eventArgs.Status[i].Quality));
                    itemProps.Add(ServerModel.ItemPropTimestampKey, OpcUtility.TimeStampToString(eventArgs.Status[i].Timestamp));

                    _serverModel.UpdateItem(eventArgs.Status[i].HandleClient, itemProps);
                }
                catch (Exception ex)
                {
                    ErrorLog errorLog = ErrorLog.GetInstance();
                    errorLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error during OPC group update");
                }
            }

            _itemAccessMutex.ReleaseMutex();
        }

        private void RecursiveServerHierarchialBrowse(ref List<string> p_ItemIds)
        {
            ArrayList l_ItemList;
            _server.Browse(OpcBrowseType.OPC_LEAF, out l_ItemList);
            foreach (string l_ItemName in l_ItemList)
            {
                p_ItemIds.Add(_server.GetItemID(l_ItemName));
            }

            ArrayList l_BranchList;
            _server.Browse(OpcBrowseType.OPC_BRANCH, out l_BranchList);
            foreach (string l_BranchName in l_BranchList)
            {
                _server.ChangeBrowsePosition(OpcBrowseDirection.OPC_BROWSE_DOWN, l_BranchName);
                RecursiveServerHierarchialBrowse(ref p_ItemIds);
                _server.ChangeBrowsePosition(OpcBrowseDirection.OPC_BROWSE_UP, "");
            }
        }

        private void RecursiveServerFlatBrowse(ref List<string> p_ItemIds)
        {
            // TODO
        }

        private void ValidateItemIds(ref List<string> p_ItemIds)
        {
            List<string> l_ItemIds = new List<string>(p_ItemIds);
            
            foreach (string l_ItemName in l_ItemIds)
            {
                OpcItemResult[] l_ItemResult;
                OpcItemDef[] l_ItemDef = new OpcItemDef[1];

                l_ItemDef[0] = new OpcItemDef(l_ItemName, true, 0, VarEnum.VT_BSTR);

                if (!_group.ValidateItems(l_ItemDef, false, out l_ItemResult))
                {
                    p_ItemIds.Remove(l_ItemName);
                }
            }
        }

        private void AddItems(List<string> p_ItemIds)
        {
            int l_ItemClientHandle = 0;
            OpcItemDef[] l_ItemDef = new OpcItemDef[p_ItemIds.Count];
            OpcItemResult[] l_AddRes;

            foreach (string l_ItemName in p_ItemIds)
            {
                l_ItemDef[l_ItemClientHandle] = new OpcItemDef(l_ItemName, false, l_ItemClientHandle, VarEnum.VT_BSTR);
                l_ItemClientHandle++;
            }

            _group.AddItems(l_ItemDef, out l_AddRes);

            l_ItemClientHandle = 0;
            foreach (string l_ItemName in p_ItemIds)
            {
                if (l_AddRes[l_ItemClientHandle].Error == HResults.S_OK)
                {
                    Dictionary<string, string> l_ItemProps = new Dictionary<string, string>();
                    l_ItemProps.Add(CModel.ServerModel.ItemPropIdKey, _server.GetItemID(l_ItemName));
                    l_ItemProps.Add(CModel.ServerModel.ItemPropTypeKey, OpcUtility.TypeToString((int)l_AddRes[l_ItemClientHandle].CanonicalDataType));

                    _serverModel.AddItem(l_ItemClientHandle, l_AddRes[l_ItemClientHandle].HandleServer, l_ItemProps);
                }

                l_ItemClientHandle++;
            }
        }
    }
}
