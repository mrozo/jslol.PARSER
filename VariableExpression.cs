using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    class VariableExpression : CodeElement
    {
        private String _identificator;
        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Parse()
        {
            this._identificator = this.matchMandatoryRegexp(Toolbox.stdRegex[Toolbox.RegExpTemplates.identificator]).Value;
        }

        public VariableExpression(Code code) : base(code, 0, 0) { }
        public VariableExpression(Code code, int offset) : base(code, offset, 0) { }
        public VariableExpression(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
