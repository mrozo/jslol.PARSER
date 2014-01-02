using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#if DEBUG
using System.Reflection;
#endif

namespace JSLOL.Parser
{

    /// <summary>
    /// Base class for evry parsing object that provides basic set of tools.
    /// </summary>
    public abstract class CodeElement
    {
        protected Code _code;
        protected int _indentionLevel;
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
            this._indentionLevel = indentionLevel;
            this.Parse(); //throws CodeElementNotFound exception
            this._stopOffset = this._offset;
#if DEBUG
            Console.WriteLine("offset : {0} :: Found {1} : '{2}'",
                this.offset,
                this.GetType().Name,
                this._code.source.Substring(this._startOffset,this._stopOffset-this._startOffset)
            );
#endif

        }

        /// <summary>
        /// Match selected CodeElement indentified by eI. When successfull add it to codeElements list and icrease parsing offset
        /// </summary>
        /// <param name="eId">code element identificator taken from enum Toolbox.codeElements</param>
        /// <returns></returns>
        protected CodeElement matchCodeElement(int eId)
        {
            CodeElement e = Toolbox.createCodeElement(eId, this._code, this._offset, this._indentionLevel);
            if (e != null)
            {
                this.codeElements.Add(e);
                this._offset = e.offset;
            }
            return e;
        }

        /// <summary>
        /// iterate over _allowedCodeElements property and stops on first match. When no match returns null
        /// </summary>
        /// <returns>matched code element or null on failure</returns>
        protected CodeElement matchAllowedCodeElementsOnce()
        {
            return this.MatchCodeElemntFromArrayOnce(this._allowedCodeElements);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr">array of CodeElements ID's from Toolbox.codeElements</param>
        /// <returns></returns>
        protected CodeElement MatchCodeElemntFromArrayOnce(int[] arr)
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
        /// 
        /// </summary>
        protected void matchEndOfInstructionMarker()
        {

            Match m = this.matchRegexp(Toolbox.stdRegex[Toolbox.RegExpTemplates.endOfInstruction]);
            if (m.Length == 0)
                throw new CodeElementNotFound(this._offset, this._code, Toolbox.stdRegex[Toolbox.RegExpTemplates.endOfInstruction].ToString());
        }


        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.throws CodeElementNotFound exception onf failure
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <param name="skipWhiteChars">skip white chars before the element</param>
        /// <returns>matched data</returns>
        protected Match matchMandatoryRegexp(Regex re,bool skipWhiteChars)
        {
            if (skipWhiteChars)
                this.skipWhiteChars();
            Match m = this._code.match(re, this._offset);
            if (!m.Success)
                throw new CodeElementNotFound(this._offset, this._code, re.ToString());
            this._offset += m.Length;
            return m;
        }

        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.throws CodeElementNotFound exception onf failure
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <returns>matched data</returns>
        protected Match matchMandatoryRegexp(Regex re)
        {
            return this.matchMandatoryRegexp(re,false);
        }

        /// <summary>
        /// Matches regular expression against the code, automatticaly increases _offset.
        /// </summary>
        /// <param name="re">regular expression to match</param>
        /// <returns>matched data</returns>
        protected Match matchRegexp(Regex re)
        {
            Match m = this._code.match(re, this._offset);
            this._offset += m.Length;
            return m;
        }

        /// <summary>
        /// The main methot that parses the code.
        /// </summary>
        protected abstract void Parse();


    }
}
