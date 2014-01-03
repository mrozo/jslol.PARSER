using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Base class for elements like : <![CDATA[<array>,<dictionary>,<argumentsList> ]]> etc.
    /// </summary>
    abstract class ListCodeElement : CodeElement
    {
        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        abstract protected Regex _listSeparator
        { get; }
        abstract protected Regex _startMarker
        { get; }
        abstract protected Regex _stopMarker
        { get; }

        protected override void Parse()
        {
            this.skipWhiteChars();
            this.matchMandatoryRegexp(this._startMarker);

            do
            {
                this.matchCodeElement((int)Toolbox.codeElement.Comment);
                this.matchAllowedCodeElement();
                this.matchCodeElement((int)Toolbox.codeElement.Comment);
            } while (this.matchRegexp(this._listSeparator).Success);

            this.matchMandatoryRegexp(this._stopMarker);
        }

        public ListCodeElement(Code code) : base(code, 0, 0) { }
        public ListCodeElement(Code code, int offset) : base(code, offset, 0) { }
        public ListCodeElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }
    }
}
