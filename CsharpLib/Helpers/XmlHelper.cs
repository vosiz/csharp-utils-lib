using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Vosiz.Helpers
{
    public static class XmlHelper
    {
        public static string ToXml(object obj)
        {

            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                var sw = new StringWriter();
                serializer.Serialize(sw, obj);
                return sw.ToString();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static T FromXml<T>(string xml)
        {

            try
            {
                return (T)FromXml(xml, typeof(T));
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static object FromXml(string xml, Type t)
        {

            try
            {
                var serializer = new XmlSerializer(t);
                var sr = new StringReader(xml);
                return serializer.Deserialize(sr);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        // Checks whether the string is valid XML, without throwing
        public static bool IsValid(string xml)
        {

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
