using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shooter02.Threading
{
    public enum ChangeMessageType
    {
        UpdateCameraView,
        UpdatePosition,
        UpdateHighlightColor,
        CreateNewRenderData,
        DeleteRenderData,
    }
}
