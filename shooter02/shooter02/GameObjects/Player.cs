using System;
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
        protected CBaseWeapon baseWeapon;
        protected CPlayer secondaryPlayer;
        protected double movementSpeed;
        protected double combineSpeed;
        protected KeyBindings keyboardBindings;
        protected ButtonBindings gamepadBindings;
        protected PlayerIndex playerIndex;
        protected bool m_bPrimaryPlayer;

        public CPlayer()
        {
            baseWeapon = new CBaseWeapon();
            movementSpeed = 200.0;
            combineSpeed = 5.0;
            keyboardBindings = new KeyBindings();
            gamepadBindings = new ButtonBindings();
            m_bPrimaryPlayer = false;

            // Object Factory will take care of the following
            // - playerIndex

            EventManager.Instance.RegisterEvent(EVENT_ID.PLAYER_COMBINE, this);
        }

        public void setSecondaryPlayer(CPlayer _player = null, bool primary = false)
        {
            secondaryPlayer = _player;
            m_bPrimaryPlayer = primary;
        }

        public void setPlayerIndex(PlayerIndex index)
        {
            playerIndex = index;
        }

        public virtual void Input(double fTimeElapsed)
        {
            // TODO: FIX: Make sure to only accept keyboard input if player one is not using a gamepad
            if (InputManager.Instance.KeyDown(keyboardBindings.MoveLeft) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveLeft))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.X -= (float)(movementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.X -= (float)(combineSpeed * fTimeElapsed);
                    secondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
                else
                {
                    // TODO: Update Player shield.
                }
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveRight) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveRight))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.X += (float)(movementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.X += (float)(combineSpeed * fTimeElapsed);
                    secondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
                else
                {
                    // TODO: Update Player shield.
                }
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveUp) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveUp))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.Y -= (float)(movementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.Y -= (float)(combineSpeed * fTimeElapsed);
                    secondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveDown) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveDown))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.Y += (float)(movementSpeed * fTimeElapsed);
                else if (m_bPrimaryPlayer)
                {
                    m_pUpdateData.velocity.Y += (float)(movementSpeed * fTimeElapsed);
                    secondaryPlayer.m_pUpdateData.velocity = this.m_pUpdateData.velocity;
                }
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.Shoot) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.Shoot))
            {
                if (secondaryPlayer == null || m_bPrimaryPlayer)
                    baseWeapon.Fire();
            }

            if (InputManager.Instance.KeyPressed(keyboardBindings.Combine) ||
                InputManager.Instance.GamePadPress(playerIndex, gamepadBindings.Combine))
            {
                if (null == secondaryPlayer)
                    sendCombineRequest();
                else
                    detachPlayer();
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

        public virtual bool sendCombineRequest()
        {
            // Send a request to combine with another player
            EventManager.Instance.SendEvent(EVENT_ID.PLAYER_COMBINE, this);
            return false;
        }

        public virtual void detachPlayer()
        {
            // handle detaching from player and notify the other player to detach as well.
            secondaryPlayer.setSecondaryPlayer();
            setSecondaryPlayer();
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
                            setSecondaryPlayer((CPlayer)_event.GetParam(), true);
                            secondaryPlayer.setSecondaryPlayer(this);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
