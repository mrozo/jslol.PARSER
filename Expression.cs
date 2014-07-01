using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{

    /// <summary>
    /// Expression is a code fragment that returns value, eg. number/string value, method call arithmetic operation etc.
    /// </summary>
    public class Expression : CodeElement
    {

        static public int[] allowedCodeElements = { 
            (int)Toolbox.codeElement.Number
            ,(int)Toolbox.codeElement.String
            ,(int)Toolbox.codeElement.MethodExpression
            ,(int)Toolbox.codeElement.VariableExpression
        };
        
        protected override int[] _allowedCodeElements
            { get { return Expression.allowedCodeElements; } }

        protected override void Parse()
        {
            //TODO: parsing extended expressions, eg 1 + intVar, 2*methodCall() etc.
            this.matchAllowedCodeElement();
            if (this.codeElements.Count==0)
                throw new CodeElementNotFound();
        }
        
        public Expression(Code code) : base(code, 0, 0) { }
        public Expression(Code code, int offset) : base(code, offset, 0) { }
        public Expression(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
