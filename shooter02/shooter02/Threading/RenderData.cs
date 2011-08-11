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
        public Color color;
        public Vector2 position;
        public Texture2D texture2D;

        public RenderData()
        {
            this.color = Color.White;
            this.position = Vector2.Zero;
            this.texture2D = null;
        }
    }
}
