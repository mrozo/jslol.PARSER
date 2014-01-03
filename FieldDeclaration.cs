using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class that is used to parse field declaration in object : <![CDATA[<declaration> ; ]]>
    /// </summary>
    class FieldDeclaration : Declaration
    {
        public FieldDeclaration(Code code) : base(code, 0,0) { }
        public FieldDeclaration(Code code,int offset) : base(code,offset,0) { }
        public FieldDeclaration(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }


        protected override void Parse()
        {
            base.Parse();
            this.matchEndOfInstructionMarker();
        }
    }
}
