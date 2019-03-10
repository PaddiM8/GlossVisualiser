using System;
using System.Collections.Generic;

class Lexer
{
   public List<Token> Lex(string input)
   {
      List<Token> tokens = new List<Token>();

      for (int c = 0; c < input.Length; c++)
      {
         for (int w = 0; w < input.Length; w++)
         {
            if (input[c+w] == ' ' || input[c+w] == '\n')
            {
               tokens.Add(new Token {
                  TokenType = GetTokenType(input[c]),
                  Value     = input.Substring(c, w)
               });

               c = c + w;
               break;
            } 
            else if (c+w == input.Length - 1)
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
