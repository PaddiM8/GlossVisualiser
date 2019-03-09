using System;
using System.Collections.Generic;

class MainClass {
  public static void Main (string[] args) {
    List<Token> tokens = new Lexer().Lex("on talo-ssa { be.1SG.PRS house-INE } | koira-t juokse-vat { dog-PL run.IMP-3PL }");
    var parse = new Parser().Parse(tokens);
    
    foreach (var sentence in parse) {
      foreach (var word in sentence.Words) {
        foreach (var morpheme in word.Morphemes) {
          Console.WriteLine("...");
          Console.WriteLine("Original: " + morpheme.Original);
          Console.WriteLine("Gloss: " + morpheme.Gloss);
          Console.WriteLine("Labels: " + string.Join(", ", morpheme.Labels));
        }
        Console.WriteLine("---------------");
      }
      Console.WriteLine(">>>" + sentence.Assemble());
    }
  }
}
