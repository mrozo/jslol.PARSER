jslol.PARSER
============

JavaScript Like Object Language Parser - C# library to automatically translate JSLOL to C#.

# JSLOL Description

JSLOL is an attempt to create a perfect object oriented language. The target is to create language specified by following features : 
* everything is an object (even method),
* basic type control,
* support for classic inheritance (from one, or multiple parrents)
* support for dynamic class object modification (eg. method copy)

# Code example
Early JSLOL code exmple.
```
{
    std.num number = 0.2;   #comment 1
    std.str[] stringVar; #comment 2
    std.str() method = (std.str arg1, std.num arg2)
    {
        
    }
}
```

# Usage example
C# code.
```
using JSLOL.Parser;
namespace JSLOL.example
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeElement parsedCode = new SourceFile(source) as CodeElement;
        }
    }
}
```
