﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.GameObjects
{
    class RicochetPattern : BaseBulletPattern
    {
        #region BaseBulletPattern Members

        public override void emitPattern()
        {
            base.emitPattern();
        }

        #endregion

        public RicochetPattern(CGameObject parent) : base(parent)
        {

        }
    }
}
