using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments
{
    public class MeshUnifier : MonoBehaviour
    {
        [SerializeField] private Mesh[] meshes;

        [SerializeField] private MeshFilter meshFilter;
        
        [ContextMenu("Unify Mesh")]
        private void UnifyMesh()
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

            var currentVertexCount = 0;

            foreach (var baseMesh in meshes)
            {
                foreach (var vertex in baseMesh.vertices)
                {
                    vertices.Add(vertex);
                }

                foreach (var triangle in baseMesh.triangles)
                {
                    triangles.Add(triangle + currentVertexCount);
                }

                foreach (var normal in baseMesh.normals)
                {
                    normals.Add(normal);
                }

                foreach (var uv in baseMesh.uv)
                {
                    uvs.Add(uv);
                }

                currentVertexCount = vertices.Count;
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
