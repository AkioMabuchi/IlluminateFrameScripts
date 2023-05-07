using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators.Panel
{
    public class PanelCellDetectorMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Rect rectVertex = new(-0.05f, -0.05f, 0.1f, 0.1f);
    
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilter == null)
            {
                return;
            }

            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();

            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y));

            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(2);
            triangles.Add(2);
            triangles.Add(1);
            triangles.Add(3);

            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);

            uvs.Add(Vector2.zero);
            uvs.Add(Vector2.zero);
            uvs.Add(Vector2.zero);
            uvs.Add(Vector2.zero);

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
