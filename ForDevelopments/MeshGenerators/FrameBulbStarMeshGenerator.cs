using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class FrameBulbStarMeshGenerator: MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private float height;
        [SerializeField] private float innerRadius;
        [SerializeField] private float outerRadius;

        [SerializeField] private float u;
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            var newGameObject = new GameObject();
            var meshFilter = newGameObject.AddComponent<MeshFilter>();
            newGameObject.AddComponent<MeshRenderer>();

            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();


            for (var r = 0; r <= count * 2; r++)
            {
                var rad = Mathf.PI * r / count;
                var x = Mathf.Cos(rad) * (r % 2 == 0 ? innerRadius : outerRadius);
                var z = Mathf.Sin(rad) * (r % 2 == 0 ? innerRadius : outerRadius);

                vertices.Add(new Vector3(x, height, z));
                uvs.Add(new Vector2(u, 0.0f));
            }

            for (var r = 0; r < count * 2; r++)
            {
                var radA = Mathf.PI * r / count;
                var radB = Mathf.PI * (r + 1) / count;
                var xa = Mathf.Cos(radA) * (r % 2 == 0 ? innerRadius : outerRadius);
                var za = Mathf.Sin(radA) * (r % 2 == 0 ? innerRadius : outerRadius);
                var xb = Mathf.Cos(radB) * (r % 2 == 0 ? outerRadius : innerRadius);
                var zb = Mathf.Sin(radB) * (r % 2 == 0 ? outerRadius : innerRadius);

                vertices.Add(new Vector3(xa, height, za));
                vertices.Add(new Vector3(xb, height, zb));
                vertices.Add(new Vector3(xa, 0.0f, za));
                vertices.Add(new Vector3(xb, 0.0f, zb));
                
                uvs.Add(new Vector2(u, 0.0f));
                uvs.Add(new Vector2(u, 0.0f));
                uvs.Add(new Vector2(u, 0.0f));
                uvs.Add(new Vector2(u, 0.0f));
            }

            vertices.Add(new Vector3(0.0f, height, 0.0f));
            uvs.Add(new Vector2(u, 0.0f));

            for (var i = 0; i < count * 2; i++)
            {
                triangles.Add(i + 1);
                triangles.Add(i);
                triangles.Add(vertices.Count - 1);
            }
            
            for (var i = 0; i < count * 2; i++)
            {
                var va = count * 2 + i * 4 + 1;
                var vb = count * 2 + i * 4 + 2;
                var vc = count * 2 + i * 4 + 3;
                var vd = count * 2 + i * 4 + 4;
                
                triangles.Add(va);
                triangles.Add(vb);
                triangles.Add(vc);
                triangles.Add(vc);
                triangles.Add(vb);
                triangles.Add(vd);
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