using UnityEngine;

namespace Interfaces.Tiles
{
    public interface IThrowableTile
    {
        public void Throw(Vector3 velocity, Vector3 angularVelocity);
        public void ThrowReset();
    }
}