using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class FrameMeshGenerator: MonoBehaviour
    {
        [Serializable]
        private struct Section
        {
            public Vector2 vertex;
            public float normal;
        }
        
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Vector2 boardSize;
        [SerializeField] private Section[] frameSections;
        
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
            
            for (var r = 0; r < 4; r++)
            {
                var rot = Quaternion.Euler(0.0f, r * 90.0f, 0.0f);
                foreach(var frameSection in frameSections)
                {
                    var vx = r % 2 == 0 ? frameSection.vertex.x + boardSize.x : frameSection.vertex.x + boardSize.y;
                    var vy = frameSection.vertex.y;
                    var vz = r % 2 == 0 ? frameSection.vertex.x + boardSize.y : frameSection.vertex.x + boardSize.x;
                    var nx = Mathf.Cos(frameSection.normal * Mathf.Deg2Rad);
                    var ny = Mathf.Sin(frameSection.normal * Mathf.Deg2Rad);
                    vertices.Add(rot * new Vector3(vx, vy, vz));
                    vertices.Add(rot * new Vector3(vx, vy, -vz));
                    normals.Add(rot * new Vector3(nx, ny, 0.0f));
                    normals.Add(rot * new Vector3(nx, ny, 0.0f));
                }
            }

            for (var r = 0; r < 4; r++)
            {
                for (var s = 1; s < frameSections.Length; s++)
                {
                    if (frameSections[s - 1].vertex == frameSections[s].vertex)
                    {
                        continue;
                    }

                    var va = r * frameSections.Length * 2 + (s - 1) * 2;
                    var vb = r * frameSections.Length * 2 + s * 2;
                    var vc = r * frameSections.Length * 2 + (s - 1) * 2 + 1;
                    var vd = r * frameSections.Length * 2 + s * 2 + 1;
                    
                    triangles.Add(va);
                    triangles.Add(vb);
                    triangles.Add(vc);
                    triangles.Add(vc);
                    triangles.Add(vb);
                    triangles.Add(vd);
                }
            }

            var min = 0.0f;
            var max = 0.0f;
            foreach (var vertex in vertices)
            {
                min = Mathf.Min(min, vertex.x);
                max = Mathf.Max(max, vertex.x);
            }

            foreach (var vertex in vertices)
            {
                var u = Mathf.InverseLerp(min, max, vertex.x);
                var v = Mathf.InverseLerp(min, max, vertex.z);
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