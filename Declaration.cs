using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TEST = JSLOL.Parser.Toolbox.RegExpTemplates;
using Cmd = System.Console;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse field declaration : <![CDATA[<typeName> <name> [<assertion>]]]>
    /// </summary>
    public class Declaration : CodeElement
    {
        /// <summary>
        /// regexp for matching field signature
        /// </summary>
        static private Regex signatureRE = Toolbox.CreateRegex(
                @"(?<type>" + Toolbox.RegExpSources[Toolbox.RegExpTemplates.identificator] + ")" +
                Toolbox.RegExpSources[Toolbox.RegExpTemplates.whiteChar] + @"*" +
                @"(?<advtype>" + Toolbox.RegExpSources[Toolbox.RegExpTemplates.advType] + @")?" +
                Toolbox.RegExpSources[Toolbox.RegExpTemplates.whiteChar] + @"+" +
                @"(?<name>" + Toolbox.RegExpSources[Toolbox.RegExpTemplates.name] + @")"
            );
        static private int[] allowedCodeElements 
            { get { throw new NotImplementedException();} }
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStart]);
        static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStop]);

        private String _type, _advType, _name, _match;

        override protected int[] _allowedCodeElements { get { return ObjectElement.allowedCodeElements; } }

        protected override void Parse() 
        {
            Match m = this.matchMandatoryRegexp(signatureRE);

            this._type = m.Groups["type"].Value;
            this._name = m.Groups["name"].Value;
            this._advType = m.Groups["advtype"].Success ? m.Groups["advtype"].Value : null;
            this._match = m.Value;

            this.matchCodeElement((int)Toolbox.codeElement.Assertion);
        }

        public override string toCSharp(string ns)
        {
            String csCode = "";


            return csCode;
        }

        public Declaration(Code code) : base(code, 0, 0) { }
        public Declaration(Code code, int offset) : base(code, offset, 0) { }
        public Declaration(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
