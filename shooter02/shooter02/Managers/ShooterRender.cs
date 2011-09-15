using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.Managers;

namespace shooter02.Managers
{
    class ShooterRender : RenderManager
    {

        public ShooterRender(DoubleBuffer doubleBuffer, Game game) : base(doubleBuffer, game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            // Before we render, update the RenderData if needed
            foreach (ChangeMessage msg in messageBuffer.Messages)
            {
                switch (msg.MessageType)
                {
                    case ChangeMessageType.UpdateCameraView:
                        break;
                    case ChangeMessageType.UpdateWorldMatrix:
                        break;
                    case ChangeMessageType.UpdateHighlightColor:
                        break;
                    case ChangeMessageType.CreateNewRenderData:
                        break;
                    case ChangeMessageType.DeleteRenderData:
                        break;
                    default:
                        break;
                }
            }

            // Now that we have all the RenderData updated, lets render!
            foreach (RenderData item in RenderDataObjects)
            {
                // Use the TextureManager to render the objects
                TextureManager.Instance.DrawTexture(item.texture2D, item.position, item.drawRectangle, item.textureTint, item.rotation, item.center, item.scale, SpriteEffects.None, 0.0f);
            }
            base.Draw(gameTime);
        }
    }
}
