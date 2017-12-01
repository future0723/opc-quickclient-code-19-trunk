/*
 *  OPCUtility.cs
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

using System.Runtime.InteropServices;
using System.Collections.Generic;
using OPCLib;

namespace GUI.CUtility
{
    class OpcUtility
    {
        public const char ITEM_PATH_SEPARATOR = '.';
        public const char ARRAY_SEPARATOR = ';';

        public const string ITEM_NULL = "NULL";
        public const string ITEM_UNKNOWN = "UNKNOWN";

        public const string ITEM_QUALITY_BAD = "BAD";
        public const string ITEM_QUALITY_UNCERTAIN = "UNCERTAIN";
        public const string ITEM_QUALITY_GOOD = "GOOD";

        public const string ITEM_TYPE_I1 = "VT_I1";
        public const string ITEM_TYPE_I2 = "VT_I2";
        public const string ITEM_TYPE_I4 = "VT_I4";
        public const string ITEM_TYPE_I8 = "VT_I8";
        public const string ITEM_TYPE_UI1 = "VT_UI1";
        public const string ITEM_TYPE_UI2 = "VT_UI2";
        public const string ITEM_TYPE_UI4 = "VT_UI4";
        public const string ITEM_TYPE_UI8 = "VT_UI8";
        public const string ITEM_TYPE_R4 = "VT_R4";
        public const string ITEM_TYPE_R8 = "VT_R8";
        public const string ITEM_TYPE_BSTR = "VT_BSTR";
        public const string ITEM_TYPE_BOOL = "VT_BOOL";
        public const string ITEM_TYPE_DATE = "VT_DATE";
        public const string ITEM_TYPE_DECIMAL = "VT_DECIMAL";
        public const string ITEM_TYPE_ARRAY = "VT_ARRAY";
        public const string ITEM_TYPE_SHORT_ARRAY = "VT_SHORT_ARRAY";
        public const string ITEM_TYPE_LONG_ARRAY = "VT_LONG_ARRAY";
        public const string ITEM_TYPE_FLOAT_ARRAY = "VT_FLOAT_ARRAY";
        public const string ITEM_TYPE_DOUBLE_ARRAY = "VT_DOUBLE_ARRAY";

        private OpcUtility()
        {
        }

        public static string TypeToString(int p_Type)
        {
            switch (p_Type)
            {
                case (int) VarEnum.VT_I1:
                    return ITEM_TYPE_I1;
                case (int) VarEnum.VT_I2:
                    return ITEM_TYPE_I2;
                case (int) VarEnum.VT_I4:
                    return ITEM_TYPE_I4;
                case (int) VarEnum.VT_I8:
                    return ITEM_TYPE_I8;
                case (int) VarEnum.VT_UI1:
                    return ITEM_TYPE_UI1;
                case (int) VarEnum.VT_UI2:
                    return ITEM_TYPE_UI2;
                case (int) VarEnum.VT_UI4:
                    return ITEM_TYPE_UI4;
                case (int) VarEnum.VT_UI8:
                    return ITEM_TYPE_UI8;
                case (int) VarEnum.VT_R4:
                    return ITEM_TYPE_R4;
                case (int) VarEnum.VT_R8:
                    return ITEM_TYPE_R8;
                case (int) VarEnum.VT_BSTR:
                    return ITEM_TYPE_BSTR;
                case (int) VarEnum.VT_BOOL:
                    return ITEM_TYPE_BOOL;
                case (int) VarEnum.VT_DATE:
                    return ITEM_TYPE_DATE;
                case (int) VarEnum.VT_DECIMAL:
                    return ITEM_TYPE_DECIMAL;
                case (int)VarEnum.VT_ARRAY:
                    return ITEM_TYPE_ARRAY;
                case (int)VarEnumArray.SHORT_ARRAY:
                    return ITEM_TYPE_SHORT_ARRAY;
                case (int)VarEnumArray.LONG_ARRAY:
                    return ITEM_TYPE_LONG_ARRAY;
                case (int)VarEnumArray.FLOAT_ARRAY:
                    return ITEM_TYPE_FLOAT_ARRAY;
                case (int) VarEnumArray.DOUBLE_ARRAY:
                    return ITEM_TYPE_DOUBLE_ARRAY;
            }
            return ITEM_UNKNOWN;
        }

        public static string ValueToString(string p_Type, object p_Value)
        {
            string l_StringValue = ITEM_UNKNOWN;

            if (p_Value == null)
            {
                l_StringValue = ITEM_NULL;
            }
            else
            {
                if (p_Type.Equals(ITEM_UNKNOWN))
                {
                    l_StringValue = ITEM_UNKNOWN;
                }
                else if (p_Type.Equals(ITEM_TYPE_SHORT_ARRAY) || p_Type.Equals(ITEM_TYPE_LONG_ARRAY))
                {
                    string l_StringInt = "";
                    List<long> l_ListInt = new List<long>((long[]) p_Value);

                    foreach (long l_Value in l_ListInt)
                    {
                        l_StringInt += l_Value + ARRAY_SEPARATOR;
                    }

                    l_StringValue = l_StringInt.Substring(0, l_StringInt.Length - 1);
                }
                else if (p_Type.Equals(ITEM_TYPE_FLOAT_ARRAY))
                {
                    string l_StringFloat = "";
                    List<float> l_ListFloat = new List<float>((float[]) p_Value);

                    foreach (long l_Value in l_ListFloat)
                    {
                        l_StringFloat += l_Value + ARRAY_SEPARATOR;
                    }

                    l_StringValue = l_StringFloat.Substring(0, l_StringFloat.Length - 1);
                }
                else if (p_Type.Equals(ITEM_TYPE_DOUBLE_ARRAY))
                {
                    string l_StringDouble = "";
                    List<double> l_ListDouble = new List<double>((double[]) p_Value);

                    foreach (long l_Value in l_ListDouble)
                    {
                        l_StringDouble += l_Value + ARRAY_SEPARATOR;
                    }

                    l_StringValue = l_StringDouble.Substring(0, l_StringDouble.Length - 1);
                }
                else
                {
                    l_StringValue = p_Value.ToString();
                }
            }

            return l_StringValue;
        }

        public static object StringToValue(string p_Type, string p_String)
        {
            switch (p_Type)
            {
                case ITEM_TYPE_I1:
                    return System.Convert.ToSByte(p_String);
                case ITEM_TYPE_I2:
                    return System.Convert.ToInt16(p_String);
                case ITEM_TYPE_I4:
                    return System.Convert.ToInt32(p_String);
                case ITEM_TYPE_I8:
                    return System.Convert.ToInt64(p_String);
                case ITEM_TYPE_UI1:
                    return System.Convert.ToByte(p_String);
                case ITEM_TYPE_UI2:
                    return System.Convert.ToUInt16(p_String);
                case ITEM_TYPE_UI4:
                    return System.Convert.ToUInt32(p_String);
                case ITEM_TYPE_UI8:
                    return System.Convert.ToUInt64(p_String);
                case ITEM_TYPE_R4:
                    return System.Convert.ToSingle(p_String);
                case ITEM_TYPE_R8:
                    return System.Convert.ToDouble(p_String);
                case ITEM_TYPE_BSTR:
                    return p_String;
                case ITEM_TYPE_BOOL:
                    if (p_String.Equals("0") || p_String.Equals("1"))
                    {
                        return p_String.Equals("1");
                    }
                    else
                    {
                        return System.Convert.ToBoolean(p_String);
                    }
                case ITEM_TYPE_DATE:
                    return System.Convert.ToDateTime(p_String);
                case ITEM_TYPE_DECIMAL:
                    return System.Convert.ToDecimal(p_String);
                case ITEM_TYPE_SHORT_ARRAY:
                case ITEM_TYPE_LONG_ARRAY:
                    List<int> l_ListInt = new List<int>();
                    string[] l_ValuesInt = p_String.Split(';');

                    foreach (string l_ValueInt in l_ValuesInt)
                    {
                        if (l_ValueInt != "")
                        {
                            l_ListInt.Add(int.Parse(l_ValueInt));
                        }
                    }
                    return l_ListInt.ToArray();
                case ITEM_TYPE_FLOAT_ARRAY:
                case ITEM_TYPE_DOUBLE_ARRAY:
                    List<double> l_ListDouble = new List<double>();
                    string[] l_ValuesDouble = p_String.Split(';');

                    foreach (string l_ValueDouble in l_ValuesDouble)
                    {
                        if (l_ValueDouble != "")
                        {
                            l_ListDouble.Add(int.Parse(l_ValueDouble));
                        }
                    }
                    return l_ListDouble.ToArray();
                default:
                    return new object();
            }
        }

        public static string QualityToString(short p_Quality)
        {
            switch (p_Quality)
            {
                case (short) OpcQualityStatus.BAD:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.CONFIG_ERROR:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.NOT_CONNECTED:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.DEVICE_FAILURE:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.SENSOR_FAILURE:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.LAST_KNOWN:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.COMM_FAILURE:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.OUT_OF_SERVICE:
                    return ITEM_QUALITY_BAD;
                case (short) OpcQualityStatus.UNCERTAIN:
                    return ITEM_QUALITY_UNCERTAIN;
                case (short) OpcQualityStatus.LAST_USABLE:
                    return ITEM_QUALITY_UNCERTAIN;
                case (short) OpcQualityStatus.SENSOR_CAL:
                    return ITEM_QUALITY_UNCERTAIN;
                case (short) OpcQualityStatus.EGU_EXCEEDED:
                    return ITEM_QUALITY_UNCERTAIN;
                case (short) OpcQualityStatus.SUB_NORMAL:
                    return ITEM_QUALITY_UNCERTAIN;
                case (short) OpcQualityStatus.OK:
                    return ITEM_QUALITY_GOOD;
                case (short) OpcQualityStatus.LOCAL_OVERRIDE:
                    return ITEM_QUALITY_GOOD;
            }
            return ITEM_UNKNOWN;
        }

        public static string TimeStampToString(long p_TimeStamp)
        {
            return System.DateTime.FromBinary(p_TimeStamp).ToString();
        }
    }
}
