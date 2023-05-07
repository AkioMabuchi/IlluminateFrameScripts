using UnityEditor;
using UnityEngine;

namespace ForDevelopments
{
    public class MeshSaver : MonoBehaviour
    {
        
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private string fileName;
        
        [ContextMenu("SaveMesh")]
        private void SaveMesh()
        {
            if (meshFilter == null || fileName == "")
            {
                return;
            }
#if UNITY_EDITOR
            var mesh = meshFilter.sharedMesh;
            AssetDatabase.CreateAsset(mesh, "Assets/Meshes/" + fileName + ".asset");
            fileName = "";
#endif
        }
    }
}
