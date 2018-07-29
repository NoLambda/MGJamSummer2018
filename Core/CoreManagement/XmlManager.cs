using System;
using System.IO;
using System.Xml.Serialization;

namespace MGJamSummer2018.Core
{
    public class XmlManager<T>
    {
        public T Load(string path, Type type)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }
    }
}