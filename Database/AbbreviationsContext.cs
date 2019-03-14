using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class AbbreviationsContext : DbContext 
{
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseSqlite("Data Source=Resources/gp.db;");
      SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
   }

   public DbSet<Abbreviation> Abbreviations { get; set; }
}
