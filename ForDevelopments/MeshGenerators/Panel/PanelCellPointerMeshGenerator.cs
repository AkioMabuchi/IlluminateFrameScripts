using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators.Panel
{
    public class PanelCellPointerMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Rect rectVertex = new(-0.05f, -0.05f, 0.1f, 0.1f);
        [SerializeField] private float baseHeight = 0.4f;


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

            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y + rectVertex.height));

            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y));

            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y));
            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y));

            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, baseHeight, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x + rectVertex.width, 0.0f, rectVertex.y + rectVertex.height));
            vertices.Add(new Vector3(rectVertex.x, 0.0f, rectVertex.y + rectVertex.height));


            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(2);
            triangles.Add(2);
            triangles.Add(1);
            triangles.Add(3);

            triangles.Add(4);
            triangles.Add(5);
            triangles.Add(6);
            triangles.Add(6);
            triangles.Add(5);
            triangles.Add(7);

            triangles.Add(8);
            triangles.Add(9);
            triangles.Add(10);
            triangles.Add(10);
            triangles.Add(9);
            triangles.Add(11);

            triangles.Add(12);
            triangles.Add(13);
            triangles.Add(14);
            triangles.Add(14);
            triangles.Add(13);
            triangles.Add(15);

            triangles.Add(16);
            triangles.Add(17);
            triangles.Add(18);
            triangles.Add(18);
            triangles.Add(17);
            triangles.Add(19);

            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);

            normals.Add(Vector3.right);
            normals.Add(Vector3.right);
            normals.Add(Vector3.right);
            normals.Add(Vector3.right);

            normals.Add(Vector3.back);
            normals.Add(Vector3.back);
            normals.Add(Vector3.back);
            normals.Add(Vector3.back);

            normals.Add(Vector3.left);
            normals.Add(Vector3.left);
            normals.Add(Vector3.left);
            normals.Add(Vector3.left);

            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            for (var i = 0; i < 20; i++)
            {
                uvs.Add(Vector2.zero);
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
