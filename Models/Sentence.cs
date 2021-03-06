using System;
using System.Collections.Generic;

public class Sentence
{
   public List<Word> Words { get; set; }

   public Sentence(List<Word> words) 
   {
      this.Words = words;
   }

   ///<summary>
   ///Reassemble to a normal sentence
   ///</summary>
   public string Assemble()
   {
      string output = "";
      foreach (var word in Words)
      {
         foreach (var morpheme in word.Morphemes)
            output += morpheme.Original;
         output += " ";
      }

      return output;
   }
}
