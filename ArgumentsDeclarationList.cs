using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse <![CDATA[<argumentsList>]]> that is a part of <![CDATA[<methodExpression>]]>
    /// </summary>
    class ArgumentsDeclarationsList : ListCodeElement
    {
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListStartMarker);
        static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListStopMarker);
        static Regex listSeparator = Toolbox.CreateRegex(Toolbox.RegExpTemplates.argumentListSeparator);
        static int[] allowedCodeElements = {
            (int)Toolbox.codeElement.Declaration
        };

        protected override Regex _listSeparator
            { get { return ArgumentsDeclarationsList.listSeparator; } }

        protected override Regex _startMarker
            { get { return ArgumentsDeclarationsList.startMarker; } }

        protected override Regex _stopMarker
            { get { return ArgumentsDeclarationsList.stopMarker;  } }


        protected override int[] _allowedCodeElements
            { get { return ArgumentsDeclarationsList.allowedCodeElements; } }


        public ArgumentsDeclarationsList(Code code) : base(code, 0, 0) { }
        public ArgumentsDeclarationsList(Code code, int offset) : base(code, offset, 0) { }
        public ArgumentsDeclarationsList(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }
    }
}
