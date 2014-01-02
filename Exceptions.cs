using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSLOL.Parser
{
    public class CriticalException : Exception
    {
        public CriticalException(String message, Exception innerException) : base(message, innerException) { }
        public CriticalException(String message) : base(message) { }
    }

    public class CodeElementNotFound : SystemException
    {
        public CodeElementNotFound(int offset, Code code, String regex) 
            : base(
                String.Format(
                    "char offset {0} :  Expected \"{1}\" , found : {2}",
                    offset,
                    regex,
                    code.source.Substring(offset, offset+15>code.source.Length?code.source.Length-offset:15)
                )
            )
        {}

        public CodeElementNotFound() : base() { } 
    }
}
