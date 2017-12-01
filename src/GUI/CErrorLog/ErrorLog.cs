/*
 *  ErrorLog.cs
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
using System.IO;
using System.Windows.Forms;

namespace GUI.CErrorLog
{
    class ErrorLog
    {
        private const string LOG_FOLDER = "\\Log\\";
        private const string LOG_FILE = "Errors.txt";

        private static ErrorLog m_Instance = null;

        private ErrorLog()
        {
            if (!System.IO.Directory.Exists(Application.StartupPath + LOG_FOLDER))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + LOG_FOLDER);
            }
            if (!System.IO.File.Exists(Application.StartupPath + LOG_FOLDER + LOG_FILE))
            {
                FileStream l_File = System.IO.File.Create(Application.StartupPath + LOG_FOLDER + LOG_FILE);
                l_File.Close();
            }
        }

        public static ErrorLog GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ErrorLog();
            }
            return m_Instance;
        }

        public void ClearErrorLog()
        {
            FileStream l_File = new FileStream(Application.StartupPath + LOG_FOLDER + LOG_FILE, FileMode.Create, FileAccess.Write);

            l_File.Close();
        }

        public string ReadErrorLog()
        {
            FileStream l_File = new FileStream(Application.StartupPath + LOG_FOLDER + LOG_FILE, FileMode.Open, FileAccess.Read);
            StreamReader l_FileReader = new StreamReader(l_File);
            string l_ErrorLogContent = l_FileReader.ReadToEnd();

            l_FileReader.Close();
            l_File.Close();

            return l_ErrorLogContent;
        }

        public void WriteToErrorLog(string p_Msg, string p_StkTrace, string p_Title)
        {
            FileStream l_File = new FileStream(Application.StartupPath + LOG_FOLDER + LOG_FILE, FileMode.Append, FileAccess.Write);
            StreamWriter l_FileWriter = new StreamWriter(l_File);

            l_FileWriter.Write("Function   : " + p_Title);
            l_FileWriter.WriteLine();
            l_FileWriter.Write("Message    : " + p_Msg);
            l_FileWriter.WriteLine();
            l_FileWriter.Write("StackTrace : " + p_StkTrace);
            l_FileWriter.WriteLine();
            l_FileWriter.Write("Date/Time  : " + DateTime.Now.ToString());
            l_FileWriter.WriteLine();
            l_FileWriter.WriteLine();
            l_FileWriter.WriteLine();

            l_FileWriter.Close();
            l_File.Close();
        }
    }
}
