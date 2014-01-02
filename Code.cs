using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    public class Code
    {
        public String source { get; private set; }
        private List<String> _typesRegistry = new List<string>();
        public List<String> typesRegistry
            { get { return this._typesRegistry; } }


        public Code(String str)
        {
            this.source = str;
        }

        public void RegisterType(String signature)
        {

        }

        public Match getWhiteChars(int offset)
        {
            return this.getWhiteChars(offset,false);
        }

        public Match getWhiteChars(int offset,bool includeNewLines)
        {
            if(includeNewLines)
                return Toolbox.stdRegex[Toolbox.RegExpTemplates.whiteCharsNewL].Match(this.source, offset);

            return Toolbox.stdRegex[Toolbox.RegExpTemplates.whiteChars].Match(this.source,offset);
        }

        public Match match(Regex r,int offset)
        {
            return r.Match(this.source, offset);
        }
    }
}
