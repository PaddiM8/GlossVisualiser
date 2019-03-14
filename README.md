# GlossVisualiser
Parses interlinear gloss and outputs HTML that shows it in a more readable, visual way.  
Hover a morpheme to see its meaning and how it affects the sentence grammatically, without needing to know how to read gloss.  
[Download](https://github.com/PaddiM8/GlossVisualiser/releases)  
[Demo](https://paddi.science/tings/glossvisualiser/)

![](https://i.imgur.com/JeeBPur.png)

# Usage
## Input format:
original sentence with morphemes separated { gloss } another sentence { gloss } *etc.*  
**Example:**
on talo-ssa { be.1SG.PRS house-INE } koira-t juokse-vat { dog-PL run.IMP-3PL }

## Running the program
When you run the executable, it will check the current directory for \*.gls files and convert them to HTML automatically. You can also specify the file name directly after the command to run the executable, if you wish to parse a single file. This would be done in a terminal/command line.

**Example:**  
File: gloss.gls  
File content: on talo-ssa { be.1SG.PRS house-INE }  
To convert: place it in the same directory as the program, run the executable.  
*or* run the program from a terminal/command line, which also allows you to set additional options and add to/edit the abbreviation database.

## Compiling
If you choose to compile the program yourself, you will need .NET Core SDK installed. 
Simply do `dotnet run` in a terminal/command line to build and run it. 

## Custom abbreviations 
Adding custom abbreviations is simple. Use the flag `--add-abbreviation` or `-ab` to add an abbreviation to your local abbreviation database.  
`-ab [ABBREVIATION] [Color] [Value/Explanation]`  
The color must be a HEX string(without the hash symbol).
**Example:** `-ab INE 0F0F0F Inessive case ('in')`
