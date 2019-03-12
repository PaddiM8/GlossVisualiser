using System;
using System.Collections.Generic;
using System.Linq;

class Parser
{
   public List<Sentence> Parse(List<Token> tokens)
   {
      List<Sentence> sentences = new List<Sentence>();
      sentences.Add(new Sentence(new List<Word>()));

      int currentSentence = 0;
      int currentGlossWord = 0;
      bool inGloss = false;

      foreach (var token in tokens)
      {
         if (token.TokenType == TokenTypes.Word && !inGloss)
         {
            var word = new Word(SplitUpWord(token.Value, false));
            sentences[currentSentence].Words.Add(word);
         }
         else if (token.TokenType == TokenTypes.Word && inGloss)
         {
            var word = sentences[currentSentence].Words[currentGlossWord];
            var morphemes = SplitUpWord(token.Value, true);

            for (int wp = 0; wp < word.Morphemes.Count; wp++)
            {
               word.Morphemes[wp].Gloss = morphemes[wp].Gloss;
               word.Morphemes[wp].Labels = morphemes[wp].Labels;
            }

            currentGlossWord++;
         }
         else if (token.TokenType == TokenTypes.OpenBracket)
         {
            inGloss = true;
         }
         else if (token.TokenType == TokenTypes.ClosedBracket)
         {
            sentences.Add(new Sentence(new List<Word>()));
            currentSentence++;
            currentGlossWord = 0;
            inGloss = false;
         }
      }

      return sentences;
   }

   private List<Morpheme> SplitUpWord(string word, bool gloss)
   {
      string[] parts = word.Split('-');
      List<Morpheme> morphemes = new List<Morpheme>();

      foreach (var part in parts)
      {
         string[] labels = part.Split('.');

         foreach (var label in labels) {
            if (label == label.ToUpper()) {
               var abbreviation = DatabaseManager.GetAbbreviation(label);
               if (!Program.Abbreviations.Any(x => x.Label == label))
                  Program.Abbreviations.Add(abbreviation);
            }
         }

         if (gloss)
         {
            morphemes.Add(new Morpheme
            {
               Original = "",
               Gloss    = labels[0],
               Labels   = labels.Skip(1).ToArray()
            });
         }
         else
         {
            morphemes.Add(new Morpheme
            {
               Original = labels[0],
               Gloss    = "",
               Labels   = new string[] {}
            });
         }
      }

      return morphemes;
   }
}
