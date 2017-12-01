/*
 *  ModelChangeEvent.cs
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

namespace GUI.CModel
{
    public enum ModelChangeEventType
    {
        Add, Update
    }

    public class ModelChangeEventArgs : EventArgs
    {
        public ModelChangeEventArgs(ModelChangeEventType eventArgsType, string id, string type,
            string value, string quality, string timestamp)
        {
            EventType = eventArgsType;

            Id = id;
            Type = type;
            Value = value;
            Quality = quality;
            Timestamp = timestamp;
        }

        public ModelChangeEventArgs(ModelChangeEventType eventArgsType,  List<IOPCItem> opcItemList)
        {
            EventType = eventArgsType;

            OpcItemList = opcItemList;
        }

        public List<IOPCItem> OpcItemList { get; }
        public ModelChangeEventType EventType { get; }

        public string Id { get; }

        public string Type { get; }

        public string Value { get; }

        public string Quality { get; }

        public string Timestamp { get; }
    }

    public delegate void ModelChangeEventHandler(object sender, ModelChangeEventArgs eventArgs);
}
