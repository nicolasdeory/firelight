using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightCore
{
    public class LEDModuleAttributesAttribute : Attribute
    {
        public Type For;
        public LEDModuleAttributesAttribute(Type forType)
        {
            this.For = forType;
        }
    }
}
