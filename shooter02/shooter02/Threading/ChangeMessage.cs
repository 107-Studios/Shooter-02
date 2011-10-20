using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace shooter02.Threading
{
    // the following tag is used to create a union
    [StructLayout(LayoutKind.Explicit)]
    public struct ChangeMessage
    {
        // this appears in all messagetypes
        // ID determining the way the message will be interpreted
        [FieldOffset(0)]
        public ChangeMessageType MessageType;

        // this is required when the message is of type UpdateCameraView
        [FieldOffset(4)]
        public Matrix CameraViewMatrix;

        // this is required when the message is dealing with a certain entity
        [FieldOffset(4)]
        public int ID;

        // this is required when the message is of type UpdateWorldMatrix
        [FieldOffset(8)]
        public Vector2 Position;

        // this is required when the message is of type UpdateHighlightColor
        [FieldOffset(16)]
        public Color HighlightColor;

        // this is required when the message is of type CreateNewRenderData
        [FieldOffset(32)]
        public int texture2D;

        // nothing extra is needed for the message type DeleteRenderData
    }
}
