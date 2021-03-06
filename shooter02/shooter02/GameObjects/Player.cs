﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Managers;
using Microsoft.Xna.Framework;
using shooter02.Threading;
using shooter02.GameStates;
using shooter02.Managers.Events;

namespace shooter02.GameObjects
{
    class CPlayer : CGameObject, IListener
    {
        // TODO: implement CPlayer
        protected CBaseWeapon m_cBaseWeapon;
        protected CPlayer m_cSecondaryPlayer;
        protected double m_dNumLives;
        protected double m_dScore;
        protected double m_dMovementSpeed;
        protected double m_dCombineSpeed;
        protected KeyBindings m_cKeyboardBindings;
        protected ButtonBindings m_cGamepadBindings;
        protected PlayerIndex m_ePlayerIndex;
        protected bool m_bPrimaryPlayer;

        public CPlayer()
        {
            m_cBaseWeapon = new CBaseWeapon(this);
            m_cSecondaryPlayer = null;
            m_dNumLives = 3;
            m_dScore = 0;
            m_dMovementSpeed = 200.0;
            m_dCombineSpeed = 150.0;
            m_cKeyboardBindings = new KeyBindings();
            m_cGamepadBindings = new ButtonBindings();
            m_bPrimaryPlayer = false;

            // Object Factory will take care of the following
            // - playerIndex

            EventManager.Instance.RegisterEvent(EVENT_ID.PLAYER_COMBINE, this);
        }

        public void SetSecondaryPlayer(CPlayer _player = null, bool primary = false)
        {
            m_cSecondaryPlayer = _player;
            m_bPrimaryPlayer = primary;
        }

        public void SetPlayerIndex(PlayerIndex index)
        {
            m_ePlayerIndex = index;
        }

        public virtual void Input(double fTimeElapsed)
        {
            // TODO: FIX: Make sure to only accept keyboard input if player one is not using a gamepad
            if (InputManager.Instance.KeyDown(m_cKeyboardBindings.MoveLeft) ||
                InputManager.Instance.GamePadDown(m_ePlayerIndex, m_cGamepadBindings.MoveLeft))
            {
                if (null == m_cSecondaryPlayer)
                    UpdateData.velocity.X -= (float)(m_dMovementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.X -= (float)(m_dCombineSpeed * fTimeElapsed);
                    m_cSecondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
                else
                {
                    // TODO: Update Player shield.
                }
            }

            if (InputManager.Instance.KeyDown(m_cKeyboardBindings.MoveRight) ||
                InputManager.Instance.GamePadDown(m_ePlayerIndex, m_cGamepadBindings.MoveRight))
            {
                if (null == m_cSecondaryPlayer)
                    UpdateData.velocity.X += (float)(m_dMovementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.X += (float)(m_dCombineSpeed * fTimeElapsed);
                    m_cSecondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
                else
                {
                    // TODO: Update Player shield.
                }
            }

            if (InputManager.Instance.KeyDown(m_cKeyboardBindings.MoveUp) ||
                InputManager.Instance.GamePadDown(m_ePlayerIndex, m_cGamepadBindings.MoveUp))
            {
                if (null == m_cSecondaryPlayer)
                    UpdateData.velocity.Y -= (float)(m_dMovementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.Y -= (float)(m_dCombineSpeed * fTimeElapsed);
                    m_cSecondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
            }

            if (InputManager.Instance.KeyDown(m_cKeyboardBindings.MoveDown) ||
                InputManager.Instance.GamePadDown(m_ePlayerIndex, m_cGamepadBindings.MoveDown))
            {
                if (null == m_cSecondaryPlayer)
                    UpdateData.velocity.Y += (float)(m_dMovementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.Y += (float)(m_dMovementSpeed * fTimeElapsed);
                    m_cSecondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
            }

            if (InputManager.Instance.KeyDown(m_cKeyboardBindings.Shoot) ||
                InputManager.Instance.GamePadDown(m_ePlayerIndex, m_cGamepadBindings.Shoot))
            {
                if (m_cSecondaryPlayer == null || m_bPrimaryPlayer)
                    m_cBaseWeapon.Fire();
            }

            if (InputManager.Instance.KeyPressed(m_cKeyboardBindings.Combine) ||
                InputManager.Instance.GamePadPress(m_ePlayerIndex, m_cGamepadBindings.Combine))
            {
                if (null == m_cSecondaryPlayer)
                    SendCombineRequest();
                else
                    DetachPlayer();
            }


        }

        public override void Update(double fTimeElapsed)
        {
            base.Update(fTimeElapsed);

            // Handle user input
            Input(fTimeElapsed);

            // early out if no movement
            if (UpdateData.velocity == Vector2.Zero)
                return;

            // update position
            UpdateData.position += UpdateData.velocity;

            // Make a new message
            ChangeMessage msg = new ChangeMessage();
            msg.ID = nId;
            msg.MessageType = ChangeMessageType.UpdatePosition;
            msg.Position = UpdateData.position;
            GamePlayState.Instance.addMessage(msg);

            UpdateData.velocity = Vector2.Zero;
        }

        public override void HandleCollision(CGameObject gameObject)
        {
            base.HandleCollision(gameObject);
        }

        public virtual bool SendCombineRequest()
        {
            // Send a request to combine with another player
            EventManager.Instance.SendEvent(EVENT_ID.PLAYER_COMBINE, this);
            return false;
        }

        public virtual void DetachPlayer()
        {
            // handle detaching from player and notify the other player to detach as well.
            m_cSecondaryPlayer.SetSecondaryPlayer();
            SetSecondaryPlayer();
        }

        /// <summary>
        /// HandleEvent
        /// </summary>
        /// <param name="_event"></param>
        public void HandleEvent(CEvent _event)
        {
            switch (_event.GetEventID())
            {
                case EVENT_ID.PLAYER_COMBINE:
                    {
                        if (_event.GetParam() is CPlayer && _event.GetParam() != this)
                        {
                            // TODO: Check to see if were within range of the calling player
                            SetSecondaryPlayer((CPlayer)_event.GetParam(), true);
                            m_cSecondaryPlayer.SetSecondaryPlayer(this);
                            m_cSecondaryPlayer.m_pUpdateData.position = m_pUpdateData.position;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public shooter02.GameObjects.CBaseWeapon BaseWeapon
        {
            get { return m_cBaseWeapon; }
            set { m_cBaseWeapon = value; }
        }

        public double Score
        {
            get { return m_dScore; }
            set { m_dScore = value; }
        }


        public shooter02.Managers.KeyBindings KeyboardBindings
        {
            get { return m_cKeyboardBindings; }
            set { m_cKeyboardBindings = value; }
        }

        public Microsoft.Xna.Framework.PlayerIndex PlayerIndex
        {
            get { return m_ePlayerIndex; }
            set { m_ePlayerIndex = value; }
        }

        public shooter02.Managers.ButtonBindings GamepadBindings
        {
            get { return m_cGamepadBindings; }
            set { m_cGamepadBindings = value; }
        }
    }
}
