using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class used to parse method expression eg. " <![CDATA[m(<argumentsDeclarationList> { <blockOfCode> } ]]>")
    /// </summary>
    class MethodExpression : CodeElement
    {
        /// <summary>
        /// Matches "<![CDATA[( <whitechars><typeidentificator><whitechars> )]]>"
        /// </summary>
        public static Regex returnType = Toolbox.CreateRegex(
            @"\((?<returntype>" + Toolbox.RegExpSources[Toolbox.RegExpTemplates.whiteCharNewL] + @"*" +
            Toolbox.RegExpSources[Toolbox.RegExpTemplates.identificator] +
            Toolbox.RegExpSources[Toolbox.RegExpTemplates.whiteCharNewL] +
            @"*)\)"
        );

        private string _returnType;

        protected override int[] _allowedCodeElements
            { get { throw new NotImplementedException(); } }

        protected override void Parse()
        {
            this._returnType = this.matchMandatoryRegexp(MethodExpression.returnType).Groups["returntype"].Value;
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.ArgumentsDeclarationsList);
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.BlockOfCode);
        }

        public MethodExpression(Code code) : base(code, 0, 0) { }
        public MethodExpression(Code code, int offset) : base(code, offset, 0) { }
        public MethodExpression(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
