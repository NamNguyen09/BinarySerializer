using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace cx.BinarySerializer
{
    public class BinarySerializer
    {
        public byte[] Serialize<T>(T o) where T : class
        {
            if (o == null) return null;
            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                var objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        public T Deserialize<T>(byte[] stream)
        {
            if (stream == null || !stream.Any()) return default(T);
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(stream))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
        public bool TryDeserialize<T>(byte[] stream, out T result)
        {
            try
            {
                if (stream == null || !stream.Any())
                {
                    result = default(T);
                    return true;
                }
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Binder = new IgnoreMissingAsemblySerializationBinder();
                using (var memoryStream = new MemoryStream(stream))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    result = (T)binaryFormatter.Deserialize(memoryStream);
                    return true;
                }
            }
            catch (Exception ex)
            {
                result = default(T);
                return false;
            }
        }
    }
    public sealed class IgnoreMissingAsemblySerializationBinder : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type tyType = null;
            string sShortAssemblyName = assemblyName.Split(',')[0];

            Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly ayAssembly in ayAssemblies)
            {
                if (string.IsNullOrWhiteSpace(ayAssembly.FullName)) continue;
                if (sShortAssemblyName == ayAssembly.FullName.Split(',')[0])
                {
                    tyType = ayAssembly.GetType(typeName);
                    break;
                }
            }
            return tyType ?? typeof(Object);
        }
    }
}