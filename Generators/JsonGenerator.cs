using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

public class JsonGenerator 
{

   ///<summary>
   ///Generate JSON from the sentences
   ///</summary>
   public string Generate(List<Sentence> sentences) 
   {
      var contractResolver = new DefaultContractResolver 
      {
         NamingStrategy = new CamelCaseNamingStrategy()
      };

      return JsonConvert.SerializeObject(sentences, new JsonSerializerSettings
      {
         ContractResolver = contractResolver
      });
   }
}
