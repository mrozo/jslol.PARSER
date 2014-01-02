using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLOL.Parser
{
    class Instruction : CodeElement
    {
        public static int[] allowedCodeElements = 
        {
            (int)Toolbox.codeElements.Declaration
        };

        protected override int[] _allowedCodeElements
        {
            get { return Instruction.allowedCodeElements; }
        }

        protected override void Parse()
        {
            this.matchAllowedCodeElementsOnce();
            this.matchEndOfInstructionMarker();
        }
        
        public Instruction(Code code) : base(code, 0, 0) { }
        public Instruction(Code code, int offset) : base(code, offset, 0) { }
        public Instruction(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
