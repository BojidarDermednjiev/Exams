namespace Medicines.Utilities
{
    using System.Text;
    using System.Xml.Serialization;
    public class XmlHelper
    {
        public T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), xmlRoot);
            using StringReader xmlReader = new StringReader(inputXml);
            T deserializedDtos = (T)serializer.Deserialize(xmlReader)!;
            return deserializedDtos;
        }
        public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T[]), xmlRoot);
            using StringReader xmlReader = new StringReader(inputXml);
            T[] deserializedDtos = (T[])serializer.Deserialize(xmlReader)!;
            return deserializedDtos;
        }

        public string Serialize<T>(T obj, string rootName)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), xmlRoot);
            using StringWriter xmlWriter = new StringWriter(sb);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            serializer.Serialize(xmlWriter, obj, namespaces);
            return sb.ToString().TrimEnd();
        }
        public string Serialize<T>(T[] obj, string rootName)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T[]), xmlRoot);
            using StringWriter xmlWriter = new StringWriter(sb);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            serializer.Serialize(xmlWriter, obj, namespaces);
            return sb.ToString().TrimEnd();
        }
    }
}
