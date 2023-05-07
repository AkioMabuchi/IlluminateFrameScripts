using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileSineSymbolMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Vector2 scale = Vector2.one;
        [SerializeField] private Vector2 offset = Vector2.zero;
        
        [SerializeField] private float thickness;
        [SerializeField] private int divCount;
        [SerializeField] private float normalDegree;
        [SerializeField] private float startHeight;
        [SerializeField] private float endHeight;

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

            var points = new List<Vector2>();

            for (var i = 0; i <= divCount; i++)
            {
                var cx = i * 2.0f / divCount - 1.0f;
                var cy = Mathf.Sin(Mathf.PI * 2.0f * i / divCount);

                var deg = Mathf.Atan2(1.0f, -Mathf.Cos(Mathf.PI * 2.0f * i / divCount) * Mathf.PI);

                var px = cx + Mathf.Cos(deg) * thickness;
                var py = cy + Mathf.Sin(deg) * thickness;

                points.Add(new Vector2(px, py));
            }


            for (var i = 0; i <= divCount; i++)
            {
                var cx = i * -2.0f / divCount + 1.0f;
                var cy = Mathf.Sin(Mathf.PI * -2.0f * i / divCount);

                var deg = Mathf.Atan2(1.0f, -Mathf.Cos(Mathf.PI * 2.0f * i / divCount) * Mathf.PI);

                var px = cx - Mathf.Cos(deg) * thickness;
                var py = cy - Mathf.Sin(deg) * thickness;

                points.Add(new Vector2(px, py));
            }
            
            points.Add(points[0]);

            var triangleIndexes = new List<Vector3Int>();
            for (var i = 0; i <= divCount; i++)
            {
                triangleIndexes.Add(new Vector3Int(i, i + 1, divCount * 2 - i));
                triangleIndexes.Add(new Vector3Int(i, divCount * 2 - i, divCount * 2 - i + 1));
            }

            foreach (var point in points)
            {
                vertices.Add(new Vector3(point.x * scale.x, startHeight, point.y * scale.y));
                normals.Add(Vector3.up);
                uvs.Add(Vector2.zero);
            }
            
            for (var i = 1; i < points.Count; i++)
            {
                if (points[i] == points[i - 1])
                {
                    continue;
                }

                var deg = Mathf.Atan2(points[i].y - points[i - 1].y, points[i].x - points[i - 1].x) * Mathf.Rad2Deg;

                var n = Quaternion.Euler(new Vector3(0.0f, -deg, 0.0f)) *
                        Quaternion.Euler(new Vector3(0.0f, 0.0f, normalDegree)) * Vector3.right;

                var pa = new Vector2(points[i - 1].x * scale.x, points[i - 1].y * scale.y);
                var pb = new Vector2(points[i].x * scale.x, points[i].y * scale.y);

                vertices.Add(new Vector3(pa.x, startHeight, pa.y));
                vertices.Add(new Vector3(pb.x, startHeight, pb.y));
                vertices.Add(new Vector3(pa.x, endHeight, pa.y));
                vertices.Add(new Vector3(pb.x, endHeight, pb.y));

                normals.Add(n);
                normals.Add(n);
                normals.Add(n);
                normals.Add(n);

                uvs.Add(Vector2.zero);
                uvs.Add(Vector2.zero);
                uvs.Add(Vector2.zero);
                uvs.Add(Vector2.zero);

                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
            }

            for (var i = 0; i < vertices.Count; i++)
            {
                vertices[i] += new Vector3(offset.x, 0.0f, offset.y);
            }

            foreach (var triangleIndex in triangleIndexes)
            {
                triangles.Add(triangleIndex.x);
                triangles.Add(triangleIndex.y);
                triangles.Add(triangleIndex.z);
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
