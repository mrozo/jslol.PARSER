using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class for parsing <![CDATA[<methodCall>]]> : <![CDATA[<identificator><argumentsList>]]>
    /// </summary>
    class MethodCall : CodeElement
    {
        public static Regex methodName = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.identificator]);
        private String name;

        protected override int[] _allowedCodeElements
            { get { throw new NotImplementedException(); } }

        protected override void Parse()
        {
            this.name = this.matchMandatoryRegexp(MethodCall.methodName).Value;
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.ArgumentsDeclarationsList);
        }

        public MethodCall(Code code) : base(code, 0,0) { }
        public MethodCall(Code code,int offset) : base(code,offset,0) { }
        public MethodCall(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
