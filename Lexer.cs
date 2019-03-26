using System;
using System.Collections.Generic;

class Lexer
{
   ///<summary>
   ///Lex input and add each token to a list
   public List<Token> Lex(string input)
   {
      List<Token> tokens = new List<Token>();
      
      // TODO: Make this just one for loop
      for (int c = 0; c < input.Length; c++) // Stop at each word or new section
      {
         for (int w = 0; w < input.Length; w++) // Loop through each character for every word or new section
         {
            if (c+w == 0 && IsWhitespace(input[c+w])) break; // Ignore, if the first character is whitespace
            if (IsWhitespace(input[c+w]) && IsWhitespace(input[c+w-1])) break; // If current character is whitespace and last one was too, ignore

            if (input[c+w] == ' ' || input[c+w] == '\n') // If new word or section
            {
               tokens.Add(new Token {
                  TokenType = GetTokenType(input[c]),
                  Value     = input.Substring(c, w)
               });

               c = c + w; // Skip to the next word or section
               break;
            } 
            else if (c+w == input.Length - 1) // If last character
            {
               tokens.Add(new Token {
                  TokenType = GetTokenType(input[c]),
                  Value     = input.Substring(c, w + 1)
               });

               c = input.Length - 1;
               break;
            }
         } 
      }

      return tokens;
   }

   ///<summary>
   ///Chec if character is whitespace or new line
   ///</summary>
   private bool IsWhitespace(char c) 
   {
      return c == ' ' || c == '\n' || c == '\t';
   }

   ///<summary>
   ///Get token type from character
   ///</summary>
   private TokenTypes GetTokenType(char c)
   {
      switch (c)
      {
         case '{':
            return TokenTypes.OpenBracket;
         case '}':
            return TokenTypes.ClosedBracket;
         case '|':
            return TokenTypes.SentenceDivider;
         default:
            return TokenTypes.Word;
      }
   }
}
