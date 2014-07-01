using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Base class that contains tools for fast creation container-type code elements parsers - eg. parser for class definition,dictionary, array etc.
    /// </summary>
    public abstract class ContainerCodeElement : CodeElement
    {
        /// <summary>
        /// Property that should return regular Regex object that matches start marker of a block. 
        /// </summary>
        abstract protected Regex _startMarker { get; }
        /// <summary>
        /// Property that should return regular Regex object that matches stop marker of a block. 
        /// </summary>
        abstract protected Regex _stopMarker { get; }

        public ContainerCodeElement(Code code) : base(code, 0, 0) { }
        public ContainerCodeElement(Code code, int offset) : base(code, offset, 0) { }
        public ContainerCodeElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

        /// <summary>
        /// Method that checks all allowed code elements from this.allowedCodeElements in code preceded by this._startMarker and followed by this._stopMarker 
        /// </summary>
        protected override void Parse()
        {
            this.matchMandatoryRegexp(this._startMarker, true);
            this.matchAllowedTypes();
            this.matchMandatoryRegexp(this._stopMarker, true);
        }
        /// <summary>
        /// Method that checks all allowed code elements from this.allowedCodeElements in code
        /// </summary>
        protected void matchAllowedTypes()
        {
                while ( this.matchAllowedCodeElement() != null ) { }
        }
    }
}
