using System;
using System.IO;
using System.Collections.Generic;

class JSGenerator 
{
   
   ///<summary>
   ///Generate JavaScript code (toolip)
   ///</summary>
   public string Generate()
   {
      return GenerateAbbreviationList(DictionaryManager.AbbreviationDictionary) +
             File.ReadAllText("Resources/script.js");
   }

   ///<summary>
   ///Generate the JavaScript list of abbreviations with a key->value pair
   ///</summary>
   private string GenerateAbbreviationList(Dictionary<string, Abbreviation> abbreviations) 
   {
      string javascript = "var abbreviations = {};\n";
      foreach (var abbreviation in abbreviations) 
         javascript += $"abbreviations[\"{abbreviation.Value.Label}\"] = \"{abbreviation.Value.Value}\";\n";

      return javascript;
   }
}
