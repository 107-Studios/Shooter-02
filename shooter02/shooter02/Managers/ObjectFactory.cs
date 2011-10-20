using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;
using Microsoft.Xna.Framework;
using shooter02.Managers;

namespace shooter02.ObjectManager
{
    class ObjectFactory
    {
        public static CPlayer createPlayer1()
        {
            CPlayer player = new CPlayer();

            player.setPlayerIndex(PlayerIndex.One);
            float startPos = 360;
            player.UpdateData.objectType = Threading.UpdateData.ObjectType.PLAYER;
            player.UpdateData.position = new Vector2(startPos);
            player.RenderData.drawRectangle = new Rectangle(0, 0, 64, 64);
            player.RenderData.position = new Vector2(startPos);
            player.RenderData.texture2D = TextureManager.Instance.LoadTexture("textures/fightersprite");
            player.RenderData.center = new Vector2(32);

            return player;
        }
    }
}
