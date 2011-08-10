using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace shooter02.Threading
{
    class RenderData
    {
        // TODO: replace this with our render data
        public Vector3 color;
        public Matrix worldMatrix;

        public RenderData()
        {
            this.color = Vector3.Zero;
            this.worldMatrix = Matrix.Identity;
        }
    }
}
