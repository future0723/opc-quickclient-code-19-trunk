/*
 *  WindowsFormViewFactory.cs
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

using GUI.CController;

namespace GUI.CView
{
    class WindowsFormViewFactory : AbstractViewFactory
    {
        private static WindowsFormViewFactory m_Instance = null;

        private WindowsFormViewFactory()
        {
        }

        public static WindowsFormViewFactory GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new WindowsFormViewFactory();
            }
            return m_Instance;
        }

        public override ISoftwareView CreateSoftwareView(ISoftwareController p_SoftwareController)
        {
            return new SoftwareFrame(p_SoftwareController);
        }

        public override IServerView CreateServerView(IServerController p_ServerController, string p_ServerName)
        {
            return new ServerTabUserControl(p_ServerController, p_ServerName);
        }
    }
}
