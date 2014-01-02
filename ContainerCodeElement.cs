using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// base class that contains tools for fast creation container-type code elements parsers - eg. parser for class definition,dictionary, array etc.
    /// </summary>
    public abstract class ContainerCodeElement : CodeElement
    {
        abstract protected Regex _startMarker { get; }
        abstract protected Regex _stopMarker { get; }

        public ContainerCodeElement(Code code) : base(code, 0, 0) { }
        public ContainerCodeElement(Code code, int offset) : base(code, offset, 0) { }
        public ContainerCodeElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

        /// <summary>
        /// Method that check all allowed code elements from this.allowedCodeElements in code preceded by this._startMarker and followed by this._stopMarker 
        /// </summary>
        protected void matchAllowedTypesAndMarkers()
        {
            Match m; // temporary variable used to save RegExp match results
            this.skipWhiteChars(true);
            m = this._code.match(this._startMarker, this._offset);
            if (m.Length == 0)
                throw new CodeElementNotFound(this.offset, this._code, this._startMarker.ToString());
            this._offset += m.Length;

            this.matchAllowedTypes();
            
            m = this._code.match(this._stopMarker, this._offset);
            if (m.Length == 0)
                throw new CodeElementNotFound(this.offset, this._code, this._stopMarker.ToString());
            this._offset += m.Length;
        }
        
        /// <summary>
        /// Method that check all allowed code elements from this.allowedCodeElements in code
        /// </summary>
        protected void matchAllowedTypes()
        {
                while ( this.matchAllowedCodeElementsOnce() != null ) { }
        }
    }
}
