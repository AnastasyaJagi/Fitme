using System;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FitmeApp.Utils
{
    public class FilesWriter
    {
        public static FilesWriter SharedInstance { get; } = new FilesWriter();

        public void SaveToJson(object data,string filename)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(path, filename);
                using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
                using (var strm = new StreamWriter(file))
                {
                    strm.Write(json);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void ReadJson<T>(string filename, out T deserializedObject)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(path, filename);
                deserializedObject = default;
                if (File.Exists(filePath))
                {
                    using var file = File.Open(filePath,FileMode.Open, FileAccess.Read);
                    using var strm = new StreamReader(file);
                    var json = strm.ReadToEnd();
                    deserializedObject = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception e)
            {
                deserializedObject = default;
                Console.WriteLine(e);
            }
        }
    }
}

