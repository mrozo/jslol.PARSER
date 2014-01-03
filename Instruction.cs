using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLOL.Parser
{
    /// <summary>
    /// General element for method, that implements logic of the program.
    /// </summary>
    class Instruction : CodeElement
    {
        public static int[] allowedCodeElements = 
        {
            (int)Toolbox.codeElement.Declaration
            ,(int)Toolbox.codeElement.MethodCall
        };

        protected override int[] _allowedCodeElements
        {
            get { return Instruction.allowedCodeElements; }
        }

        protected override void Parse()
        {
            this.matchAllowedCodeElement();
            this.matchEndOfInstructionMarker();
        }
        
        public Instruction(Code code) : base(code, 0, 0) { }
        public Instruction(Code code, int offset) : base(code, offset, 0) { }
        public Instruction(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
