using System;
using System.Collections.Generic;
using System.IO;

public class HtmlGenerator 
{
   public string GenerateSpans(List<Sentence> sentences)
   {
      return GeneratePrefixedSpans(sentences, "");
   }

   private string GeneratePrefixedSpans(List<Sentence> sentences, string prefix = "") 
   {
      string sentencesString = "";
      foreach (var sentence in sentences) 
         sentencesString += prefix + GenerateSentence(sentence);

      return sentencesString;
   }

   public string GenerateFull(List<Sentence> sentences) 
   {
      string templatesDirectory = AppDomain.CurrentDomain.BaseDirectory + "/Templates/";
      return File.ReadAllText(templatesDirectory + "top.html") +
             GenerateDiv(sentences) + 
             File.ReadAllText(templatesDirectory + "bottom.html");
   }

   public string GenerateDiv(List<Sentence> sentences) 
   {
      string spans = GeneratePrefixedSpans(sentences, "\t");
      return $"<div id='glossbox'>\n<span id='tooltip'></span>\n{spans}\n</div>";
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
