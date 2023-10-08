using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class FrameBulbMediumTerminalMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private int count;
        [SerializeField] private float topHeight;
        [SerializeField] private float mediumHeight;
        [SerializeField] private float mediumRadius;
        [SerializeField] private float bottomRadius;

        [SerializeField] private float u;
        
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
            var uvs = new List<Vector2>();

            var radius = new List<float>
            {
                0.0f,
                mediumRadius,
                bottomRadius,
            };
            var height = new List<float>
            {
                topHeight,
                mediumHeight,
                0.0f
            };

            for (var h = 0; h < 2; h++)
            {
                for (var r = 0; r < count; r++)
                {
                    var radA = Mathf.PI * 2.0f * r / count;
                    var radB = Mathf.PI * 2.0f * (r + 1) / count;
                    var xa = Mathf.Cos(radA) * radius[h];
                    var za = Mathf.Sin(radA) * radius[h];
                    var xb = Mathf.Cos(radB) * radius[h];
                    var zb = Mathf.Sin(radB) * radius[h];
                    var xc = Mathf.Cos(radA) * radius[h + 1];
                    var zc = Mathf.Sin(radA) * radius[h + 1];
                    var xd = Mathf.Cos(radB) * radius[h + 1];
                    var zd = Mathf.Sin(radB) * radius[h + 1];

                    vertices.Add(new Vector3(xa, height[h], za));
                    vertices.Add(new Vector3(xb, height[h], zb));
                    vertices.Add(new Vector3(xc, height[h + 1], zc));
                    vertices.Add(new Vector3(xd, height[h + 1], zd));

                    uvs.Add(new Vector2(u, 0.0f));
                    uvs.Add(new Vector2(u, 0.0f));
                    uvs.Add(new Vector2(u, 0.0f));
                    uvs.Add(new Vector2(u, 0.0f));
                }
            }


            for (var h = 0; h < 2; h++)
            {
                for (var i = 0; i < count; i++)
                {
                    var va = count * h * 4 + i * 4;
                    var vb = count * h * 4 + i * 4 + 1;
                    var vc = count * h * 4 + i * 4 + 2;
                    var vd = count * h * 4 + i * 4 + 3;

                    triangles.Add(va);
                    triangles.Add(vb);
                    triangles.Add(vc);
                    triangles.Add(vc);
                    triangles.Add(vb);
                    triangles.Add(vd);
                }
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }
    }
}