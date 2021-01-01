using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FirelightCore
{
    public abstract class GameModuleAttributes : ModuleAttributes
    {
        public string Name { get; protected set; } = "Unknown Module";

        public string ProcessName { get; protected set; }
    }
}
