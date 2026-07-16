using System;
using Vosiz.Helpers;

namespace Tests.Helpers
{

    public class XmlSampleData
    {
        public string Name { get; set; }
        public int Value { get; set; }

        // Constructor
        public XmlSampleData() { }
    }

    public static class XmlHelperTests
    {

        // ToXml serializes an object into an XML string containing its data
        public static void ToXmlSerializesObject() {

            XmlSampleData data = new XmlSampleData { Name = "test", Value = 42 };
            string xml = XmlHelper.ToXml(data);

            Check.True(xml.Contains("<Name>test</Name>"), "XML should contain the Name element");
            Check.True(xml.Contains("<Value>42</Value>"), "XML should contain the Value element");
        }

        // FromXml<T> deserializes back into an equivalent object
        public static void FromXmlGenericRoundTrips() {

            XmlSampleData original = new XmlSampleData { Name = "test", Value = 42 };
            string xml = XmlHelper.ToXml(original);

            XmlSampleData result = XmlHelper.FromXml<XmlSampleData>(xml);

            Check.Equal("test", result.Name);
            Check.Equal(42, result.Value);
        }

        // FromXml with an explicit Type deserializes back into an equivalent object
        public static void FromXmlWithTypeRoundTrips() {

            XmlSampleData original = new XmlSampleData { Name = "test", Value = 42 };
            string xml = XmlHelper.ToXml(original);

            XmlSampleData result = (XmlSampleData)XmlHelper.FromXml(xml, typeof(XmlSampleData));

            Check.Equal("test", result.Name);
            Check.Equal(42, result.Value);
        }

    }
}
