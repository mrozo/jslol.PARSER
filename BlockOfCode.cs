﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to parse methods body
    /// </summary>
    class BlockOfCode : ContainerCodeElement
    {
        static Regex startMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.blockOfCodeStartMarker);
        static Regex stopMarker = Toolbox.CreateRegex(Toolbox.RegExpTemplates.blockOfCodeStopMarker);

        static public int[] allowedCodeElements = 
        {
            (int)Toolbox.codeElement.Instruction
            ,(int)Toolbox.codeElement.Comment
        };
        protected override Regex _startMarker
            { get { return BlockOfCode.startMarker; } }

        protected override Regex _stopMarker
            { get { return BlockOfCode.stopMarker; } }

        protected override int[] _allowedCodeElements
            { get { return BlockOfCode.allowedCodeElements; } }

        public BlockOfCode(Code code) : base(code, 0, 0) { }
        public BlockOfCode(Code code, int offset) : base(code, offset, 0) { }
        public BlockOfCode(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
