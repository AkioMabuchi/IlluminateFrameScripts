using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileSymbolMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        
        [SerializeField] private Vector2 scale = Vector2.one;
        [SerializeField] private Vector2 offset=Vector2.zero;
        
        [SerializeField] private Vector2[] points;
        [SerializeField] private Vector3Int[] triangleIndexes;
        
        [SerializeField] private float normalDegree;
        [SerializeField] private float startHeight;
        [SerializeField] private float endHeight;

        [SerializeField] private bool flipMesh;
        
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

            foreach (var point in points)
            {
                vertices.Add(new Vector3(point.x * scale.x, startHeight, point.y * scale.y));
                normals.Add(Vector3.up);
                uvs.Add(Vector2.zero);
            }

            for (var i = 1; i < points.Length; i++)
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

                if (flipMesh)
                {
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 1);
                }
                else
                {
                    triangles.Add(vertices.Count - 4);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 2);
                    triangles.Add(vertices.Count - 3);
                    triangles.Add(vertices.Count - 1);
                }
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
