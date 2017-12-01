/*
 *  COPCServer.cs
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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace OPCLib
{
    public struct OpcProperty
    {
        public int PropertyId;
        public string Description;
        public VarEnum DataType;

        public override string ToString()
        {
            return "ID:" + PropertyId + " '" + Description + "' T:" + DummyVariant.VarEnumToString(DataType);
        }
    }

    public struct OpcPropertyData
    {
        public int PropertyId;
        public int Error;
        public object Data;

        public override string ToString()
        {
            if (Error == HResults.S_OK)
            {
                return "ID:" + PropertyId + " Data:" + Data.ToString();
            }
            else
            {
                return "ID:" + PropertyId + " Error:" + Error.ToString();
            }
        }
    }

    public struct OpcPropertyItem
    {
        public int PropertyId;
        public int Error;
        public string NewItemId;

        public override string ToString()
        {
            if (Error == HResults.S_OK)
            {
                return "ID:" + PropertyId + " newID:" + NewItemId;
            }
            else
            {
                return "ID:" + PropertyId + " Error:" + Error.ToString();
            }
        }
    }

    public class ShutdownRequestEventArgs : EventArgs
    {
        private string m_ShutdownReason;

        public ShutdownRequestEventArgs(string p_ShutdownReason)
        {
            m_ShutdownReason = p_ShutdownReason;
        }

        public string ShutdownReason
        {
            get
            {
                return m_ShutdownReason;
            }
        }
    }

    public delegate void ShutdownRequestEventHandler(object sender, ShutdownRequestEventArgs eventArgs);

    [ComVisible(true)]
    public class OpcServer : IOPCShutdown
    {
        private object m_OPCServerObj = null;
        private IOPCServer m_IfServer = null;
        private IOPCCommon m_IfCommon = null;
        private IOPCBrowseServerAddressSpace m_IfBrowse = null;
        private IOPCItemProperties m_IfItmProps = null;

        private IConnectionPointContainer m_PointContainer = null;
        private IConnectionPoint m_ShutdownPoint = null;
        private int m_ShutdownCookie = 0;

        public event ShutdownRequestEventHandler ShutdownRequested = null;

        public OpcServer()
        {
        }

        ~OpcServer()
        {
            Disconnect();
        }

        public void LocalhostConnect(string p_ClsIDOPCServer)
        {
            Disconnect();

            Type l_TypeOfOPCServer = Type.GetTypeFromProgID(p_ClsIDOPCServer);
            if (l_TypeOfOPCServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.OPC_E_NOTFOUND);
            }

            m_OPCServerObj = Activator.CreateInstance(l_TypeOfOPCServer);
            m_IfServer = (IOPCServer) m_OPCServerObj;
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.CONNECT_E_NOCONNECTION);
            }

            // Connect all interfaces
            m_IfCommon = (IOPCCommon) m_OPCServerObj;
            m_IfBrowse = (IOPCBrowseServerAddressSpace) m_IfServer;
            m_IfItmProps = (IOPCItemProperties) m_IfServer;
            m_PointContainer = (IConnectionPointContainer) m_OPCServerObj;
            AdviseIOPCShutdown();
        }

        public void RemoteConnect(string p_ClsIDOPCServer, string p_MachineName)
        {
            Disconnect();

            Type l_TypeOfOPCServer = Type.GetTypeFromProgID(p_ClsIDOPCServer, p_MachineName);
            if (l_TypeOfOPCServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.OPC_E_NOTFOUND);
            }

            m_OPCServerObj = Activator.CreateInstance(l_TypeOfOPCServer);
            m_IfServer = (IOPCServer) m_OPCServerObj;
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.CONNECT_E_NOCONNECTION);
            }

            // Connect all interfaces
            m_IfCommon = (IOPCCommon) m_OPCServerObj;
            m_IfBrowse = (IOPCBrowseServerAddressSpace) m_IfServer;
            m_IfItmProps = (IOPCItemProperties) m_IfServer;
            m_PointContainer = (IConnectionPointContainer) m_OPCServerObj;
            AdviseIOPCShutdown();
        }

        public void Disconnect()
        {
            if (m_ShutdownPoint != null)
            {
                if (m_ShutdownCookie != 0)
                {
                    m_ShutdownPoint.Unadvise(m_ShutdownCookie);
                    m_ShutdownCookie = 0;
                }
                Marshal.ReleaseComObject(m_ShutdownPoint);
                m_ShutdownPoint = null;
            }

            m_PointContainer = null;
            m_IfBrowse = null;
            m_IfItmProps = null;
            m_IfCommon = null;
            m_IfServer = null;

            if (m_OPCServerObj != null)
            {
                Marshal.ReleaseComObject(m_OPCServerObj);
                m_OPCServerObj = null;
            }
        }

        public void GetStatus(out ServerStatus p_ServerStatus)
        {
            m_IfServer.GetStatus(out p_ServerStatus);
        }

        public string GetErrorString(int p_ErrorCode, int p_LocaleID)
        {
            string l_ErrorRes;

            m_IfServer.GetErrorString(p_ErrorCode, p_LocaleID, out l_ErrorRes);

            return l_ErrorRes;
        }

        public OpcGroup AddGroup(string groupName, bool setActive, int requestedUpdateRate)
        {
            return AddGroup(groupName, setActive, requestedUpdateRate, null, null, 0);
        }

        public OpcGroup AddGroup(string groupName, bool setActive, int requestedUpdateRate,
            int[] biasTime, float[] percentDeadband, int localeId)
        {
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            var group = new OpcGroup(ref m_IfServer, false, groupName, setActive, requestedUpdateRate);

            group.InternalAdd(biasTime, percentDeadband, localeId);

            return group;
        }

        public OpcGroup GetPublicGroup(string p_GroupName)
        {
            if (m_IfServer == null)
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            OpcGroup l_Grp = new OpcGroup(ref m_IfServer, true, p_GroupName, false, 1000);
            l_Grp.InternalAdd(null, null, 0);

            return l_Grp;
        }

        public void SetLocaleID(int p_LocalID)
        {
            m_IfCommon.SetLocaleID(p_LocalID);
        }

        public void GetLocaleID(out int p_LocalID)
        {
            m_IfCommon.GetLocaleID(out p_LocalID);
        }

        public void QueryAvailableLocaleIDs(out int[] p_LocalIDs)
        {
            p_LocalIDs = null;
            int l_Count;
            IntPtr l_PtrIDs;
            int l_HResult = m_IfCommon.QueryAvailableLocaleIDs(out l_Count, out l_PtrIDs);

            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }
            if (((int) l_PtrIDs) == 0)
            {
                return;
            }
            if (l_Count < 1)
            {
                Marshal.FreeCoTaskMem(l_PtrIDs);
                return;
            }

            p_LocalIDs = new int[l_Count];
            Marshal.Copy(l_PtrIDs, p_LocalIDs, 0, l_Count);
            Marshal.FreeCoTaskMem(l_PtrIDs);
        }

        public void SetClientName(string p_Name)
        {
            m_IfCommon.SetClientName(p_Name);
        }

        public OpcNamespaceType QueryOrganization()
        {
            OpcNamespaceType l_NamespaceType;

            m_IfBrowse.QueryOrganization(out l_NamespaceType);

            return l_NamespaceType;
        }

        public void ChangeBrowsePosition(OpcBrowseDirection p_Direction, string p_Name)
        {
            m_IfBrowse.ChangeBrowsePosition(p_Direction, p_Name);
        }

        public void BrowseOPCItemIDs(OpcBrowseType p_FilterType, string p_FilterCriteria,
            VarEnum p_DataTypeFilter, OpcAccessRights p_AccessRightsFilter, out UCOMIEnumString p_StringEnumerator)
        {
            object l_EnumTemp;

            m_IfBrowse.BrowseOPCItemIDs(p_FilterType, p_FilterCriteria, (short) p_DataTypeFilter, p_AccessRightsFilter, out l_EnumTemp);
            p_StringEnumerator = (UCOMIEnumString) l_EnumTemp;
        }

        public string GetItemID(string p_ItemDataID)
        {
            string l_ItemID;

            m_IfBrowse.GetItemID(p_ItemDataID, out l_ItemID);

            return l_ItemID;
        }

        public void BrowseAccessPaths(string p_ItemID, out UCOMIEnumString p_StringEnumerator)
        {
            object l_EnumTemp;

            m_IfBrowse.BrowseAccessPaths(p_ItemID, out l_EnumTemp);
            p_StringEnumerator = (UCOMIEnumString) l_EnumTemp;
        }

        public void Browse(OpcBrowseType p_Type, out ArrayList p_Lst)
        {
            p_Lst = null;
            UCOMIEnumString l_Enumerator;

            BrowseOPCItemIDs(p_Type, "", VarEnum.VT_EMPTY, 0, out l_Enumerator);
            if (l_Enumerator == null)
            {
                return;
            }

            p_Lst = new ArrayList(500);
            int l_Cft;
            string[] l_StrF = new string[100];
            int l_HResult;
            do
            {
                l_Cft = 0;
                l_HResult = l_Enumerator.Next(100, l_StrF, out l_Cft);
                if (l_Cft > 0)
                {
                    for (int i = 0; i < l_Cft; ++i)
                    {
                        p_Lst.Add(l_StrF[i]);
                    }
                }
            }
            while (l_HResult == HResults.S_OK);

            Marshal.ReleaseComObject(l_Enumerator);
            l_Enumerator = null;
            p_Lst.TrimToSize();
        }

        public void QueryAvailableProperties(string p_ItemID, out OpcProperty[] p_OPCProperties)
        {
            p_OPCProperties = null;
            int l_Count = 0;
            IntPtr l_PtrID;
            IntPtr l_PtrDesc;
            IntPtr l_PtrTyp;

            m_IfItmProps.QueryAvailableProperties(p_ItemID, out l_Count, out l_PtrID, out l_PtrDesc, out l_PtrTyp);
            if ((l_Count == 0) || (l_Count > 10000))
            {
                return;
            }

            int l_RunID = (int) l_PtrID;
            int l_RunDesc = (int) l_PtrDesc;
            int l_RunTyp = (int) l_PtrTyp;
            if ((l_RunID == 0) || (l_RunDesc == 0) || (l_RunTyp == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_OPCProperties = new OpcProperty[l_Count];

            IntPtr l_PtrString;
            for (int i = 0; i < l_Count; ++i)
            {
                p_OPCProperties[i] = new OpcProperty();

                p_OPCProperties[i].PropertyId = Marshal.ReadInt32((IntPtr) l_RunID);
                l_RunID += 4;

                l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) l_RunDesc);
                l_RunDesc += 4;
                p_OPCProperties[i].Description = Marshal.PtrToStringUni(l_PtrString);
                Marshal.FreeCoTaskMem(l_PtrString);

                p_OPCProperties[i].DataType = (VarEnum) Marshal.ReadInt16((IntPtr) l_RunTyp);
                l_RunTyp += 2;
            }

            Marshal.FreeCoTaskMem(l_PtrID);
            Marshal.FreeCoTaskMem(l_PtrDesc);
            Marshal.FreeCoTaskMem(l_PtrTyp);
        }

        public bool GetItemProperties(string p_ItemID, int[] p_PropertyIDs, out OpcPropertyData[] p_PropertiesData)
        {
            p_PropertiesData = null;
            int l_Count = p_PropertyIDs.Length;
            if (l_Count < 1)
            {
                return false;
            }

            IntPtr l_PtrDat;
            IntPtr l_PtrErr;
            int l_HResult = m_IfItmProps.GetItemProperties(p_ItemID, l_Count, p_PropertyIDs, out l_PtrDat, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunDat = (int) l_PtrDat;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunDat == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_PropertiesData = new OpcPropertyData[l_Count];

            for (int i = 0; i < l_Count; ++i)
            {
                p_PropertiesData[i] = new OpcPropertyData();
                p_PropertiesData[i].PropertyId = p_PropertyIDs[i];

                p_PropertiesData[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                l_RunErr += 4;

                if (p_PropertiesData[i].Error == HResults.S_OK)
                {
                    p_PropertiesData[i].Data = Marshal.GetObjectForNativeVariant((IntPtr) l_RunDat);
                    DummyVariant.VariantClear((IntPtr) l_RunDat);
                }
                else
                {
                    p_PropertiesData[i].Data = null;
                }

                l_RunDat += DummyVariant.CONST_SIZE;
            }

            Marshal.FreeCoTaskMem(l_PtrDat);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        public bool LookupItemIDs(string p_ItemID, int[] p_PropertyIDs, out OpcPropertyItem[] p_PropertyItems)
        {
            p_PropertyItems = null;
            int l_Count = p_PropertyIDs.Length;
            if (l_Count < 1)
            {
                return false;
            }

            IntPtr l_PtrErr;
            IntPtr l_PtrIDs;
            int l_HResult = m_IfItmProps.LookupItemIDs(p_ItemID, l_Count, p_PropertyIDs, out l_PtrIDs, out l_PtrErr);
            if (HResults.Failed(l_HResult))
            {
                Marshal.ThrowExceptionForHR(l_HResult);
            }

            int l_RunIDs = (int) l_PtrIDs;
            int l_RunErr = (int) l_PtrErr;
            if ((l_RunIDs == 0) || (l_RunErr == 0))
            {
                Marshal.ThrowExceptionForHR(HResults.E_ABORT);
            }

            p_PropertyItems = new OpcPropertyItem[l_Count];

            IntPtr l_PtrString;
            for (int i = 0; i < l_Count; ++i)
            {
                p_PropertyItems[i] = new OpcPropertyItem();
                p_PropertyItems[i].PropertyId = p_PropertyIDs[i];

                p_PropertyItems[i].Error = Marshal.ReadInt32((IntPtr) l_RunErr);
                l_RunErr += 4;

                if (p_PropertyItems[i].Error == HResults.S_OK)
                {
                    l_PtrString = (IntPtr) Marshal.ReadInt32((IntPtr) l_RunIDs);
                    p_PropertyItems[i].NewItemId = Marshal.PtrToStringUni(l_PtrString);
                    Marshal.FreeCoTaskMem(l_PtrString);
                }
                else
                {
                    p_PropertyItems[i].NewItemId = null;
                }

                l_RunIDs += 4;
            }

            Marshal.FreeCoTaskMem(l_PtrIDs);
            Marshal.FreeCoTaskMem(l_PtrErr);

            return l_HResult == HResults.S_OK;
        }

        void IOPCShutdown.ShutdownRequest(string p_ShutdownReason)
        {
            ShutdownRequestEventArgs l_Event = new ShutdownRequestEventArgs(p_ShutdownReason);
            if (ShutdownRequested != null)
            {
                ShutdownRequested(this, l_Event);
            }
        }

        private void AdviseIOPCShutdown()
        {
            Type l_SinkType = typeof(IOPCShutdown);
            Guid l_SinkGuid = l_SinkType.GUID;

            m_PointContainer.FindConnectionPoint(ref l_SinkGuid, out m_ShutdownPoint);
            if (m_ShutdownPoint == null)
            {
                return;
            }

            m_ShutdownPoint.Advise(this, out m_ShutdownCookie);
        }
    }
}
