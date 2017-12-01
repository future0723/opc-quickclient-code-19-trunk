/*
 *  TaskDelegate.cs
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
using System.Reflection; 

using GUI.CErrorLog;

namespace GUI.CTaskDelegate
{
    public class TaskDelegate
    {
        public TaskDelegate(object p_Target, string p_MethodName, object[] p_Parameters)
        {
            try
            {
                Type l_Type = p_Target.GetType();
                MethodInfo l_MethodInfo = l_Type.GetMethod(p_MethodName);

                if (l_MethodInfo != null)
                {
                    l_MethodInfo.Invoke(p_Target, p_Parameters); 
                }
            }
            catch (Exception l_Ex)
            {
                ErrorLog l_ErrLog = ErrorLog.GetInstance();
                l_ErrLog.WriteToErrorLog(l_Ex.Message, l_Ex.StackTrace, "TaskDelegate");
            }
        }
    }
}
