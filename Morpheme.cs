using System;

class Morpheme
{
   public string Original { get; set; }
   public string Gloss { get; set; }
   public string[] Labels { get; set; }

   public Morpheme(string original, string gloss, string[] labels)
   {
      this.Original = original;
      this.Gloss = gloss;
      this.Labels = labels;
   }
}
