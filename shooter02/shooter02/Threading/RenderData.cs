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
        // TODO: replace this with our render data
        public Vector2 position;
        public Vector2 center;
        public Vector2 scale;
        public int texture2D;
        public Rectangle drawRectangle;

        public RenderData()
        {
            this.drawRectangle = Rectangle.Empty;
            this.position = Vector2.Zero;
            this.center = Vector2.Zero;
            this.texture2D = -1;
        }
    }
}
