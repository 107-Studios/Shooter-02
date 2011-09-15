using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Managers;

namespace shooter02.GameObjects
{
    class CPlayer : CGameObject
    {
        // TODO: implement CPlayer
        CBaseWeapon baseWeapon;
        CPlayer secondaryPlayer;
        float movementSpeed;
        float combineSpeed;
        KeyBindings keyboardBindings;
        ButtonBindings gamepadBindings;

        public CPlayer()
        {
        }

        public override void Update(float fTimeElapsed)
        {
            base.Update(fTimeElapsed);
        }

        public override void HandleCollision(CGameObject gameObject)
        {
            base.HandleCollision(gameObject);
        }
        
    }
}
