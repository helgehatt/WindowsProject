using System.IO;
using System.Text;
using System.Threading.Tasks;
using ClassDiagramTool.Model;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClassDiagramTool.Commands
{
    public class Serializer
    {
        public static Serializer Instance { get; } = new Serializer();

        private Serializer() { }

        public async void AsyncSerializeToFile(Diagram diagram, string path)
        {
            await Task.Run(() => SerializeToFile(diagram, path));
        }

        private void SerializeToFile(Diagram diagram, string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var serializer = new DataContractSerializer(typeof(Diagram));
                serializer.WriteObject(stream, diagram);
            }
        }

        private void SerializeToClipboard(Shape shape, string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var serializer = new DataContractSerializer(typeof(Shape));
                serializer.WriteObject(stream, shape);
                
            }
        }


        public Task<Diagram> AsyncDeserializeFromFile(string path)
        {
            return Task.Run(() => DeserializeFromFile(path));
        }

        public Diagram DeserializeFromFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                var serializer = new DataContractSerializer(typeof(Diagram));
                var diagram = serializer.ReadObject(reader, true) as Diagram;

                return diagram;
            }
        }

        public Task<string> AsyncSerializeToString(Diagram diagram)
        {
            return Task.Run(() => SerializeToString(diagram));
        }

        private string SerializeToString(Diagram diagram)
        {
            var stringBuilder = new StringBuilder();

            using (TextWriter stream = new StringWriter(stringBuilder))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Diagram));
                serializer.Serialize(stream, diagram);
            }

            return stringBuilder.ToString();
        }

        public Task<Diagram> AsyncDeserializeFromString(string xml)
        {
            return Task.Run(() => DeserializeFromString(xml));
        }

        private Diagram DeserializeFromString(string xml)
        {
            using (TextReader stream = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Diagram));
                Diagram diagram = serializer.Deserialize(stream) as Diagram;

                return diagram;
            }
        }
    }

}
