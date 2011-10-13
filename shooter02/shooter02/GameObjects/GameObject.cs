using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.Threading;

namespace shooter02.GameObjects
{
    class CGameObject
    {
        protected UpdateData m_pUpdateData;
        protected RenderData m_pRenderData;
        protected int nId;

        public CGameObject()
        {
            m_pUpdateData = new UpdateData();
            m_pRenderData = new RenderData();
            nId = -1;
        }

        public int getListID()
        {
            return nId;
        }

        public void setListID(int newID)
        {
            nId = newID;
        }

        public virtual void Update(float fTimeElapsed)
        {

        }

        public virtual void HandleCollision(CGameObject gameObject)
        {

        }

    }
}
