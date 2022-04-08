using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EdGeLib.Sprites
{
    public static class SpriteManager
    {
        public static string DataPath { get; private set; }
        public static List<string> Names { get; private set; }
        public static Dictionary<string, Sprite> Templates { get; set; }

        public static void Initialize(string gameName, List<string> spriteNames)
        {
            DataPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EdGeGames\" + gameName + @"\Sprites\";
            Names = spriteNames;
            Templates = new Dictionary<string, Sprite>();
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            string[] files = Directory.GetFiles(DataPath);
            if (files.Length > 0)
            {
                List<string> savedSpriteNames = new List<string>();
                foreach (string file in files)
                {
                    string spriteName = Path.GetFileNameWithoutExtension(file);
                    savedSpriteNames.Add(spriteName);
                    Templates.Add(spriteName, DeserializeSpriteData(spriteName));
                }
                foreach (string spriteName in Names)
                {
                    if (!savedSpriteNames.Contains(spriteName))
                    {
                        Templates.Add(spriteName, new Sprite());
                    }
                }
            }
            else
            {
                foreach (string spriteName in Names)
                {
                    Templates.Add(spriteName, new Sprite());
                }
            }
        }

        public static Sprite GetSprite(string key)
        {
            return Templates[key]; 
        }

        public static void Write()
        {
            DirectoryInfo dir = new DirectoryInfo(DataPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
            foreach (string spriteName in Names)
            {
                SerializeSpriteData(spriteName);
            }
        }

        public static void SerializeSpriteData(string key)
        {
            SpriteData spriteData = new SpriteData(Templates[key]);
            XmlSerializer serializer = new XmlSerializer(typeof(SpriteData));
            FileStream stream = File.Create(DataPath + key + ".xml");
            serializer.Serialize(stream, spriteData);
            stream.Close();
        }

        public static Sprite DeserializeSpriteData(string key)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SpriteData));
            StreamReader reader = new StreamReader(DataPath + key + ".xml");
            SpriteData spriteData = (SpriteData)serializer.Deserialize(reader);
            reader.Close();
            return new Sprite(spriteData);
        }
    }
}
