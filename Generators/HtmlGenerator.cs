using System;
using System.Collections.Generic;

class HtmlGenerator 
{
   public string GenerateSpans(List<Sentence> sentences)
   {
      string sentencesString = "";
      foreach (var sentence in sentences) 
         sentencesString += GenerateSentence(sentence);

      return sentencesString;
   }

   private string GenerateSentence(Sentence sentence) 
   {
      string sentenceString = "";
      foreach (var word in sentence.Words) 
         sentenceString += GenerateWord(word);

      return sentenceString + ".";
   }

   private string GenerateWord(Word word)
   {
      string wordString = "";
      foreach (var morpheme in word.Morphemes) 
         wordString += GenerateSpan(morpheme);

      return wordString + "\n";
   }

   private string GenerateSpan(Morpheme morpheme) 
   {
      string gloss = morpheme.Gloss;
      string original = morpheme.Original;
      string labels = "labels='" + string.Join(" ", morpheme.Labels) + "'";

      return $"<span class='morpheme' gloss='{gloss}' {labels}>{original}</span>";
   }
}
