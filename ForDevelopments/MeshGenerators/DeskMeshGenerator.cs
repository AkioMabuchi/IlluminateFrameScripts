using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class DeskMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private float width;
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilter == null)
            {
                return;
            }


            var halfWidth = width * 0.5f;
            var halfDepth = width * 0.3125f;
            var height = width * 0.375f;
            
            var mesh = new Mesh();
            
            var vertices = new List<Vector3>
            {
                new(-halfWidth, 0.0f, halfDepth),
                new(halfWidth, 0.0f, halfDepth),
                new(-halfWidth, 0.0f, -halfDepth),
                new(halfWidth, 0.0f, -halfDepth),
                new(-halfWidth, 0.0f, -halfDepth),
                new(halfWidth, 0.0f, -halfDepth),
                new(-halfWidth, -height, -halfDepth),
                new(halfWidth, -height, -halfDepth),
            };
            var normals = new List<Vector3>
            {
                Vector3.up,
                Vector3.up,
                Vector3.up,
                Vector3.up,
                Vector3.back,
                Vector3.back,
                Vector3.back,
                Vector3.back
            };
            var uvs = new List<Vector2>
            {
                new(0.0f, 1.0f),
                new(1.0f, 1.0f),
                new(0.0f, 0.375f),
                new(1.0f, 0.375f),
                new(0.0f, 0.375f),
                new(1.0f, 0.375f),
                new(0.0f, 0.0f),
                new(1.0f, 0.0f),
            };
            var triangles = new List<int>
            {
                0, 1, 2,
                2, 1, 3,
                4, 5, 6,
                6, 5, 7
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