using System;
using System.Collections.Generic;
using System.IO;

class CssGenerator 
{
   
   ///<summary>
   ///Generate CSS
   ///</summary>

   public string Generate()
   {
      return File.ReadAllText("Resources/style.css") +
             GenerateAbbreviationStyles(DictionaryManager.AbbreviationDictionary);
   }

   ///<summary>
   ///Generate CSS that colors each abbreviation type
   ///</summary>
   private string GenerateAbbreviationStyles(Dictionary<string, Abbreviation> abbreviations) 
   {
      string css = "";
      foreach (var abbreviation in abbreviations) {
         css += $"span[gloss='{abbreviation.Value.Label}'], span[labels^='{abbreviation.Value.Label}'] {{\n\t" + 
                $"border-color: #{abbreviation.Value.Color}; color: #{abbreviation.Value.Color};\n}}\n\n";
      }
      return css;
   }
}
