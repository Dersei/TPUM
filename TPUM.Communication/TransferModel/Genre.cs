using System;

namespace TPUM.Communication.TransferModel
{
    [Flags]
    public enum Genre : uint
    {
        RPG = 1,
        FPS = 2,
        RTS = 4,
        WalkingSim = 8,
        Adventure = 16,
        TPS = 32,
        ImmersiveSim = 64,
        Action = 128
    }
}