using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace JSLOL.Parser
{
    public class NumberElement : CodeElement
    {
        //TODO: Add diffrent number formats
        /// <summary>
        /// List of regexp parsing all supported number formats
        /// </summary>
        static Regex[] valueRE ={
            Toolbox.CreateRegex(Toolbox.RegExpTemplates.number)
        };

        private Decimal value=0;

        protected override int[] _allowedCodeElements
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Parse()
        {
            Match m = null;
            foreach (Regex r in NumberElement.valueRE)
            {
                m = this._code.match(r, this.offset);
                if (m.Length != 0)
                    break;
            }
            if (!m.Success)
                throw new CodeElementNotFound();

            this.offset += m.Length;
            //TODO: Parse number
            this.value = Decimal.Parse(m.Value, 
                NumberStyles.Any,CultureInfo.InvariantCulture
                );

        }

        public NumberElement(Code code) : base(code, 0, 0) { }
        public NumberElement(Code code, int offset) : base(code, offset, 0) { }
        public NumberElement(Code code, int offset, int indentionLevel) : base(code, offset, indentionLevel) { }

    }
}
