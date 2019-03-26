using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class DictionaryManager 
{
   public static string DictionaryLocation = "Resources/dictionary.bin";
   public static Dictionary<string, Abbreviation> AbbreviationDictionary 
      = new Dictionary<string, Abbreviation>();

   ///<summary>
   ///Read a manually created dictionary file and load it in to memory
   ///</summary>
   public void CreateFromFile(string fileLocation) 
   {
      foreach (var line in File.ReadAllLines(fileLocation)) 
      {
         string label = "";
         string value = "";
         string color = "";
         List<int> spaces = new List<int>();

         // Loop through each character in the current line
         for (int i = 0; i < line.Length && line != ""; i++) 
         {
            // If current character is a space and label and color isn't already found,
            // add everything between the last space and this space to the list
            if (line[i] == ' ' && spaces.Count < 2)
            {
               spaces.Add(i);

               if (spaces.Count == 1)
                  label = line.Substring(0, i);
               else if (spaces.Count == 2)
                  color = line.Substring(spaces[0] + 1, i - spaces[0] - 1);
            }
            else if (spaces.Count == 2) // If 'label' and 'color' are found already, add the rest, since that's the 'value'
            {
               value = line.Substring(spaces[1] + 1, line.Length - spaces[1] - 1);
            }
         }

         AbbreviationDictionary.Add(label, new Abbreviation(label, value, color));
         Save(Path.GetFileNameWithoutExtension(fileLocation));
      }
   }

   ///<summary>
   ///Save the current dictionary in memory to disk in binary format
   ///</summary>
   public void Save(string name)
   {
      using (FileStream fs = File.OpenWrite(name))
         using (BinaryWriter writer = new BinaryWriter(fs))
         {
            writer.Write(AbbreviationDictionary.Count); // Specify how many items there will be
            foreach (var pair in AbbreviationDictionary) // Write label, value and color to the file
            {
               writer.Write(pair.Key);
               writer.Write(pair.Value.Value);
               writer.Write(pair.Value.Color);
            }
         }
   }

   ///<summary>
   ///Load a binary dictionary file to memory
   ///</summary>
   public void Load(string fileLocation) 
   {
      using (FileStream fs = File.OpenRead(fileLocation))
         using (BinaryReader reader = new BinaryReader(fs))
         {
            int count = reader.ReadInt32(); //  Read how many items there are 
            for (int i = 0; i < count; i++) // Loop through each item add att them to memory
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
