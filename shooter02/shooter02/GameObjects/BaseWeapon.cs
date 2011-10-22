using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class CBaseWeapon : CGameObject
    {
        // TODO: implement BaseWeapon

        BaseBulletPattern m_pBulletPattern;

        public CBaseWeapon(CGameObject parent) : base()
        {
            m_pParent = parent;
            m_pBulletPattern = new BaseBulletPattern(this);
        }

        public virtual void Fire()
        {
            m_pBulletPattern.emitPattern();
        }

    }
}
