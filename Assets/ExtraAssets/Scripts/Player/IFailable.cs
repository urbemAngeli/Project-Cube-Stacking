using System;

namespace ExtraAssets.Scripts.Player
{
    public interface IFailable
    {
        event Action OnFail;
    }
}