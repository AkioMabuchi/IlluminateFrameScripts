using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class RectangleMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Vector2 size;
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilter == null)
            {
                return;
            }

            var mesh = new Mesh();
            var vertices = new List<Vector3>
            {
                new(size.x * -0.5f, size.y * 0.5f, 0.0f),
                new(size.x * 0.5f, size.y * 0.5f, 0.0f),
                new(size.x * -0.5f, size.y * -0.5f, 0.0f),
                new(size.x * 0.5f, size.y * -0.5f, 0.0f),
            };
            var triangles = new List<int>
            {
                0, 1, 2, 2, 1, 3
            };
            var normals = new List<Vector3>
            {
                Vector3.back,
                Vector3.back,
                Vector3.back,
                Vector3.back,
            };
            var uvs = new List<Vector2>
            {
                new(0.0f, 1.0f),
                new(1.0f, 1.0f),
                new(0.0f, 0.0f),
                new(1.0f, 0.0f)
            };
            
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