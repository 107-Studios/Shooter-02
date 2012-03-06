using System.Collections.Generic;
using shooter02.GameObjects;
using shooter02.Managers;
using Microsoft.Xna.Framework;
using shooter02.Managers;

namespace shooter02.GUI.Lobby
{
    class LobbyObject
    {
        private CPlayer m_cPlayerObject;
        private CBaseWeapon m_cWeapon;
        private int m_nWeaponTexture;
        private int m_nShipTexture;
        private int m_nShipWeaponIndex;
        private Rectangle m_rDrawRect;

        public LobbyObject(CPlayer _player)
        {
            m_cPlayerObject = _player;
            m_cWeapon = _player.BaseWeapon;
            m_nShipWeaponIndex = 0;
            m_nShipTexture = TextureManager.Instance.LoadTexture("textures/shipselect");
            m_nWeaponTexture = TextureManager.Instance.LoadTexture("textures/weaponselect");
        }

        public void Input()
        {
            if (InputManager.Instance.KeyDown(m_cPlayerObject.KeyboardBindings.MoveLeft) ||
                InputManager.Instance.GamePadDown(m_cPlayerObject.PlayerIndex, m_cPlayerObject.GamepadBindings.MoveLeft))
            {
                m_nShipWeaponIndex = (m_nShipWeaponIndex + 1) & 3;
            }

            if (InputManager.Instance.KeyDown(m_cPlayerObject.KeyboardBindings.MoveRight) ||
                InputManager.Instance.GamePadDown(m_cPlayerObject.PlayerIndex, m_cPlayerObject.GamepadBindings.MoveRight))
            {
                // Jooish way to implement -1
                m_nShipWeaponIndex = (m_nShipWeaponIndex + 3) & 3;
            }
        }
    }
}
