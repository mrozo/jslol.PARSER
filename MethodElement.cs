using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class used to parse method expression eg. " <![CDATA[m(<argumentsList> { <blockOfCode> } ]]>")
    /// </summary>
    class MethodElement : CodeElement
    {

        protected override int[] _allowedCodeElements
            { get { throw new NotImplementedException(); } }

        protected override void Parse()
        {
            this.matchCodeElement((int)Toolbox.codeElements.MethodArgumentsList);
            this.matchCodeElement((int)Toolbox.codeElements.BlockOfCode);
        }

        public MethodElement(Code code) : base(code, 0, 0) { }
        public MethodElement(Code code, int offset) : base(code, offset, 0) { }
        public MethodElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
