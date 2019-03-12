using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;
using System.Linq;

class Program
{
   public static List<Abbreviation> Abbreviations = new List<Abbreviation>();

   private static string[] InputFiles              { get; set; }
   private delegate string Output(List<Sentence> sentences);
   private static OutputTypes OutputType = OutputTypes.HtmlFull;
   private static OutputMethods OutputMethod       { get; set; }
   private static Output OutputFunction            { get; set; }

   public static void Main(string[] args)
   {
      ParseArgs(args);

      if (InputFiles == null || InputFiles.Length == 0) 
         InputFiles = Directory.GetFiles(Environment.CurrentDirectory,
                                                              "*.gls");
      if (OutputFunction == null)
         OutputFunction = GetOutputFunction("");

      foreach (var inputFile in InputFiles) 
      {
         List<Token> tokens = new Lexer().Lex(File.ReadAllText(inputFile));
         var parse = new Parser().Parse(tokens);

         string output = "";
         output = OutputFunction(parse);
         string fileName = Path.GetFileNameWithoutExtension(inputFile);

         if (OutputType == OutputTypes.HtmlFull) 
         {
            Directory.CreateDirectory(fileName);
            File.WriteAllText(fileName + "/index.html", output);
            File.WriteAllText(fileName + "/style.css", new CssGenerator()
                                  .Generate(Abbreviations));
            File.WriteAllText(fileName + "/script.js", new JSGenerator()
                                  .Generate(Abbreviations));

            if (!File.Exists(fileName + "/Hack.ttf"))
               File.Copy("Resources/Hack.ttf", fileName + "/Hack.ttf");
            continue;
         }

         if (OutputMethod == OutputMethods.Console)
            Console.WriteLine(output);
         else if (OutputMethod == OutputMethods.File)
            File.WriteAllText(fileName +
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
         case "html-full":
            return new Output(new HtmlGenerator().GenerateFull);
         case "html-spans":
            return new Output(new HtmlGenerator().GenerateSpans);
         case "html-div":
            return new Output(new HtmlGenerator().GenerateDiv);
         case "json":
            return new Output(new JsonGenerator().Generate);
         default:
            return new Output(new HtmlGenerator().GenerateFull);
      }
   }
   
   private static OutputTypes GetOutputType(string input) 
   {
      switch (input.ToLower()) 
      {
         case "html-full":
            return OutputTypes.HtmlFull;
         case "html-spans":
            return OutputTypes.HtmlSpans;
         case "html-div":
            return OutputTypes.HtmlDiv;
         case "json":
            return OutputTypes.Json;
         default:
            return OutputTypes.HtmlFull;
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
