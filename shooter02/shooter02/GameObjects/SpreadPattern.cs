using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class SpreadPattern : BaseBulletPattern
    {
        #region BaseBulletPattern

        public override void emitPattern()
        {
            base.emitPattern();
        }

        #endregion

        public SpreadPattern(CGameObject parent) : base(parent)
        {
        }
    }
}
