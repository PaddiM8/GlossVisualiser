using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

class MainClass {
   public static void Main (string[] args)
   {
      List<Token> tokens = new Lexer().Lex("on talo-ssa { be.1SG.PRS house-INE } | koira-t juokse-vat { dog-PL run.IMP-3PL }");
      var parse = new Parser().Parse(tokens);

      var contractResolver = new DefaultContractResolver
      {
         NamingStrategy = new CamelCaseNamingStrategy()
      };

      string json = JsonConvert.SerializeObject(parse, new JsonSerializerSettings
      {
         ContractResolver = contractResolver
      });

      Console.WriteLine(json);
   }
}
