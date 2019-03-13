using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Abbreviation 
{
   [Key]
   public string Label { get; set; }
   public string Value { get; set; }
   public string Color { get; set; }

   public Abbreviation(string label, string value, string color) 
   {
      this.Label = label;
      this.Value = value;
      this.Color = color;
   }
}
