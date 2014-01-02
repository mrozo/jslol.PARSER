using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLOL;
using JSLOL.Parser;

namespace JSLOL.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class SourceFile : CodeElement
    {
        List<CodeElement> elements = new List<CodeElement>();
        protected override int[] _allowedCodeElements
            {get { throw new NotImplementedException(); }}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Source to parses</param>
        public SourceFile(String source)
            : base(new Code(source),0,0)
        {}


        protected override void Parse()
        {
            this.elements.Add(Toolbox.createCodeElement((int)Toolbox.codeElements.Object,this._code,this.offset,this._indentionLevel));
        }
    }
}
