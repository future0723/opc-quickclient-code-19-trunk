/*
 *  ServerModel.cs
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

using GUI.CUtility;

namespace GUI.CModel
{
    class OpcItemException : Exception
    {
        private string m_Id;

        public OpcItemException(string p_Message, string p_Id) : base(p_Message)
        {
            m_Id = p_Id;
        }

        public string Id
        {
            get
            {
                return m_Id;
            }
        }
    }

    class ServerModel : IServerModel
    {
        public const string ItemPropIdKey = "ID";
        public const string ItemPropTypeKey = "TYPE";
        public const string ItemPropValueKey = "VALUE";
        public const string ItemPropQualityKey = "QUALITY";
        public const string ItemPropTimestampKey = "TIMESTAMP";

        public event ModelChangeEventHandler ModelChanged;

        private Dictionary<string, int> m_ItemIdToServerHandleDictionary;
        private Dictionary<int, IOPCItem> m_ClientHandleToItemDictionary;
        private Dictionary<int, int> m_ServerHandleToClientHandleDictionary;
        
        public ServerModel()
        {
            m_ItemIdToServerHandleDictionary = new Dictionary<string, int>();
            m_ClientHandleToItemDictionary = new Dictionary<int, IOPCItem>();
            m_ServerHandleToClientHandleDictionary = new Dictionary<int, int>();
        }

        public List<int> GetServerHandleList()
        {
            List<int> l_ServerHandleList = new List<int>();

            foreach (int l_Key in m_ServerHandleToClientHandleDictionary.Keys)
            {
                l_ServerHandleList.Add(l_Key);
            }

            return l_ServerHandleList;
        }

        public int GetServerHandleFromItemId(string p_ItemId)
        {
            int l_ServerHandle;

            if (!m_ItemIdToServerHandleDictionary.TryGetValue(p_ItemId, out l_ServerHandle))
            {
                throw new OpcItemException("The server handle doesn't exist in the model.", p_ItemId);
            }
            else
            {
                return l_ServerHandle;
            }
        }

        public int GetClientHandleFromServerHandle(int p_ServerHandle)
        {
            int l_ClientHandle;

            if (!m_ServerHandleToClientHandleDictionary.TryGetValue(p_ServerHandle, out l_ClientHandle))
            {
                throw new OpcItemException("The client handle doesn't exist in the model.", p_ServerHandle.ToString());
            }
            else
            {
                return l_ClientHandle;
            }
        }

        public IOPCItem GetItemFromClientHandle(int p_ClientHandle)
        {
            IOPCItem l_Item;

            if (!m_ClientHandleToItemDictionary.TryGetValue(p_ClientHandle, out l_Item))
            {
                throw new OpcItemException("The item doesn't exist in the model.", p_ClientHandle.ToString());
            }
            else
            {
                return l_Item;
            }
        }

        public void AddItem(int clientHandle, int serverHandle, Dictionary<string, string> properties)
        {
            if (!properties.ContainsKey(ItemPropIdKey) || !properties.ContainsKey(ItemPropTypeKey))
            {
                throw new OpcItemException("The item can't be added to the model.", clientHandle.ToString());
            }

            string value, quality,timestamp;
            properties.TryGetValue(ItemPropIdKey, out var id);
            properties.TryGetValue(ItemPropTypeKey, out var type);

            if (!properties.ContainsKey(ItemPropValueKey))
            {
                value = OpcUtility.ITEM_UNKNOWN;
            }
            else
            {
                properties.TryGetValue(ItemPropValueKey, out value);
            }

            if (!properties.ContainsKey(ItemPropQualityKey))
            {
                quality = OpcUtility.ITEM_UNKNOWN;
            }
            else
            {
                properties.TryGetValue(ItemPropQualityKey, out quality);
            }

            if (!properties.ContainsKey(ItemPropTimestampKey))
            {
                timestamp = OpcUtility.ITEM_UNKNOWN;
            }
            else
            {
                properties.TryGetValue(ItemPropTimestampKey, out timestamp);
            }

            IOPCItem item = new OPCItem(id, type)
            {
                Value = value,
                Quality = quality,
                Timestamp = timestamp
            };

            m_ItemIdToServerHandleDictionary.Add(item.ID, serverHandle);
            m_ClientHandleToItemDictionary.Add(clientHandle, item);
            m_ServerHandleToClientHandleDictionary.Add(serverHandle, clientHandle);

            var args = new ModelChangeEventArgs(ModelChangeEventType.Add, id, type, value, quality, timestamp);

            ModelChanged?.Invoke(this, args);
        }

        public void UpdateItem(int clientHandle, Dictionary<string, string> properties)
        {
            if (!properties.ContainsKey(ItemPropValueKey) ||
                !properties.ContainsKey(ItemPropQualityKey) ||
                !properties.ContainsKey(ItemPropTimestampKey))
            {
                throw new OpcItemException("The item can't be updated.", clientHandle.ToString());
            }

            if (!m_ClientHandleToItemDictionary.TryGetValue(clientHandle, out var item)) return;

            properties.TryGetValue(ItemPropValueKey, out var value);
            properties.TryGetValue(ItemPropQualityKey, out var quality);
            properties.TryGetValue(ItemPropTimestampKey, out var timestamp);

            item.Value = value;
            item.Quality = quality;
            item.Timestamp = timestamp;

            var eventArgs = new ModelChangeEventArgs(ModelChangeEventType.Update,
                item.ID, item.Type, value, quality, timestamp);

            ModelChanged?.Invoke(this, eventArgs);
        }
    }
}
