using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Security.Permissions;
using System.IO;

namespace UserStorage
{
    [Serializable]
    public class XMLWorker : ISerializable
    {
        private readonly XmlSerializer _serializer;

        #region ctors
        public XMLWorker()
        {
            _serializer = new XmlSerializer(typeof(List<User>));
        }
        protected XMLWorker(SerializationInfo info, StreamingContext context):this()
        {
            CheckSerializationInfo(info);
            var type = Type.GetType((string)info.GetValue("type", typeof(string)));
        }
        #endregion
        public void WriteToXML(List<User> users, string path)
        {
            if (users == null) throw new ArgumentException("There are no users for writing in xml file");
            if(String.IsNullOrEmpty(path)) throw new ArgumentException("The path of file is not created");
            SerializableList list = new SerializableList(users);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(fs, list);
            }
        }

        public List<User> ReadFromXML(string path)
        {
            if (!File.Exists(path)) return null;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                List<User> newUsers = (List<User>)_serializer.Deserialize(fs);
                return newUsers;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            CheckSerializationInfo(info);
            info.AddValue("type", typeof(List<User>).FullName, typeof(string));
        }

        private void CheckSerializationInfo(SerializationInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
        }
    }


    [Serializable]
    public class SerializableList
    {
        public List<User> Users { get; set; }
        public SerializableList(List<User> list)
        {
            Users = list;
        }
    }
}
