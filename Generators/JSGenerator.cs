using System;
using System.IO;
using System.Collections.Generic;

class JSGenerator 
{
   public string Generate()
   {
      return GenerateAbbreviationList(DictionaryManager.AbbreviationDictionary) +
             File.ReadAllText("Resources/script.js");
   }

   private string GenerateAbbreviationList(Dictionary<string, Abbreviation> abbreviations) 
   {
      string javascript = "var abbreviations = {};\n";
      foreach (var abbreviation in abbreviations) 
         javascript += $"abbreviations[\"{abbreviation.Value.Label}\"] = \"{abbreviation.Value.Value}\";\n";

      return javascript;
   }
}
