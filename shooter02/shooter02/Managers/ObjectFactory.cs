using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;
using Microsoft.Xna.Framework;
using shooter02.Managers;
using shooter02.Misc;

namespace shooter02.ObjectManager
{
    class ObjectFactory
    {
        /// <summary>
        /// CreatePlayer
        /// Creates a generic player and sets the attributes they share
        /// </summary>
        /// <returns></returns>
        public static CPlayer CreatePlayer()
        {
            CPlayer player = new CPlayer();
            
            float startPos = SaveInfo.Instance.ScreenResolution.Y - 128;
            player.UpdateData.objectType = Threading.UpdateData.ObjectType.PLAYER;
            player.UpdateData.position = new Vector2(startPos);
            player.RenderData.drawRectangle = new Rectangle(0, 0, 64, 64);
            player.RenderData.position = new Vector2(startPos);
            player.RenderData.center = new Vector2(32);
            return player;
        }

        /// <summary>
        /// CreatePlayer1
        /// This creates the first player
        /// Imagine that
        /// </summary>
        /// <param name="spriteID">The sprite ID the player selected</param>
        /// <returns></returns>
        public static CPlayer CreatePlayer1(char spriteID)
        {
            CPlayer player = CreatePlayer();

            player.SetPlayerIndex(PlayerIndex.One);
            player.RenderData.texture2D = TextureManager.Instance.LoadTexture("textures/fightersprite" + spriteID);
            player.UpdateData.position.X = SaveInfo.Instance.ScreenResolution.X / 2 - 192;
            player.RenderData.position.X = player.UpdateData.position.X;

            return player;
        }

        /// <summary>
        /// CreatePlayer2
        /// This creates the second player
        /// Imagine that....were lazy
        /// </summary>
        /// <param name="spriteID">The sprite ID the player selected</param>
        /// <returns></returns>
        public static CPlayer CreatePlayer2(char spriteID)
        {
            CPlayer player = CreatePlayer();

            player.SetPlayerIndex(PlayerIndex.Two);
            player.RenderData.texture2D = TextureManager.Instance.LoadTexture("textures/fightersprite" + spriteID);
            player.UpdateData.position.X = SaveInfo.Instance.ScreenResolution.X / 2 - 64;
            player.RenderData.position.X = player.UpdateData.position.X;

            return player;
        }

        /// <summary>
        /// CreatePlayer3
        /// This creates the third player
        /// Imagine that....were lazy
        /// </summary>
        /// <param name="spriteID">The sprite ID the player selected</param>
        /// <returns></returns>
        public static CPlayer CreatePlayer3(char spriteID)
        {
            CPlayer player = CreatePlayer();

            player.SetPlayerIndex(PlayerIndex.Three);
            player.RenderData.texture2D = TextureManager.Instance.LoadTexture("textures/fightersprite" + spriteID);
            player.UpdateData.position.X = SaveInfo.Instance.ScreenResolution.X / 2 + 64;
            player.RenderData.position.X = player.UpdateData.position.X;

            return player;
        }

        /// <summary>
        /// CreatePlayer4
        /// This creates the fourth player
        /// Imagine that....were lazy
        /// </summary>
        /// <param name="spriteID">The sprite ID the player selected</param>
        /// <returns></returns>
        public static CPlayer CreatePlayer4(char spriteID)
        {
            CPlayer player = CreatePlayer();

            player.SetPlayerIndex(PlayerIndex.Four);
            player.RenderData.texture2D = TextureManager.Instance.LoadTexture("textures/fightersprite" + spriteID);
            player.UpdateData.position.X = SaveInfo.Instance.ScreenResolution.X / 2 + 192;
            player.RenderData.position.X = player.UpdateData.position.X;

            return player;
        }
    }
}
