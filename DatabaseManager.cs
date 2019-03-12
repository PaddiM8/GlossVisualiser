using System;
using System.Linq;
using System.Collections.Generic;

public static class DatabaseManager
{
   public static Abbreviation GetAbbreviation(string label) 
   {
      try
      {
         using (var db = new AbbreviationsContext())
            return db.Abbreviations.Where(a => a.Label == label).Single();
      }
      catch (Exception ex) 
      {
         Console.WriteLine(ex.ToString());
         Console.WriteLine($"Error. Are you sure {label} is in the abbreviation database?");
         System.Environment.Exit(1);
         return null;
      }
   }
}
