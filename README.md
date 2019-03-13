# GlossVisualiser
Parses linear gloss and outputs HTML that shows it in a more readable, visual way.  
Hover a morpheme to see its meaning and how it affects the sentence grammatically, without needing to know how to read gloss.

![](https://i.imgur.com/JeeBPur.png)

# Usage
## Input format:
original sentence with morphemes separated { gloss } another sentence { gloss } *etc.*  
**Example:**
on talo-ssa { be.1SG.PRS house-INE } koira-t juokse-vat { dog-PL run.IMP-3PL }

## Running the program
To run the program you need .NET Core installed.  
Do `dotnet run` to run the program. This will check the current directory for \*.gls files and will convert them to HTML automatically. You can also specify the file name directly after `dotnet run` if you wish to parse a single file.

## Custom abbreviations 
Adding custom abbreviations is simple. Use the flag `--add-abbreviation` or `-ab` to add an abbreviation to your local abbreviation database.  
`dotnet run -ab [ABBREVIATION] [Color] [Value/Explanation]`  
The color must be a HEX string(without the hash symbol).
**Example:** `dotnet run -ab INE 0F0F0F Inessive case ('in')`
