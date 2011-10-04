using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Managers;
using Microsoft.Xna.Framework;
using shooter02.Threading;
using shooter02.GameStates;

namespace shooter02.GameObjects
{
    class CPlayer : CGameObject
    {
        // TODO: implement CPlayer
        protected CBaseWeapon baseWeapon;
        protected CPlayer secondaryPlayer;
        protected float movementSpeed;
        protected float combineSpeed;
        protected KeyBindings keyboardBindings;
        protected ButtonBindings gamepadBindings;
        protected PlayerIndex playerIndex;

        public CPlayer()
        {
        }

        public virtual void Input(float fTimeElapsed)
        {
            // TODO: FIX: Make sure to only accept keyboard input if player one is not using a gamepad
            if (InputManager.Instance.KeyDown(keyboardBindings.MoveLeft) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveLeft))
            {
                if (null == secondaryPlayer)
                    m_pUpdateData.velocity.X -= (movementSpeed * fTimeElapsed);
                else
                    m_pUpdateData.velocity.X -= (combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveRight) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveRight))
            {
                if (null == secondaryPlayer)
                    m_pUpdateData.velocity.X += (movementSpeed * fTimeElapsed);
                else
                    m_pUpdateData.velocity.X += (combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveUp) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveUp))
            {
                if (null == secondaryPlayer)
                    m_pUpdateData.velocity.Y -= (movementSpeed * fTimeElapsed);
                else
                    m_pUpdateData.velocity.Y -= (combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveDown) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveDown))
            {
                if (null == secondaryPlayer)
                    m_pUpdateData.velocity.Y += (movementSpeed * fTimeElapsed);
                else
                    m_pUpdateData.velocity.Y += (movementSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.Shoot) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.Shoot))
            {
                baseWeapon.Fire();
            }

            if (InputManager.Instance.KeyPressed(keyboardBindings.Combine) ||
                InputManager.Instance.GamePadPress(playerIndex, gamepadBindings.Combine))
            {
                if (null == secondaryPlayer)
                    handleCombineRequest();
                else
                    detachPlayer(secondaryPlayer);
            }


        }

        public override void Update(float fTimeElapsed)
        {
            base.Update(fTimeElapsed);

            Input(fTimeElapsed);

            // early out if no movement
            if (m_pUpdateData.velocity == Vector2.Zero)
                return;

            // update position
            m_pUpdateData.position += m_pUpdateData.velocity;

            // Make a new message
            ChangeMessage msg = new ChangeMessage();
            // TODO: design system to handle objects
            //msg.ID
            msg.MessageType = ChangeMessageType.UpdatePosition;
            msg.Position = m_pUpdateData.position;
            GamePlayState.Instance.addMessage(msg);

            m_pUpdateData.velocity = Vector2.Zero;
        }

        public override void HandleCollision(CGameObject gameObject)
        {
            base.HandleCollision(gameObject);
        }

        public virtual bool handleCombineRequest()
        {
            // TODO: Make sure a second player is within range, combine with player.
            return false;
        }

        public virtual void detachPlayer(CPlayer secondPlayer)
        {
            // TODO: handle detaching from player and notify the other player to detach as well.
        }
        
    }
}
