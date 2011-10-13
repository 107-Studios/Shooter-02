using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace shooter02.Threading
{
    class UpdateData
    {
        // TODO: replace this with our own update data
        public enum ObjectType
        {
            BASE,
            PLAYER,
        }

        public ObjectType objectType;
        public Vector2 acceleration;
        public Vector2 velocity;
        public Vector2 position;
        public float fRotation;

        public UpdateData()
        {
            acceleration = Vector2.Zero;
            velocity = Vector2.Zero;
            position = Vector2.Zero;
            fRotation = 0.0f;
        }
    }
}
