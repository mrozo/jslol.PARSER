using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSLOL.Parser
{
    /// <summary>
    /// Class to store the code and helpers.
    /// </summary>
    public class Code
    {
        /// <summary>
        /// 
        /// </summary>
        public String source { get; private set; }

        private String _classDefinitions = "";

        public Code(String str)
        {
            this.source = str;
        }

        /// <summary>
        /// Matches and returns white chars from the beggining of the code
        /// </summary>
        /// <param name="offset">Number of chars to skip before matching</param>
        /// <returns>Mtch object</returns>
        public Match getWhiteChars(int offset)
        {
            return this.getWhiteChars(offset,false);
        }

        /// <summary>
        /// Get white chars from the code starting from the offset
        /// </summary>
        /// <param name="offset">Number of chars to skip before matching</param>
        /// <param name="includeNewLines"></param>
        /// <returns></returns>
        public Match getWhiteChars(int offset,bool includeNewLines)
        {
            if(includeNewLines)
                return Toolbox.stdRegex[Toolbox.RegExpTemplates.whiteCharsNewL].Match(this.source, offset);

            return Toolbox.stdRegex[Toolbox.RegExpTemplates.whiteChars].Match(this.source,offset);
        }

        /// <summary>
        /// Matches given regex against the code at given offset.
        /// </summary>
        /// <param name="r">Regex object to match</param>
        /// <param name="offset">Number of chars to skip before matching</param>
        /// <returns></returns>
        public Match match(Regex r,int offset)
        {
            return r.Match(this.source, offset);
        }

    }
}
