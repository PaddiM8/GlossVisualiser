using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class DictionaryManager 
{
   public static string DictionaryLocation = "Resources/dictionary.bin";
   public static Dictionary<string, Abbreviation> AbbreviationDictionary 
      = new Dictionary<string, Abbreviation>();

   public void CreateFromFile(string fileLocation) 
   {
      foreach (var line in File.ReadAllLines(fileLocation)) 
      {
         string label = "";
         string value = "";
         string color = "";
         List<int> spaces = new List<int>();

         for (int i = 0; i < line.Length && line != ""; i++) 
         {
            if (line[i] == ' ' && spaces.Count < 2)
            {
               spaces.Add(i);

               if (spaces.Count == 1)
                  label = line.Substring(0, i);
               else if (spaces.Count == 2)
                  color = line.Substring(spaces[0] + 1, i - spaces[0] - 1);
            }
            else if (spaces.Count == 2) 
            {
               value = line.Substring(spaces[1] + 1, line.Length - spaces[1] - 1);
            }
         }

         AbbreviationDictionary.Add(label, new Abbreviation(label, value, color));
         Save(Path.GetFileNameWithoutExtension(fileLocation));
      }
   }

   private void Save(string name)
   {
      using (FileStream fs = File.OpenWrite(name + ".bin"))
         using (BinaryWriter writer = new BinaryWriter(fs))
         {
            writer.Write(AbbreviationDictionary.Count);
            foreach (var pair in AbbreviationDictionary)
            {
               writer.Write(pair.Key);
               writer.Write(pair.Value.Value);
               writer.Write(pair.Value.Color);
            }
         }
   }

   public void Load(string fileLocation) 
   {
      using (FileStream fs = File.OpenRead(fileLocation))
         using (BinaryReader reader = new BinaryReader(fs))
         {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
               string label = reader.ReadString();
               string value = reader.ReadString();
               string color = reader.ReadString();

               AbbreviationDictionary[label] = new Abbreviation(
                  label,
                  value,
                  color
               );
            }
         }
   }
}
