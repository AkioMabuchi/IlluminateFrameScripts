using UnityEngine;

namespace Views
{
    public class ViewParams : MonoBehaviour
    {
        [SerializeField] private float tileSize;
        public float TileSize => tileSize;

        [SerializeField] private Vector3 boardBasePositionSmall;
        public Vector3 BoardBasePositionSmall => boardBasePositionSmall;
        [SerializeField] private Vector3 boardBasePositionMedium;
        public Vector3 BoardBasePositionMedium => boardBasePositionMedium;
        [SerializeField] private Vector3 boardBasePositionLarge;
        public Vector3 BoardBasePositionLarge => boardBasePositionLarge;
    }
}