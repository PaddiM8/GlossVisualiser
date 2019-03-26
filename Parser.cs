using System;
using System.Collections.Generic;
using System.Linq;

class Parser
{
   ///<summary>
   ///Parse input and create a list of "Sentences"
   public List<Sentence> Parse(List<Token> tokens)
   {
      List<Sentence> sentences = new List<Sentence>();
      sentences.Add(new Sentence(new List<Word>()));

      int currentSentence = 0;
      int currentGlossWord = 0;
      bool inGloss = false;

      foreach (var token in tokens)
      {
         if (token.TokenType == TokenTypes.Word && !inGloss) // If it's a word in the original sentence
         {
            var word = new Word(SplitUpWord(token.Value, false));
            sentences[currentSentence].Words.Add(word);
         }
         else if (token.TokenType == TokenTypes.Word && inGloss) // If it's a word in the gloss
         {
            var word = sentences[currentSentence].Words[currentGlossWord];
            var morphemes = SplitUpWord(token.Value, true);

            for (int wp = 0; wp < word.Morphemes.Count; wp++) // Add each glossed word and all labels to the current word
            {
               word.Morphemes[wp].Gloss = morphemes[wp].Gloss;
               word.Morphemes[wp].Labels = morphemes[wp].Labels;
            }

            currentGlossWord++;
         }
         else if (token.TokenType == TokenTypes.OpenBracket) // If {, update inGloss variable to show that it's currently inside a gloss section
         {
            inGloss = true;
         }
         else if (token.TokenType == TokenTypes.ClosedBracket) // If }, update to show that it's currently not inside a gloss section
         {
            sentences.Add(new Sentence(new List<Word>())); // Prepare a new sentence to fill out
            currentSentence++;
            currentGlossWord = 0;
            inGloss = false;
         }
      }

      return sentences;
   }

   ///<summary>
   ///Split up a word in morphemes and labels
   ///</summary>
   private List<Morpheme> SplitUpWord(string word, bool gloss)
   {
      string[] parts = word.Split('-');
      List<Morpheme> morphemes = new List<Morpheme>();

      // Split up each label
      foreach (var part in parts)
      {
         string[] labels = part.Split('.');

         foreach (var label in labels) {
            if (label == label.ToUpper()) {
               if (!Program.Abbreviations.Any(x => x.Label == label)) { // If not already existing, add the label to the label list so it will be colored
                  var abbreviation = new Abbreviation(label, "0000", "value");
                  Program.Abbreviations.Add(abbreviation);
               }
            }
         }

         if (gloss) // If it's a gloss morpheme
         {
            morphemes.Add(new Morpheme
            {
               Original = "",
               Gloss    = labels[0],
               Labels   = labels.Skip(1).ToArray()
            });
         }
         else // If it's from an original word
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
