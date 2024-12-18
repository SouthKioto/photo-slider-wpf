using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    public class TxtFileHandler
    {
        public static void CreatePhotoDatabase(string path)
        {
            if (!File.Exists(path))
            {
                string template = "1;Laki;Piękne zdjęcie łąki;11-12-2022;" +
                                  "2;Gory;Piękne zdjęcie gór;12-12-2022;" +
                                  "3;Lasy;Piękne zdjęcie lasu;13-12-2022;";

                File.WriteAllText(path, template);
            }
        }

        public static List<Photo> GetPhotosData(string path)
        {
            List<Photo> deserialized_data = new List<Photo>();
            if (File.Exists(path))
            {
                string readed_data = File.ReadAllText(path);
                string[] splited_data = [];

                splited_data = readed_data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for(int i = 0; i < splited_data.Length; i+=4)
                {
                    var photo_data = new Photo
                    {
                        Photo_Id = int.Parse(splited_data[i].ToString()),
                        Photo_Title = splited_data[i + 1].ToString(),
                        Photo_Description = splited_data[i + 2].ToString(),
                        Add_Date = splited_data[i + 3].ToString(),
                        Photo_Source = $"img/{splited_data[i + 1]}.jpg",
                    };
                    deserialized_data.Add(photo_data);
                }
            }
            return deserialized_data;
        }
    }
}
