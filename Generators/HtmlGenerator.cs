using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class HtmlGenerator 
{
   private static StringBuilder stringBuild = new StringBuilder(); // String builder to append everything to

   ///<summary>
   ///Generate full HTML
   ///</summary>
   public string GenerateFull(List<Sentence> sentences) 
   {
      foreach (var sentence in sentences)
         GenerateSentence(sentence);

      string templatesDirectory = "Resources/";
      return File.ReadAllText(templatesDirectory + "top.html") +
             stringBuild.ToString() + 
             File.ReadAllText(templatesDirectory + "bottom.html");
   }

   ///<summary>
   ///Generate HTML from a single sentence
   ///</summary>
   private void GenerateSentence(Sentence sentence)
   {
      for (int s = 0; s < sentence.Words.Count; s++) {
         var word = sentence.Words[s];
         for (int w = 0; w < word.Morphemes.Count; w++)
            stringBuild.Append(GenerateSpan(word.Morphemes[w],
                     s == 0 && w == 0));

         if (s != sentence.Words.Count - 1) stringBuild.Append("\n");
         else stringBuild.Append(".");
      }
   }

   ///<summary>
   ///Generate a single span (morpheme)
   ///</summary>
   private string GenerateSpan(Morpheme morpheme, bool firstWord = false) 
   {
      string gloss = morpheme.Gloss;
      string original = morpheme.Original;
      string labels = "labels='" + string.Join(" ", morpheme.Labels) + "'";
      if (firstWord) original = original.FirstCharToUpper();

      return $"<span class='morpheme' gloss='{gloss}' {labels}>{original}</span>";
   }
}
