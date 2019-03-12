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
      string templatesDirectory = "Resources/";
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
      for (int i = 0; i < sentence.Words.Count; i++) {
         sentenceString += GenerateWord(sentence.Words[i], i == 0);

         if (i != sentence.Words.Count - 1) sentenceString += "\n";
         else sentenceString += ".";
      }

      return sentenceString;
   }

   private string GenerateWord(Word word, bool firstWord = false)
   {
      string wordString = "";
      for (int i = 0; i < word.Morphemes.Count; i++) {
         wordString += GenerateSpan(word.Morphemes[i], firstWord && i == 0);
      }

      return wordString;
   }

   private string GenerateSpan(Morpheme morpheme, bool firstWord = false) 
   {
      string gloss = morpheme.Gloss;
      string original = morpheme.Original;
      string labels = "labels='" + string.Join(" ", morpheme.Labels) + "'";
      if (firstWord) original = original.FirstCharToUpper();

      return $"<span class='morpheme' gloss='{gloss}' {labels}>{original}</span>";
   }
}
