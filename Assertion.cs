using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLOL;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse assertion to a variable
    /// </summary>
    class Assertion : CodeElement
    {
        static int[] AllowedCodeElements = {
            (int)Toolbox.codeElements.Expression
        };

        protected override int[] _allowedCodeElements
            { get { return Assertion.AllowedCodeElements; } }
        
        public Assertion(Code code) : base(code, 0, 0) { }
        public Assertion(Code code, int offset) : base(code, offset, 0) { }
        public Assertion(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

        protected override void parse()
        {
            if (!this.matchRegexp(Toolbox.stdRegex[Toolbox.RegExpTemplates.assertion]).Success)
                throw new CodeElementNotFound(this._offset, this._code, Toolbox.stdRegex[Toolbox.RegExpTemplates.assertion].ToString());
            this.matchAllowedCodeElementsOnce(); 
        }

    }
}
