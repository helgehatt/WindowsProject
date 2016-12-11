using System.IO;
using System.Text;
using System.Threading.Tasks;
using ClassDiagramTool.Model;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClassDiagramTool.Tools
{
    public class SerializationController
    {
        public static async void AsyncSerializeToFile(Diagram diagram, string path)
        {
            await Task.Run(() => SerializeToFile(diagram, path));
        }

        private static void SerializeToFile(Diagram diagram, string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var serializer = new DataContractSerializer(typeof(Diagram));
                serializer.WriteObject(stream, diagram);
            }
        }

        public static Task<Diagram> AsyncDeserializeFromFile(string path)
        {
            return Task.Run(() => DeserializeFromFile(path));
        }

        public static Diagram DeserializeFromFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                var serializer = new DataContractSerializer(typeof(Diagram));
                var diagram = serializer.ReadObject(reader, true) as Diagram;

                return diagram;
            }
        }        
    }
}
