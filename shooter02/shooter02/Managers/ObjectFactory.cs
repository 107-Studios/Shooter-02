using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;
using Microsoft.Xna.Framework;

namespace shooter02.ObjectManager
{
    class ObjectFactory
    {
        public static CPlayer createPlayer1()
        {
            CPlayer player = new CPlayer();

            player.setPlayerIndex(PlayerIndex.One);

            return player;
        }
    }
}
