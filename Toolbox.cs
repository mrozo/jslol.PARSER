using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Toolbox class that stores common data and provides some supporting methods
    /// </summary>
    public static class Toolbox
    {
        
        /// <summary>
        /// list of supported code elements
        /// </summary>
        public enum codeElement
        {
            Code
            ,Object
            ,Declaration
            ,FieldDeclaration
            ,Expression
            ,Number
            ,String
            ,MethodCall
            ,Comment
            ,Assertion
            ,MethodExpression
            ,ArgumentsDeclarationsList
            ,BlockOfCode
            ,Instruction
            ,ArgumentsList
            ,VariableExpression
            ,IfInstruction
            ,WhileInstruction
        };

        /// <summary>
        /// List of identificators for basic regexp sources
        /// </summary>
        public enum RegExpTemplates
        {
            name
            ,identificator
            ,advType
            ,methodType
            ,objectStart
            ,objectStop
            ,whiteChar
            ,whiteCharNewL
            ,whiteChars
            ,whiteCharsNewL
            ,arrayStart
            ,arrayEnd
            ,number
            ,assertion
            ,endOfInstruction
            ,comment
            ,stringValue
            ,methodExpressionMarker
            ,argumentListStartMarker
            ,argumentListStopMarker
            ,blockOfCodeStartMarker
            ,blockOfCodeStopMarker
            ,argumentListSeparator
            ,controlInstructionArgumentStartMarker
            ,controlInstructionArgumentStopMarker
        };

        /// <summary>
        /// basic Regexp sources for parsing and complex regexp creation.
        /// </summary>
        readonly public static Dictionary<RegExpTemplates, String> RegExpSources = new Dictionary<RegExpTemplates,string>() {
                {RegExpTemplates.name,@"[a-z_]+[a-z0-9_-]*"}
                ,{RegExpTemplates.identificator,@"[a-z_]+[a-z0-9_-]*(\.[a-z_]+[a-z0-9_-]*)*"}
                ,{RegExpTemplates.advType,@"((\[[\t ]*\])|(\([\t ]*\))|({[\t ]*}))"}
                ,{RegExpTemplates.methodType,@"\(\[ \t]*\)"}
                ,{RegExpTemplates.objectStart,@"{"}
                ,{RegExpTemplates.objectStop,@"}"}
                ,{RegExpTemplates.whiteChar,@"[ \t]"}
                ,{RegExpTemplates.whiteCharNewL,@"[ \t\n\r]"}
                ,{RegExpTemplates.whiteChars,@"[ \t]+"}
                ,{RegExpTemplates.whiteCharsNewL,@"[ \t\n\r]+"}
                ,{RegExpTemplates.arrayStart,@"\["}
                ,{RegExpTemplates.arrayEnd,@"\]"}
                ,{RegExpTemplates.number,@"[+-]?[0-9]+(\.[0-9]*)?"}
                ,{RegExpTemplates.assertion,@"="}
                ,{RegExpTemplates.endOfInstruction,@";"}
                ,{RegExpTemplates.comment,@"#([^\n\r]*|(\\)*)*"}
                ,{RegExpTemplates.stringValue,"\"(?<value>[^\\\"]*|\\\\.)\""}
                ,{RegExpTemplates.methodExpressionMarker,@"m"}
                ,{RegExpTemplates.argumentListStartMarker,@"\("}
                ,{RegExpTemplates.argumentListStopMarker,@"\)"}
                ,{RegExpTemplates.blockOfCodeStartMarker,@"{"}
                ,{RegExpTemplates.blockOfCodeStopMarker,@"}"}
                ,{RegExpTemplates.argumentListSeparator,@","}
                ,{RegExpTemplates.controlInstructionArgumentStartMarker,@"\("}
                ,{RegExpTemplates.controlInstructionArgumentStopMarker,@"\)"}

            };

        readonly public static Dictionary<RegExpTemplates, Regex> stdRegex;

        static Toolbox()
        {

            Toolbox.stdRegex = new Dictionary<RegExpTemplates, Regex>()
            {
                {RegExpTemplates.whiteChar, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteChar])}
                ,{RegExpTemplates.whiteCharNewL, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteCharNewL])}
                ,{RegExpTemplates.whiteChars, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteChars])}
                ,{RegExpTemplates.whiteCharsNewL, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteCharsNewL])}
                ,{RegExpTemplates.endOfInstruction, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.endOfInstruction])}
                ,{RegExpTemplates.assertion, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.assertion])}
                ,{RegExpTemplates.identificator, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.identificator])}
                ,{RegExpTemplates.name, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.name])}
            };

        }

        /// <summary>
        /// Creates regexp object to use with the parser
        /// </summary>
        /// <param name="id">RegExp template id from RegExpTemplates</param>
        /// <returns>Regex object</returns>
        static public Regex CreateRegex(RegExpTemplates id)
        {
            return Toolbox.CreateRegex(Toolbox.RegExpSources[id]);
        }

        /// <summary>
        /// Creates regexp object to use with the parser. Method prepends source with "\G" flag and 
        /// </summary>
        /// <param name="source">source of the regexp(don't use \G falg!)</param>
        /// <returns>Regexp object</returns>
        static public Regex CreateRegex(String source)
        {
            return new Regex(@"\G"+source,
                    RegexOptions.ExplicitCapture | RegexOptions.Multiline |
                    RegexOptions.Singleline | RegexOptions.CultureInvariant |
                    RegexOptions.IgnoreCase | RegexOptions.Compiled
                    );
        }

        /// <summary>
        /// Tries to create code element selected by id. Returns null on CodeElementNotFound.  
        /// </summary>
        /// <param name="id">Code element identificator taken from enum Toolbox.codeElements</param>
        /// <param name="code">The Code object to pass as an argument</param>
        /// <param name="offset">The offset to pass as an argument</param>
        /// <returns>Code element object identified by id or NULL on failure</returns>
        public static CodeElement createCodeElement(int id, Code code, int offset,int indentionLevel)
        {
            try
            {
                offset += code.getWhiteChars(offset,true).Length;
                switch (id)
                {
                    case (int)Toolbox.codeElement.Declaration: return new Declaration(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.Expression: return new Expression(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.ArgumentsList: return new ArgumentsList(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.MethodCall:    return new MethodCall(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElement.Number: return new NumberElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElement.Object: return new ObjectElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElement.String: return new StringElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElement.Assertion: return  new Assertion(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElement.Comment: return new Comment(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.MethodExpression: return new MethodExpression(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.FieldDeclaration: return new FieldDeclaration(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.ArgumentsDeclarationsList: return new ArgumentsDeclarationsList(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.BlockOfCode: return new BlockOfCode(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.Instruction: return new Instruction(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.VariableExpression: return new VariableExpression(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.IfInstruction: return new IfInstruction(code, offset, indentionLevel);
                    case (int)Toolbox.codeElement.WhileInstruction: return new WhileInstruction(code, offset, indentionLevel);
                    
                    default: throw new CriticalException("Internal parser exception : unknown code element id : "+id.ToString());
                }
            }
            catch (CodeElementNotFound){
                return null;
            }
      }


    }
}
