using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

class Program
{
   public static List<Abbreviation> Abbreviations = new List<Abbreviation>();

   private static List<String> InputFiles = new List<String>();
   private delegate string Output(List<Sentence> sentences);
   private static OutputTypes OutputType = OutputTypes.HtmlFull;
   private static OutputMethods OutputMethod;
   private static Output OutputFunction      { get; set; }

   public static void Main(string[] args)
   {
      // Parse input arguments and change the global options
      ParseArgs(args);

      // If no input file(s) specified, load all .gls files in the current directory
      if (InputFiles == null || InputFiles.Count == 0) 
         InputFiles = Directory.GetFiles(Environment.CurrentDirectory,
                                                              "*.gls").ToList();
      // If no output type is specified, grab the default one
      if (OutputFunction == null)
         OutputFunction = GetOutputFunction("");

      // Loop through each input file that is specified and parse it
      foreach (var inputFile in InputFiles) 
      {
         List<Token> tokens = new Lexer().Lex(File.ReadAllText(inputFile));
         var parse = new Parser().Parse(tokens);
         string output = "";
         output = OutputFunction(parse);
         string fileName = Path.GetFileNameWithoutExtension(inputFile);

         if (OutputType == OutputTypes.HtmlFull) 
         {
            new DictionaryManager().Load(DictionaryManager.DictionaryLocation); // Load dictionary into memory
            Directory.CreateDirectory(fileName);
            File.WriteAllText(fileName + "/index.html", output);          // Generate HTML
            File.WriteAllText(fileName + "/style.css", new CssGenerator() // Generate CSS
                                  .Generate());
            File.WriteAllText(fileName + "/script.js", new JSGenerator()  // Generate JavaScript
                                  .Generate());

            if (!File.Exists(fileName + "/Hack.ttf")) // Add font, if not already in the directory
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

   ///<summary>
   ///Parse input arguments and update the global option variables 
   ///</summary>
   private static void ParseArgs(string[] args) 
   {
      // Loop through each entry
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
                  new DictionaryManager().Save(DictionaryManager
                                              .DictionaryLocation);
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
                  Console.WriteLine(editedAbbreviation.Color);
                  new DictionaryManager().Save(DictionaryManager
                                              .DictionaryLocation);
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

   ///<summary>
   ///Show program help in console
   ///</summary>
   private static void ShowHelp() 
   {
      Console.WriteLine("-=GlossVisualiser Help=-");
      Console.WriteLine("-h, --help: Show help");
      Console.WriteLine("-t: Output type (json, html)");
      Console.WriteLine("-o: Output method (console, file)");
      Console.WriteLine("-ab, --add-abbreviation: [ABBREVIATION] [Color] [Value/Meaning]");
      Console.WriteLine("-eb, --edit-abreviation: [ABBREVIATION] [Color] [Value/Meaning]");
      Console.WriteLine("-d, --dictionary: Abbreviation-dictionary file location");
   }

   ///<summary>
   ///Get which output function is specified in argument
   ///</summary>
   private static Output GetOutputFunction(string input)  // TODO: Reduce code duplication
   {
      switch (input.ToLower()) 
      {
         case "html":
            return new Output(new HtmlGenerator().GenerateFull);
         case "json":
            return new Output(new JsonGenerator().Generate);
         default:
            return new Output(new HtmlGenerator().GenerateFull);
      }
   }

   ///<summary>
   ///Get which output type is specified in argument
   ///</summary>
   public static OutputTypes GetOutputType(string input) 
   {
      switch (input.ToLower()) 
      {
         case "html":
            return OutputTypes.HtmlFull;
         case "json":
            return OutputTypes.Json;
         default:
            return OutputTypes.HtmlFull;
      }
   }

   ///<summary>
   ///Get which output method is specified in argument
   ///</summary>
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

   ///<summary>
   ///Retrieve file extension needed for output type
   ///</summary>
   private static string OutputTypeToExtension(OutputTypes outputType) 
   {
      // This will eventually have a few more.
      switch (outputType) 
      {
         case OutputTypes.Json:
            return ".json";
         default:
            return ".txt";
      }
   }
}
