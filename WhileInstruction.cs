using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    class WhileInstruction : CodeElement
    {
        static Regex conditionStartMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.controlInstructionArgumentStartMarker]);
        static Regex conditionStopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.controlInstructionArgumentStopMarker]);
        static Regex instructionName = Toolbox.CreateRegex("while");
        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Parse()
        {

            if (!this.matchMandatoryRegexp(WhileInstruction.instructionName).Success)
                throw new CodeElementNotFound();

            this.matchMandatoryRegexp(WhileInstruction.conditionStartMarker);
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.Expression);
            this.matchMandatoryRegexp(WhileInstruction.conditionStopMarker);
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.BlockOfCode);
        }

        public WhileInstruction(Code code) : base(code, 0, 0) { }
        public WhileInstruction(Code code, int offset) : base(code, offset, 0) { }
        public WhileInstruction(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
