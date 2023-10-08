using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class MeshModifier : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilterInput;

        [SerializeField] private Vector3 rotation;
        [SerializeField] private Vector3 scale;
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilterInput == null)
            {
                return;
            }

            var newGameObject = new GameObject();
            var meshFilter = newGameObject.AddComponent<MeshFilter>();
            newGameObject.AddComponent<MeshRenderer>();

            var sharedMesh = meshFilterInput.sharedMesh;
            var mesh = new Mesh();
            var vertices = new List<Vector3>(sharedMesh.vertices);
            var triangles = new List<int>(sharedMesh.triangles);
            var normals = new List<Vector3>(sharedMesh.normals);
            var uvs = new List<Vector2>(sharedMesh.uv);

            for (var i = 0; i < vertices.Count; i++)
            {
                vertices[i] = Quaternion.Euler(rotation) * Vector3.Scale(vertices[i], scale);
            }
            

            for (var i = 0; i < normals.Count; i++)
            {
                normals[i] = Quaternion.Euler(rotation) * normals[i];
            }
            
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }
    
    }
}
