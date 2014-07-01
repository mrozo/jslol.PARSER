using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#if DEBUG
using System.Reflection;
using System.Diagnostics;
#endif

namespace JSLOL.Parser
{

    /// <summary>
    /// Base class for evry parsing object that provides basic set of tools.
    /// </summary>
    public abstract class CodeElement
    {
        protected Code _code;
        protected int _indentLevel;
        protected String _indention;
        abstract protected int[] _allowedCodeElements { get; }

        /// <summary>
        /// Variable to store starting point of the parsed expression
        /// </summary>
        protected int _startOffset = 0;
        /// <summary>
        /// Variable to store ending point of the parsed expression
        /// </summary>
        protected int _stopOffset = 0;
        /// <summary>
        /// List of code element objects found inside.
        /// </summary>
        protected List <CodeElement> codeElements = new List<CodeElement>();
        /// <summary>
        /// Current code offset for regex matching
        /// </summary>
        protected int _offset = 0 ;
        /// <summary>
        /// offset used to parse code
        /// </summary>
        public int offset {
            get { return this._offset; }
            protected set{ this._offset = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">The Code object</param>
        /// <param name="offset">Number of chars that has been alreadey parsed</param>
        /// <param name="indentionLevel">Code indention level aka. number of '\t'</param>
        public CodeElement(Code code, int offset, int indentionLevel)
        {
            this._code = code;
            this._offset = offset;
            this._startOffset = offset;
            this._indentLevel = indentionLevel;
            this._indention = new String('\t', this._indentLevel);
            this.Parse(); //throws CodeElementNotFound exception
            this._stopOffset = this._offset;

#if DEBUG
            String codeFragment ;
            if (this._stopOffset - this._startOffset > 30)
                codeFragment = this._code.source.Substring(this._startOffset, 15) + " (...) " + this._code.source.Substring(this._stopOffset - 8, 8);
            else
                codeFragment = this._code.source.Substring(this._startOffset, this._stopOffset - this._startOffset);
            codeFragment = codeFragment.Replace("\r\n","\\n");
            Debug.WriteLine(String.Format("offset : {0} to {1} :: Found {2} : '{3:20}'",
                this._startOffset,
                this._stopOffset,
                this.GetType().Name,
                codeFragment
            ));
#endif

        }


        /// <summary>
        /// Match selected CodeElement indentified by eID When successfull add it to codeElements list and icrease parsing offset
        /// </summary>
        /// <param name="eID">ID of code element to match</param>
        /// <param name="mandatory">If true method drops CodeElementNotFound exception on failure</param>
        /// <returns></returns>
        protected CodeElement matchCodeElement(int eID, bool mandatory)
        {
            CodeElement e = Toolbox.createCodeElement(eID, this._code, this._offset, this._indentLevel);
            if (e != null)
            {
                this.codeElements.Add(e);
                this._offset = e.offset;
            }
            else if (mandatory)
                throw new CodeElementNotFound();
            return e;
        }

        /// <summary>
        /// Match selected CodeElement indentified by eI. When successfull add it to codeElements list and icrease parsing offset. Alias for CodeElement MatchCodeElement(int,bool)
        /// </summary>
        /// <param name="eID">code element identificator taken from enum Toolbox.codeElements</param>
        /// <returns></returns>
        protected CodeElement matchCodeElement(int eID)
        {
            return this.matchCodeElement(eID, false);
        }

        /// <summary>
        /// Match selected CodeElement indentified by eI. When successfull add it to codeElements list and icrease parsing offset otherwise throws CodeElementNotFound exception. Alias for CodeElement MatchCodeElement(int,bool)
        /// </summary>
        /// <param name="eID">code element identificator taken from enum Toolbox.codeElements</param>
        /// <returns></returns>
        protected CodeElement matchMandatoryCodeElement(int eID)
        {
            return this.matchCodeElement(eID, true);
        }


        /// <summary>
        /// iterate over _allowedCodeElements property and stops on first match. When no match returns null
        /// </summary>
        /// <returns>matched code element or null on failure</returns>
        protected CodeElement matchAllowedCodeElement()
        {
            return this.MatchCodeElemntFromArray(this._allowedCodeElements);
        }

        /// <summary>
        /// Iterates over given array of code elements ID's and stops on first match.
        /// </summary>
        /// <param name="arr">array of CodeElements ID's from Toolbox.codeElements</param>
        /// <returns>Matched code element object or NULL</returns>
        protected CodeElement MatchCodeElemntFromArray(int[] arr)
        {
            CodeElement e = null;
            foreach (int eId in arr)
                if ((e = this.matchCodeElement(eId)) != null)
                    return e;

            return null;
        }

        /// <summary>
        /// skips white chars in code
        /// </summary>
        protected void skipWhiteChars() {
            this.skipWhiteChars(false);
        }

        /// <summary>
        /// skips white chars. When includeNewLines is true also skips new line chars.
        /// </summary>
        /// <param name="includeNewLines"></param>
        protected void skipWhiteChars(bool includeNewLines)
        {
            this.offset+=this._code.getWhiteChars(this._offset,includeNewLines).Length;
        }


        /// <summary>
        /// Matches <![CDATA[<endofinstructionmarker>]]>
        /// </summary>
        protected void matchEndOfInstructionMarker()
        {
            this.matchMandatoryRegexp(Toolbox.stdRegex[Toolbox.RegExpTemplates.endOfInstruction],true);
        }


        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.throws CodeElementNotFound exception onf failure
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <param name="skipWhiteChars">skip white chars before the element</param>
        /// <returns>matched data</returns>
        protected Match matchMandatoryRegexp(Regex re,bool skipWhiteChars)
        {
            return this.matchRegexp(re, skipWhiteChars, true);
        }

        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.throws CodeElementNotFound exception onf failure
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <returns>matched data</returns>
        protected Match matchMandatoryRegexp(Regex re)
        {
            return this.matchRegexp(re,false,true);
        }

        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <returns>matched data</returns>
        protected Match matchRegexp(Regex re)
        {
            return this.matchRegexp(re,false,false);
        }

        /// <summary>
        /// Matches given Regex object against the code with current offset. Icrease offset on success
        /// </summary>
        /// <param name="re">Regex to match</param>
        /// <param name="skipWhiteChars">Allows presence of white chars before matching</param>
        /// <param name="mandatory">Indicates if method throws CodeElementNotFound exception on failure</param>
        /// <returns>Match object</returns>
        protected Match matchRegexp(Regex re,bool skipWhiteChars,bool mandatory)
        {
            if (skipWhiteChars)
                this.skipWhiteChars(true);
            
            Match m = this._code.match(re, this._offset);
            if (mandatory && !m.Success)
                throw new CodeElementNotFound(this._offset, this._code, re.ToString());

            this._offset += m.Length;
            return m;
        }

        /// <summary>
        /// Calls toCSHarp() for evry contained CodeElement
        /// <param name="ns">namespace to put code in</param>
        /// <returns>C# code returned by calls</returns>
        virtual protected String subobjectsToCSharp(String ns)
        {
            String cSharpCode = "";
            foreach (CodeElement e in this.codeElements)
                cSharpCode += e.toCSharp(ns);
            return cSharpCode;
        }

        /// <summary>
        /// The main methot that parses the code. 
        /// </summary>
        protected abstract void Parse();

        /// <summary>
        /// Parse object and all its subobjects to C# code
        /// </summary>
        /// <returns>C# code</returns>
        public virtual String toCSharp(String ns)
        {
            throw new NotImplementedException();
        }


    }
}
