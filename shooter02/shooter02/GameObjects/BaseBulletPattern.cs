using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class BaseBulletPattern : CGameObject
    {
        #region CGameObject Members

        public override void Update(double fTimeElapsed)
        {
            base.Update(fTimeElapsed);
        }

        public override void HandleCollision(CGameObject gameObject)
        {
            base.HandleCollision(gameObject);
        }

        #endregion

        public BaseBulletPattern(CGameObject parent) : base()
        {
            m_pParent = parent;
        }
        
        public virtual void emitPattern()
        {
        }
    }
}
