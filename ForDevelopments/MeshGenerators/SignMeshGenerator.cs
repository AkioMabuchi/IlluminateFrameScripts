using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class SignMeshGenerator : MonoBehaviour
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

            var mesh = new Mesh();

            var vertices = new List<Vector3>
            {
                new(-halfWidth, 0.0f, halfWidth),
                new(halfWidth, 0.0f, halfWidth),
                new(-halfWidth, 0.0f, -halfWidth),
                new(halfWidth, 0.0f, -halfWidth),
            };
            var normals = new List<Vector3>
            {
                Vector3.up,
                Vector3.up,
                Vector3.up,
                Vector3.up,
            };
            var uvs = new List<Vector2>
            {
                new(0.0f, 1.0f),
                new(1.0f, 1.0f),
                new(0.0f, 0.0f),
                new(1.0f, 0.0f),
            };
            var triangles = new List<int>
            {
                0, 1, 2,
                2, 1, 3,
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