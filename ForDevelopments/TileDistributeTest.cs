using UnityEngine;

namespace ForDevelopments
{
    public class TileDistributeTest : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private void Start()
        {
            for (var x = 0; x < 15; x++)
            {
                for (var z = 0; z < 13; z++)
                {
                    var positionX = 0.1f * x;
                    var positionZ = 0.1f * z;
                    Instantiate(prefab, new Vector3(positionX, 0.0f, positionZ), Quaternion.identity);
                }
            }
        }
    }
}
