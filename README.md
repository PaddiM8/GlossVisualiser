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
When you run the binary, it will check the current directory for \*.gls files and convert them to HTML automatically. You can also specify the file name directly after `dotnet run` if you wish to parse a single file.

## Compiling
If you choose to compile the program yourself, you will need .NET Core SDK installed. 
Simply do `dotnet run` in a terminal/command line to build and run it. 

## Custom abbreviations 
Adding custom abbreviations is simple. Use the flag `--add-abbreviation` or `-ab` to add an abbreviation to your local abbreviation database.  
`dotnet run -ab [ABBREVIATION] [Color] [Value/Explanation]`  
The color must be a HEX string(without the hash symbol).
**Example:** `dotnet run -ab INE 0F0F0F Inessive case ('in')`
