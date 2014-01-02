using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TEST = JSLOL.Parser.Toolbox.RegExpTemplates;
using Cmd = System.Console;

namespace JSLOL.Parser
{
    public class Declaration : CodeElement
    {
        //regexp for matching field signature
        static private Regex signatureRE = Toolbox.CreateRegex(
                @"(?<type>" + Toolbox.RegExpSources[TEST.identificator] +
                @"(\." + Toolbox.RegExpSources[TEST.identificator] + @")*)" +
                Toolbox.RegExpSources[TEST.whiteChar] + @"*" +
                @"(?<advtype>" + Toolbox.RegExpSources[TEST.advType] + @")?" +
                Toolbox.RegExpSources[TEST.whiteChar] + @"+" + 
                @"(?<name>" + Toolbox.RegExpSources[TEST.identificator] + @")"
            );
        static private int[] allowedCodeElements = {
            (int)Toolbox.codeElements.Declaration
            ,(int)Toolbox.codeElements.Comment
        };
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStart]);
        static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStop]);

        private String _type, _advType, _name, _match;
        private CodeElement value;
        private CodeElement initialization = null;

        override protected int[] _allowedCodeElements { get { return ObjectElement.allowedCodeElements; } }

        public Declaration(Code code) : base(code, 0,0) { }
        public Declaration(Code code,int offset) : base(code,offset,0) { }
        public Declaration(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

        protected override void parse() 
        {
            Match m = this._code.match(Declaration.signatureRE,this.offset);
            if (m.Length == 0)
                throw new CodeElementNotFound(this.offset, this._code, Declaration.signatureRE.ToString());

            this._type = m.Groups["type"].Value;
            this._name = m.Groups["name"].Value;
            this._advType = m.Groups["advtype"].Success ? m.Groups["advtype"].Value : null;
            this._match = m.Value;
            this.offset += m.Length;

            #if DEBUG
            Cmd.WriteLine("offset : {3} :: Declaration '{0}' '{1}' '{2}'", this._type, this._advType, this._name, this.offset);
            #endif

            this.matchCodeElement((int)Toolbox.codeElements.Assertion);
            this.matchEndOfInstructionMarker();
        }

    }
}
