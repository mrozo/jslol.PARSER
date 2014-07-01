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
        /// <param name="source">JSLOL source to parses</param>
        public SourceFile(String source) : base(new Code(source),0,0) {}

        protected override void Parse()
        {
            //get comments, parse objectElement, again get comments,
            //skip white chars and check if it's the end of the source
            while (null != this.matchCodeElement((int)Toolbox.codeElement.Comment)) { }
            this.matchMandatoryCodeElement((int)Toolbox.codeElement.Object);
            while (null != this.matchCodeElement((int)Toolbox.codeElement.Comment)) { }
            this.skipWhiteChars(true);
            if (this.offset != this._code.source.Length)
                throw new Exception();

        }

        public override string toCSharp(String ns)
        {
            return "namespace " + ns + "\n{\n" + this.subobjectsToCSharp(ns) + "}\n";
        }
    }
}
