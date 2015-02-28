using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class GameFactory : IGameFactory
    {
        public Game CreateGame(Map map, Ai ai)
        {
            return new Game(map, ai);
        }
    }
}
