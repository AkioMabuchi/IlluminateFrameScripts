using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    [Serializable]
    public struct Surface
    {
        public SurfacePoint[] points;
        public Vector3Int[] triangles;
        public Vector3 eulerAngle;
        public Vector3 offset;
        public int submesh;
    }

    [Serializable]
    public struct SurfacePoint
    {
        public Vector2 vertex;
        public Vector2 uv;
    }
    public class SurfacesMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Surface[] surfaces;
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilter == null)
            {
                return;
            }

            var mesh = new Mesh();
            var vertices = new List<Vector3>();

            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();

            var submesh = 0;
            foreach (var surface in surfaces)
            {
                submesh = Mathf.Max(surface.submesh, submesh);
            }

            mesh.subMeshCount = submesh + 1;

            var triangles = new List<List<int>>();

            for (var i = 0; i < mesh.subMeshCount; i++)
            {
                triangles.Add(new List<int>());
            }

            var triangleIndex = 0;
            
            foreach (var surface in surfaces)
            {
                var normal = Vector3.Cross(
                    Quaternion.Euler(surface.eulerAngle) * surface.points[1].vertex -
                    Quaternion.Euler(surface.eulerAngle) * surface.points[0].vertex,
                    Quaternion.Euler(surface.eulerAngle) * surface.points[2].vertex -
                    Quaternion.Euler(surface.eulerAngle) * surface.points[0].vertex).normalized;
                
                foreach (var point in surface.points)
                {
                    var vertex = Quaternion.Euler(surface.eulerAngle) * point.vertex + surface.offset;
                    vertices.Add(vertex);
                    normals.Add(normal);
                    uvs.Add(point.uv);
                }
                foreach (var triangle in surface.triangles)
                {
                    triangles[surface.submesh].Add(triangle.x + triangleIndex);
                    triangles[surface.submesh].Add(triangle.y + triangleIndex);
                    triangles[surface.submesh].Add(triangle.z + triangleIndex);
                }
                
                triangleIndex += surface.points.Length;
            }

            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);

            for (var i = 0; i < triangles.Count; i++)
            {
                mesh.SetTriangles(triangles[i], i);
            }

            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }
    }
}
