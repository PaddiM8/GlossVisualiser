using System;

class Token
{
   public TokenTypes TokenType { get; set; }
   public string Value { get; set; }

   public Token(TokenTypes tokenType, string value)
   {
      this.TokenType = tokenType;
      this.Value = value;
   }
}
