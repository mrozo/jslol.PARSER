using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    class MethodsArgumentsList : CodeElement
    {
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListStartMarker);
        static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListStopMarker);
        static Regex ArgumentsListSeparator = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListSeparator);

        protected override int[] _allowedCodeElements
            { get { throw new NotImplementedException(); } }

        protected override void Parse()
        {
            this.matchMandatoryRegexp(MethodsArgumentsList.startMarker);
            do
            {
                this.matchCodeElement((int)Toolbox.codeElements.Declaration);
                this.skipWhiteChars();
            } while (this.matchRegexp(MethodsArgumentsList.ArgumentsListSeparator).Success);

            this.matchMandatoryRegexp(MethodsArgumentsList.stopMarker);
        }

        public MethodsArgumentsList(Code code) : base(code, 0, 0) { }
        public MethodsArgumentsList(Code code, int offset) : base(code, offset, 0) { }
        public MethodsArgumentsList(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
