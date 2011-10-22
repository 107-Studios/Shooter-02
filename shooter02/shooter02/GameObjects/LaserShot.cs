using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class CLaserShot : CBaseWeapon
    {
        #region CBaseWeapon Members

        public override void Fire()
        {
            // TODO: Implement LaserShot's Fire method
        }

        #endregion

        public CLaserShot(CGameObject parent) : base(parent)
        {
        }
    }
}
