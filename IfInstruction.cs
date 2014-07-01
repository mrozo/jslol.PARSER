using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse if statement : <![CDATA[if(<expression>)<blockOfCode>[else <blockOfCode>]]]>
    /// </summary>
    class IfInstruction : CodeElement
    {

        static Regex conditionStartMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.controlInstructionArgumentStartMarker]);
        static Regex conditionStopMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.controlInstructionArgumentStopMarker]);
        static Regex instructionName = Toolbox.CreateRegex(@"if");
        static Regex elseInstructionName = Toolbox.CreateRegex(@"else");

        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Parse()
        {
            if(!this.matchMandatoryRegexp(IfInstruction.instructionName).Success)
                throw new CodeElementNotFound();

            this.matchMandatoryRegexp(IfInstruction.conditionStartMarker);
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.Expression);
            this.matchMandatoryRegexp(IfInstruction.conditionStopMarker);
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.BlockOfCode);

            if (this.matchRegexp(IfInstruction.elseInstructionName,true,false).Success)
                this.matchMandatoryCodeElement((int)Toolbox.codeElement.BlockOfCode);
        }
    
        public IfInstruction(Code code) : base(code, 0, 0) { }
        public IfInstruction(Code code, int offset) : base(code, offset, 0) { }
        public IfInstruction(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
