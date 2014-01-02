using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    public class StringElement : CodeElement
    {
        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void parse()
        {
            throw new NotImplementedException();
        }

        public StringElement(Code code) : base(code, 0, 0) { }
        public StringElement(Code code, int offset) : base(code, offset, 0) { }
        public StringElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
