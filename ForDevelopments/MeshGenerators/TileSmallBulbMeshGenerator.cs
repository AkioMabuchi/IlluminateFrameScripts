using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileSmallBulbMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private float radius;
        [SerializeField] private int divCountX;
        [SerializeField] private int divCountY;
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

            for (var y = 0; y <= divCountY; y++)
            {
                var vy = Mathf.Cos(Mathf.PI * 0.5f * y / divCountY);
                var vr = Mathf.Sin(Mathf.PI * 0.5f * y / divCountY);
                for (var x = 0; x <= divCountX; x++)
                {
                    var vx = Mathf.Cos(Mathf.PI * 2.0f * x / divCountX) * vr;
                    var vz = Mathf.Sin(Mathf.PI * 2.0f * x / divCountX) * vr;
                    var v = new Vector3(vx, vy, vz);
                    vertices.Add(v * radius + new Vector3(0.0f, baseHeight + 0.0f));
                    normals.Add(v);
                    uvs.Add(new Vector2((float) x / divCountX, 0.0f));
                }
            }
            
            
            for (var i = 0; i < divCountX; i++)
            {
                for (var j = 0; j < divCountY; j++)
                {

                    var indexA = i + 1 + (j + 1) * (divCountX + 1);
                    var indexB = i + (j + 1) * (divCountX + 1);
                    var indexC = i + j * (divCountX + 1);
                    var indexD = i + 1 + j * (divCountX + 1);

                    triangles.Add(indexA);
                    triangles.Add(indexB);
                    triangles.Add(indexD);
                    triangles.Add(indexD);
                    triangles.Add(indexB);
                    triangles.Add(indexC);
                }
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
