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
        protected double movementSpeed;
        protected double combineSpeed;
        protected KeyBindings keyboardBindings;
        protected ButtonBindings gamepadBindings;
        protected PlayerIndex playerIndex;

        public CPlayer()
        {
            baseWeapon = new CBaseWeapon();
            movementSpeed = 200.0;
            combineSpeed = 5.0;
            keyboardBindings = new KeyBindings();
            gamepadBindings = new ButtonBindings();

            // Object Factory will take care of the following
            // - playerIndex
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
                else
                    UpdateData.velocity.X -= (float)(combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveRight) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveRight))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.X += (float)(movementSpeed * fTimeElapsed);
                else
                    UpdateData.velocity.X += (float)(combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveUp) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveUp))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.Y -= (float)(movementSpeed * fTimeElapsed);
                else
                    UpdateData.velocity.Y -= (float)(combineSpeed * fTimeElapsed);
            }

            if (InputManager.Instance.KeyDown(keyboardBindings.MoveDown) ||
                InputManager.Instance.GamePadDown(playerIndex, gamepadBindings.MoveDown))
            {
                if (null == secondaryPlayer)
                    UpdateData.velocity.Y += (float)(movementSpeed * fTimeElapsed);
                else
                    UpdateData.velocity.Y += (float)(movementSpeed * fTimeElapsed);
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
