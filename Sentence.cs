using System;
using System.Collections.Generic;

class Sentence
{
   public List<Word> Words { get; set; }

   public Sentence(List<Word> words) 
   {
      this.Words = words;
   }

   public string Assemble()
   {
      string output = "";
      foreach (var word in Words)
      {
         foreach (var morpheme in word.Morphemes) output += morpheme.Original;
         output += " ";
      }

      return output;
   }
}
