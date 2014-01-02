using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLOL.Parser;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    class Comment : CodeElement

    {
        static Regex CommentRE = Toolbox.CreateRegex(Toolbox.RegExpTemplates.comment);
        private String value;
        protected override int[] _allowedCodeElements
            {get { throw new NotImplementedException(); }}

        protected override void Parse()
        {
            this.value = this.matchMandatoryRegexp(Comment.CommentRE).Value;
        }

        public Comment(Code code) : base(code, 0, 0) { }
        public Comment(Code code, int offset) : base(code, offset, 0) { }
        public Comment(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
