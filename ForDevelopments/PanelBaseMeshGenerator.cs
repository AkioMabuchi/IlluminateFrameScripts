using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments
{
    public class PanelBaseMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Rect rectVertex;
        [SerializeField] private Rect rectUV;
        [SerializeField] private float baseHeight;
    

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

            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y));

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

            uvs.Add(new Vector2(rectUV.x, rectUV.y + rectUV.height));
            uvs.Add(new Vector2(rectUV.x + rectUV.width, rectUV.y + rectUV.height));
            uvs.Add(new Vector2(rectUV.x, rectUV.y));
            uvs.Add(new Vector2(rectUV.x + rectUV.width, rectUV.y));

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
