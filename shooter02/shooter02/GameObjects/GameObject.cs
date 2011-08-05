using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shooter02.GameObjects
{
    class CGameObject
    {
        protected Vector2 m_vPosition;
        protected Vector2 m_vDirection;

        //protected Texture m_texImage;

        public CGameObject()
        {
            m_vPosition = new Vector2(0.0f);
            m_vDirection = new Vector2(0.0f);
            //m_texImage = new Texture();
        }

        public void Update(float fTimeElapsed)
        {

        }

        public void Render()
        {
            
        }
    }
}
