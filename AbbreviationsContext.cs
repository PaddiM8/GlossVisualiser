using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class AbbreviationsContext : DbContext 
{
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlite("Data Source=Resources/gp.db;");
   }

   public DbSet<Abbreviation> Abbreviations { get; set; }
}
