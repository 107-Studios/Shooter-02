using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.Managers;

namespace shooter02.Threading
{
    class RenderData
    {
        public Vector2 position;
        public Vector2 center;
        public Vector2 scale;
        public int texture2D;
        public Rectangle drawRectangle;
        public float rotation;
        public Color textureTint;
        public SpriteEffects effect;

        public RenderData()
        {
            drawRectangle = Rectangle.Empty;
            position = Vector2.Zero;
            center = Vector2.Zero;
            scale = new Vector2(1.0f);
            texture2D = -1;
            textureTint = Color.White;
            rotation = 0.0f;
            effect = SpriteEffects.None;
        }
    }
}
