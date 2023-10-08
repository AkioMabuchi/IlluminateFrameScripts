using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class FrameLargeSideBulbsGroupMeshGenerator : MonoBehaviour
    {
        private struct Akio
        {
            public List<Vector3> vertices;
            public Vector3 normal;
            public int trianglesIndex;
        }

        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Vector2 size;
        [SerializeField] private float top;
        [SerializeField] private float bottom;

        [SerializeField] private int count;
        [SerializeField] private float startPositionZ;
        [SerializeField] private float pitchPositionZ;

        [SerializeField] private int[] skipIndexes;
        
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

            var hashSetSkipIndexes = new HashSet<int>(skipIndexes);

            var triangleIndex = 0;
            for (var i = 0; i < count; i++)
            {
                if (hashSetSkipIndexes.Contains(i))
                {
                    continue;
                }
                
                var right = size.x * 0.5f;
                var left = size.x * 0.5f - size.x;
                var forward = size.y * 0.5f + startPositionZ + pitchPositionZ * i;
                var back = size.y * 0.5f - size.y + startPositionZ + pitchPositionZ * i;

                var vertexA = new Vector3(left, top, forward);
                var vertexB = new Vector3(right, top, forward);
                var vertexC = new Vector3(left, top, back);
                var vertexD = new Vector3(right, top, back);
                var vertexE = new Vector3(left, bottom, forward);
                var vertexF = new Vector3(right, bottom, forward);
                var vertexG = new Vector3(left, bottom, back);
                var vertexH = new Vector3(right, bottom, back);

                foreach (var akio in new List<Akio>
                         {
                             new()
                             {
                                 vertices = new List<Vector3>
                                 {
                                     vertexA,
                                     vertexB,
                                     vertexC,
                                     vertexD
                                 },
                                 normal = Vector3.up,
                                 trianglesIndex = 0
                             },
                             new()
                             {
                                 vertices = new List<Vector3>
                                 {
                                     vertexD,
                                     vertexB,
                                     vertexH,
                                     vertexF,
                                 },
                                 normal = Vector3.right,
                                 trianglesIndex = 4
                             },
                             new()
                             {
                                 vertices = new List<Vector3>
                                 {
                                     vertexB,
                                     vertexA,
                                     vertexF,
                                     vertexE
                                 },
                                 normal = Vector3.forward,
                                 trianglesIndex = 8
                             },
                             new()
                             {
                                 vertices = new List<Vector3>
                                 {
                                     vertexA,
                                     vertexC,
                                     vertexE,
                                     vertexG,
                                 },
                                 normal = Vector3.left,
                                 trianglesIndex = 12
                             },
                             new()
                             {
                                 vertices = new List<Vector3>
                                 {
                                     vertexC,
                                     vertexD,
                                     vertexG,
                                     vertexH,
                                 },
                                 normal = Vector3.back,
                                 trianglesIndex = 16
                             }
                         })
                {
                    foreach (var vertex in akio.vertices)
                    {
                        vertices.Add(vertex);
                        normals.Add(akio.normal);
                        uvs.Add(new Vector2(Mathf.InverseLerp(0.0f, count, i), 0.0f));
                    }

                    triangles.Add(akio.trianglesIndex + triangleIndex);
                    triangles.Add(akio.trianglesIndex + triangleIndex + 1);
                    triangles.Add(akio.trianglesIndex + triangleIndex + 2);
                    triangles.Add(akio.trianglesIndex + triangleIndex + 2);
                    triangles.Add(akio.trianglesIndex + triangleIndex + 1);
                    triangles.Add(akio.trianglesIndex + triangleIndex + 3);


                }
                triangleIndex += 20;
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