using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLOL;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse assertion
    /// </summary>
    class Assertion : CodeElement
    {
        /*static Regex StartMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.assertion]);
        static Regex StopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.endOfInstruction]);
        */
        static int[] AllowedCodeElements = {
            (int)Toolbox.codeElements.Expression
        };

      

        /*protected override System.Text.RegularExpressions.Regex _startMarker
            { get { return Assertion.StartMarker;  } }

        protected override System.Text.RegularExpressions.Regex _stopMarker
            { get { return Assertion.StopMarker; } }*/

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
