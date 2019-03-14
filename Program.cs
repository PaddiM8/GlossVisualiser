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

   private static List<String> InputFiles = new List<String>();
   private delegate string Output(List<Sentence> sentences);
   private static OutputTypes OutputType = OutputTypes.HtmlFull;
   private static OutputMethods OutputMethod { get; set; }
   private static Output OutputFunction      { get; set; }

   public static void Main(string[] args)
   {
      ParseArgs(args);

      if (InputFiles == null || InputFiles.Count == 0) 
         InputFiles = Directory.GetFiles(Environment.CurrentDirectory,
                                                              "*.gls").ToList();
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
            new DictionaryManager().Load(DictionaryManager.DictionaryLocation);
            Directory.CreateDirectory(fileName);
            File.WriteAllText(fileName + "/index.html", output);
            File.WriteAllText(fileName + "/style.css", new CssGenerator()
                                  .Generate());
            File.WriteAllText(fileName + "/script.js", new JSGenerator()
                                  .Generate());

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
               case "-h":
               case "--help":
                  ShowHelp();
                  Environment.Exit(1);
                  break;
               case "-t":
                  OutputType = GetOutputType(args[i+1]);
                  OutputFunction = GetOutputFunction(args[i+1]);
                  break;
               case "-o":
                  OutputMethod = GetOutputMethod(args[i+1]);
                  break;
               case "-ab":
               case "--add-abbreviation":
                  var abbreviation = new Abbreviation(
                        args[i+1], 
                        string.Join(" ", args.Skip(3)),
                        args[i+2]
                  );

                  DictionaryManager.AbbreviationDictionary.Add(args[i+1],
                                                           abbreviation);
                  Environment.Exit(1);
                  break;
               case "-eb":
               case "--edit-abbreviation":
                  var editedAbbreviation = new Abbreviation(
                        args[i+1], 
                        string.Join(" ", args.Skip(3)),
                        args[i+2]
                  );

                  DictionaryManager.AbbreviationDictionary[args[i+1]]
                                                      = editedAbbreviation;
                  Environment.Exit(1);
                  break;
               case "-d":
               case "--dictionary":
                  DictionaryManager.DictionaryLocation = args[i+1];
                  break;
               case "--generate-dictionary":
               case "-gd":
                  new DictionaryManager().CreateFromFile(args[i+1]);
                  break;
               default:
                  continue;
            }

            i++;
            continue;
         }

         InputFiles.Add(args[i]);
      }
   }

   private static void ShowHelp() 
   {
      Console.WriteLine("-=GlossVisualiser Help=-");
      Console.WriteLine("-h, --help: Show help");
      Console.WriteLine("-t: Output type (json, html, html-spans, html-div)");
      Console.WriteLine("-o: Output method (console, file)");
      Console.WriteLine("-ab, --add-abbreviation: [ABBREVIATION] [Color] [Value/Meaning]");
      Console.WriteLine("-eb, --edit-abreviation: [ABBREVIATION] [Color] [Value/Meaning]");
      Console.WriteLine("-d, --dictionary: Abbreviation-dictionary file location");
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

   public static OutputTypes GetOutputType(string input) 
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
