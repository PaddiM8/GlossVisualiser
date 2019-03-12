using System;
using System.Linq;

public static class StringExtensions
{
   public static string FirstCharToUpper(this string input)
   {
      return input.First().ToString().ToUpper() + input.Substring(1);
   }
}
