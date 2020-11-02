using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightUI.ControllerModel
{
    class Game
    {
        public string Id { get; }
        public string Name { get; }

        public Game(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
