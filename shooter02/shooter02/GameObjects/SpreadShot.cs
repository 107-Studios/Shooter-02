using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class CSpreadShot : CBaseWeapon
    {
        #region CBaseWeapon Members

        public override void Fire()
        {
            // TODO: Implement SpreadShot's Fire method
        }

        #endregion

        public CSpreadShot(CGameObject parent) : base(parent)
        {
        }
    }
}
