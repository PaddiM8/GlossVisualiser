using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;

class Program
{
   private static OutputTypes OutputType = OutputTypes.Json;
   private static string[] InputFiles { get; set; }
   private static OutputMethods OutputMethod { get; set; }
   private delegate string Output(List<Sentence> sentences);
   private static Output OutputFunction { get; set; }

   public static void Main(string[] args)
   {
      ParseArgs(args);

      if (InputFiles == null || InputFiles.Length == 0) 
      {
         InputFiles = Directory.GetFiles(Environment.CurrentDirectory,
                                                              "*.gls");
      }

      foreach (var inputFile in InputFiles) 
      {
         List<Token> tokens = new Lexer().Lex(File.ReadAllText(inputFile));
         var parse = new Parser().Parse(tokens);

         string output = "";
         output = OutputFunction(parse);

         if (OutputMethod == OutputMethods.Console)
            Console.WriteLine(output);
         else if (OutputMethod == OutputMethods.File)
            File.WriteAllText(Path.GetFileNameWithoutExtension(inputFile) +
                                        OutputTypeToExtension(OutputType), 
                                        output);
      }
   }


   private static void ParseArgs(string[] args) 
   {
      for (int i = 0; i < args.Length; i++) 
      {
         if (args[i][0] == '-') 
         {
            switch (args[i]) 
            {
               case "-t":
                  OutputType = GetOutputType(args[i+1]);
                  OutputFunction = GetOutputFunction(args[i+1]);
                  break;
               case "-o":
                  OutputMethod = GetOutputMethod(args[i+1]);
                  break;
               default:
                  continue;
            }

            i++;
            continue;
         }

         InputFiles[InputFiles.Length] = args[i];
      }
   }

   private static Output GetOutputFunction(string input) 
   {
      switch (input.ToLower()) 
      {
         case "html-spans":
            return new Output(new HtmlGenerator().GenerateSpans);
         case "html-div":
            return new Output(new HtmlGenerator().GenerateDiv);
         case "json":
            return new Output(new JsonGenerator().Generate);
         default:
            return new Output(new JsonGenerator().Generate);
      }
   }
   
   private static OutputTypes GetOutputType(string input) 
   {
      switch (input.ToLower()) 
      {
         case "html-spans":
            return OutputTypes.HtmlSpans;
         case "html-div":
            return OutputTypes.HtmlDiv;
         case "json":
            return OutputTypes.Json;
         default:
            return OutputTypes.Json;
      }
   }

   private static OutputMethods GetOutputMethod(string input) 
   {
      switch (input.ToLower()) 
      {
         case "console":
            return OutputMethods.Console;
         case "file":
            return OutputMethods.File;
         default:
            return OutputMethods.Console;
      }
   }

   private static string OutputTypeToExtension(OutputTypes outputType) 
   {
      switch (outputType) 
      {
         case OutputTypes.HtmlSpans:
            return ".html";
         case OutputTypes.Json:
            return ".json";
         default:
            return ".txt";
      }
   }
}
