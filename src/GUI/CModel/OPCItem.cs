/*
 *  OPCItem.cs
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

namespace GUI.CModel
{
    class OPCItem : IOPCItem
    {
        private string m_ID;
        private string m_Type;
        private string m_Value;
        private string m_Quality;
        private string m_Timestamp;

        public OPCItem(string p_ID, string p_Type)
        {
            m_ID = p_ID;
            m_Type = p_Type;
        }

        public string ID
        {
            get
            {
                return m_ID;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }
        }

        public string Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        public string Quality
        {
            get
            {
                return m_Quality;
            }
            set
            {
                m_Quality = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return m_Timestamp;
            }
            set
            {
                m_Timestamp = value;
            }
        }
    }
}
