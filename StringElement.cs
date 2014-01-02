using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    public class StringElement : CodeElement
    {
        static Regex StrValRE = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.stringValue]);
        private Match match;
        private String value;
        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Parse()
        {
            this.match = this.matchRegexp(StringElement.StrValRE);
            if (!match.Success)
                throw new CodeElementNotFound(this._offset, this._code, StringElement.StrValRE.ToString());

            this.value = this.match.Groups["value"].Value;
        }

        public StringElement(Code code) : base(code, 0, 0) { }
        public StringElement(Code code, int offset) : base(code, offset, 0) { }
        public StringElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
