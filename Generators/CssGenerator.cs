using System;
using System.Collections.Generic;
using System.IO;

class CssGenerator 
{
   public string Generate()
   {
      return File.ReadAllText("Resources/style.css") +
             GenerateAbbreviationStyles(DictionaryManager.AbbreviationDictionary);
   }

   private string GenerateAbbreviationStyles(Dictionary<string, Abbreviation> abbreviations) 
   {
      string css = "";
      foreach (var abbreviation in abbreviations) 
         css += $"span[gloss='{abbreviation.Value.Label}'] {{\n\t" + 
             $"border-color: #{abbreviation.Value.Color}; color: #{abbreviation.Value.Color};\n}}\n\n";

      return css;
   }
}
