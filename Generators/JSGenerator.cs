using System;
using System.IO;
using System.Collections.Generic;

class JSGenerator 
{
   public string Generate(List<Abbreviation> abbreviations) 
   {
      return GenerateAbbreviationList(abbreviations) +
             File.ReadAllText("Resources/script.js");
   }

   private string GenerateAbbreviationList(List<Abbreviation> abbreviations) 
   {
      string javascript = "var abbreviations = {};\n";
      foreach (var abbreviation in abbreviations) 
         javascript += $"abbreviations[\"{abbreviation.Label}\"] = \"{abbreviation.Value}\";\n";

      return javascript;
   }
}
