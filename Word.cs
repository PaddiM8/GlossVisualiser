using System;
using System.Collections.Generic;

class Word
{
   public List<Morpheme> Morphemes { get; set; }

   public Word(List<Morpheme> morphemes)
   {
      this.Morphemes = morphemes;
   }
}
