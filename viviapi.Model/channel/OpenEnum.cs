using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public enum OpenEnum
    {
        None = 0,
        AllClose = 1,
        AllOpen = 2,
        Close = 4,
        Open = 8,
    }
}
