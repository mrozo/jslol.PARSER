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
        public enum codeElements
        {
            Code
            ,Object
            ,Declaration
            ,Expression
            ,Number
            ,String
            ,MethodCall
            ,Comment
            ,Assertion
        };

        /// <summary>
        /// List of identificators for basic regexp sources
        /// </summary>
        public enum RegExpTemplates
        {
            identificator
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
        };

        /// <summary>
        /// basic Regexp sources for parsing and complex regexp creation.
        /// </summary>
        readonly public static Dictionary<RegExpTemplates, String> RegExpSources = new Dictionary<RegExpTemplates,string>() {
                {RegExpTemplates.identificator,@"[a-z_]+[a-z0-9_-]*"}
                ,{RegExpTemplates.advType,@"((\[[\t ]*\])|(\([\t ]*\))|({[\t ]*}))"}
                ,{RegExpTemplates.methodType,@"\(\[ \t]*\)"}
                ,{RegExpTemplates.objectStart,@"{"}
                ,{RegExpTemplates.objectStop,@"}"}
                ,{RegExpTemplates.whiteChar,@"[ \t]"}
                ,{RegExpTemplates.whiteCharNewL,@"([ \t\n]|\r\n)"}
                ,{RegExpTemplates.whiteChars,@"[ \t]+"}
                ,{RegExpTemplates.whiteCharsNewL,@"([ \t\n]|\r\n)+"}
                ,{RegExpTemplates.arrayStart,@"\["}
                ,{RegExpTemplates.arrayEnd,@"\]"}
                ,{RegExpTemplates.number,@"[+-]?[0-9]*\.[0-9]"}
                ,{RegExpTemplates.assertion,@"="}
                ,{RegExpTemplates.endOfInstruction,@";"}
                ,{RegExpTemplates.comment,@"#([^\n\r]*|(\\)*)*"}
                ,{RegExpTemplates.stringValue,"\"(?<value>[^\\\"]*|\\\\.)\""}

            };

        readonly public static Dictionary<RegExpTemplates, Regex> stdRegex;

        static Toolbox()
        {

            Toolbox.stdRegex = new Dictionary<RegExpTemplates, Regex>()
            {
                {RegExpTemplates.whiteChar, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteChar])},
                {RegExpTemplates.whiteCharNewL, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteCharNewL])},
                {RegExpTemplates.whiteChars, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteChars])},
                {RegExpTemplates.whiteCharsNewL, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.whiteCharsNewL])},
                {RegExpTemplates.endOfInstruction, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.endOfInstruction])},
                {RegExpTemplates.assertion, Toolbox.CreateRegex(Toolbox.RegExpSources[RegExpTemplates.assertion])}
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
        /// <returns></returns>
        public static CodeElement createCodeElement(int id, Code code, int offset,int indentionLevel)
        {
            try
            {
                offset += code.getWhiteChars(offset,true).Length;
                switch (id)
                {
                    case (int)Toolbox.codeElements.Declaration: return new Declaration(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.Expression: return new Expression(code, offset, indentionLevel); 
                    //case (int)Toolbox.codeElements.MethodCall:    return new (code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.Number: return new NumberElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.Object: return new ObjectElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.String: return new StringElement(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.Assertion: return  new Assertion(code, offset, indentionLevel); 
                    case (int)Toolbox.codeElements.Comment: return new Comment(code, offset, indentionLevel); 
                    default: throw new CriticalException("Internal parser exception : unknown code element id : "+id.ToString());
                }
            }
            catch (CodeElementNotFound){
                return null;
            }
      }


    }
}
