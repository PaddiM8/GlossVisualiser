using System;
using System.Collections.Generic;
using System.IO;

class CssGenerator 
{
   public string Generate(List<Abbreviation> abbreviations) 
   {
      return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Templates/style.css") +
             GenerateAbbreviationStyles(abbreviations);
   }

   private string GenerateAbbreviationStyles(List<Abbreviation> abbreviations) 
   {
      string css = "";
      foreach (var abbreviation in abbreviations) 
         css += $"span[gloss='{abbreviation.Label}'] border-color: blue;\n";

      return css;
   }
}
