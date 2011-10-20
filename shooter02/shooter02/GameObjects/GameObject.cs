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
        private UpdateData m_pUpdateData;
        private RenderData m_pRenderData;
        protected int nId;
        protected bool bIsDirty = false;

        public CGameObject()
        {
            m_pUpdateData = new UpdateData();
            m_pRenderData = new RenderData();
            nId = -1;
        }

        public UpdateData UpdateData
        {
            get { return m_pUpdateData; }
            set { m_pUpdateData = value; }
        }

        public RenderData RenderData
        {
            get { return m_pRenderData; }
            set { m_pRenderData = value; }
        }

        public bool getIsDirty()
        {
            return bIsDirty;
        }

        public int getListID()
        {
            return nId;
        }

        public void setListID(int newID)
        {
            nId = newID;
        }

        public virtual void Update(double fTimeElapsed)
        {

        }

        public virtual void HandleCollision(CGameObject gameObject)
        {

        }

    }
}
