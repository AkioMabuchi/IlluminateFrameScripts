using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators.Panel
{
    public class PanelFrameMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Rect outerRectVertex;
        [SerializeField] private Rect innerRectVertex;
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


            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight,
                outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight, outerRectVertex.y));

            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight,
                innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight, innerRectVertex.y));

            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight,
                outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, 0.0f, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, 0.0f,
                outerRectVertex.y + outerRectVertex.height));

            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x, 0.0f, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, 0.0f, outerRectVertex.y));

            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y));
            vertices.Add(new Vector3(outerRectVertex.x, 0.0f, outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x, 0.0f, outerRectVertex.y));

            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, baseHeight,
                outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x, baseHeight, outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x + outerRectVertex.width, 0.0f,
                outerRectVertex.y + outerRectVertex.height));
            vertices.Add(new Vector3(outerRectVertex.x, 0.0f, outerRectVertex.y + outerRectVertex.height));





            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x, 0.0f, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x, 0.0f, innerRectVertex.y + innerRectVertex.height));

            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight,
                innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x, 0.0f, innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, 0.0f,
                innerRectVertex.y + innerRectVertex.height));

            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight,
                innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, 0.0f,
                innerRectVertex.y + innerRectVertex.height));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, 0.0f, innerRectVertex.y));

            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, baseHeight, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x, baseHeight, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x + innerRectVertex.width, 0.0f, innerRectVertex.y));
            vertices.Add(new Vector3(innerRectVertex.x, 0.0f, innerRectVertex.y));

            triangles.Add(1);
            triangles.Add(5);
            triangles.Add(0);
            triangles.Add(0);
            triangles.Add(5);
            triangles.Add(4);

            triangles.Add(1);
            triangles.Add(3);
            triangles.Add(5);
            triangles.Add(5);
            triangles.Add(3);
            triangles.Add(7);
        
            triangles.Add(3);
            triangles.Add(2);
            triangles.Add(7);
            triangles.Add(7);
            triangles.Add(2);
            triangles.Add(6);
        
            triangles.Add(2);
            triangles.Add(0);
            triangles.Add(6);
            triangles.Add(6);
            triangles.Add(0);
            triangles.Add(4);




            for (var i = 2; i < 10; i++)
            {
                triangles.Add(i * 4);
                triangles.Add(i * 4 + 1);
                triangles.Add(i * 4 + 2);
                triangles.Add(i * 4 + 2);
                triangles.Add(i * 4 + 1);
                triangles.Add(i * 4 + 3);
            }

            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);

            for (var i = 0; i < 2; i++)
            {
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
            }

            foreach (var vertex in vertices)
            {
                var u = Mathf.InverseLerp(outerRectVertex.x, outerRectVertex.x + outerRectVertex.width, vertex.x);
                var v = Mathf.InverseLerp(outerRectVertex.x, outerRectVertex.x + outerRectVertex.width, vertex.z);
                uvs.Add(new Vector2(u, v));
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
