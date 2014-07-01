using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse arguments list for method call : <![CDATA[ ( <expression>[, <expression>[, <expression> [..]]]]>
    /// </summary>
    class ArgumentsList : ListCodeElement
    {
        public static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.argumentListStartMarker]);
        public static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.argumentListStopMarker]);
        public static Regex listSeparator = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.argumentListSeparator]);

        public static int[] allowedCodeElements ={
            (int)Toolbox.codeElement.Expression
            ,(int)Toolbox.codeElement.VariableExpression
        };

        protected override int[] _allowedCodeElements
            { get { return ArgumentsList.allowedCodeElements; } }

        protected override Regex _listSeparator
            { get { return ArgumentsList.listSeparator; } }

        protected override Regex _startMarker
            { get { return ArgumentsList.startMarker; } }

        protected override Regex _stopMarker
            { get { return ArgumentsList.stopMarker; } }

        
        public ArgumentsList(Code code) : base(code, 0, 0) { }
        public ArgumentsList(Code code, int offset) : base(code, offset, 0) { }
        public ArgumentsList(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
