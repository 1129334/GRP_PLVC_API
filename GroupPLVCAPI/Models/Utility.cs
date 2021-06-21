using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace GroupPLVCAPI.Models
{
    public class Utility
    {
        public string SerializeToXML(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        //public static void ConvertDataTable<T>(DataTable dt,ref List<T> data)
        public static void ConvertDataTable<T>(DataTable dt, ref List<T> data)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                //List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                //return data;
            }
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        if (!DBNull.Value.Equals(dr[column.ColumnName]))
                        {
                            Type proptype = pro.PropertyType;
                            if (proptype.IsGenericType && proptype.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                proptype = new NullableConverter(pro.PropertyType).UnderlyingType;
                            }
                            if (proptype == dr[column.ColumnName].GetType())
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);

                            }
                            else
                            {

                                pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], proptype));
                            }
                        }
                        else
                            continue;
                }
                //temp.GetProperties().Where(x => x.Name == column.ColumnName).SingleOrDefault();
            }
            return obj;
        }

        public static string ValidateString(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                strInput = string.Empty;
            }
            return strInput.TrimEnd();
        }
        public static DateTime ValidateDate(DateTime dtInput)
        {
            if (dtInput.Year < 1800)
            {
                dtInput = Convert.ToDateTime("01-01-1753");
            }
            return dtInput;
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);

        }

        public static object DateFormatMonth(object objInput)
        {
            if (object.ReferenceEquals(objInput, DBNull.Value))
            {
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(objInput)))
                {
                    return null;
                }
                else
                {
                    System.DateTime dt = Convert.ToDateTime(objInput);
                    objInput = dt.ToString("dd/MM/yyyy");
                    return objInput;
                }
            }
        }

        public static string RemoveSpecialCharactersWithSpace(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                strInput = Regex.Replace(strInput, @"[^0-9a-zA-Z]+", string.Empty).ToUpper();
            }
            else
            {
                strInput = string.Empty;
            }
            return strInput;
        }

        public static T DeserializeSring<T>(string strInput)
        {
            T obj = Activator.CreateInstance<T>();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(strInput);
            obj = (T)serializer.Deserialize(stringReader);
            //XmlSerializer serializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));
            return obj;
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            int intAge = 0;
            intAge = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                intAge = intAge - 1;
            return intAge;
        }

        public static string ToUpper(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                strInput = strInput.ToUpper();
            }
            else
            {
                strInput = string.Empty;
            }
            return strInput;
        }
    }
}