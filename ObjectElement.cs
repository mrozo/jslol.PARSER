﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    public class ObjectElement : ContainerCodeElement
    {
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStart]);
        static Regex stopMarker  = Toolbox.CreateRegex(Toolbox.RegExpSources[Toolbox.RegExpTemplates.objectStop]);
        static public int[] allowedCodeElements = {
            (int)Toolbox.codeElement.FieldDeclaration
            ,(int)Toolbox.codeElement.Comment
        };

        override protected Regex _startMarker  {
            get {return ObjectElement.startMarker; }
        }
        override protected Regex _stopMarker {
            get {return ObjectElement.stopMarker; }
        }

        override protected int[] _allowedCodeElements { get { return ObjectElement.allowedCodeElements; } }
        
        public ObjectElement(Code code) : base(code, 0,0) { }
        public ObjectElement(Code code,int offset) : base(code,offset,0) { }
        public ObjectElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

        public override String toCSharp(String ns)
        {
            return this.subobjectsToCSharp(ns);
        }
    }
}
