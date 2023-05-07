using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    [Serializable]
    public struct SectionParamsGroup
    {
        public Vector2 baseVertex;
        public float baseNormalDegree;
    }

    [Serializable]
    public struct RadialSectionParamsGroup
    {
        public Vector2 position;
        public float normalDegree;
    }
    public class TileLineMeshGenerator : MonoBehaviour
    {
        [Serializable]
        public enum MeshMode
        {
            Straight,
            Curve,
            Jump
        }
        
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshMode meshMode;

        [SerializeField] private SectionParamsGroup[] sectionParamsGroups;
        [SerializeField] private Vector2 sectionScale;
        [SerializeField] private float baseHeight = 0.01f;
        [SerializeField] private float rotationY;
        [SerializeField] private float startPositionZ = 0.05f;
        
        [SerializeField] private float straightEndPositionZ = -0.05f;
        
        [SerializeField] private int curveDivCount = 30;
        [SerializeField] private float curveRadius = 0.02f;

        [SerializeField] private int jumpDivCount = 20;
        [SerializeField] private float jumpStartPositionZ = 0.03f;
        [SerializeField] private float jumpStartDegree = 40.0f;
        [SerializeField] private float jumpRadius;
        [SerializeField] private float jumpBasePositionY;

        [SerializeField] private bool hasTrianglesEndSection;
        
        [ContextMenu("GenerateMesh")]
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


            var sectionCount = 1;
            
            foreach (var sectionParamsGroup in sectionParamsGroups)
            {
                var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                vertices.Add(new Vector3(vx, vy, startPositionZ));
                uvs.Add(Vector2.zero);
                normals.Add(new Vector3(nx, ny, 0.0f));
            }

            switch (meshMode)
            {
                case MeshMode.Straight:
                {
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        vertices.Add(new Vector3(vx, vy, straightEndPositionZ));
                        uvs.Add(Vector2.zero);
                        normals.Add(new Vector3(nx, ny, 0.0f));
                    }
                    sectionCount = 2;
                    break;
                }
                case MeshMode.Curve:
                {
                    for (var i = 0; i <= curveDivCount; i++)
                    {
                        var rot = i * 90.0f / curveDivCount;
                        var ox = curveRadius - Mathf.Cos(rot * Mathf.Deg2Rad) * curveRadius;
                        var oz = curveRadius - Mathf.Sin(rot * Mathf.Deg2Rad) * curveRadius;
                        foreach (var sectionParamsGroup in sectionParamsGroups)
                        {
                            var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                            var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                            var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                            var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                            var v = new Vector3(vx, vy, 0.0f);
                            var n = new Vector3(nx, ny, 0.0f);
                            v = Quaternion.Euler(new Vector3(0.0f, -rot, 0.0f)) * v;
                            v += new Vector3(ox, 0.0f, oz);
                            n = Quaternion.Euler(new Vector3(0.0f, -rot, 0.0f)) * n;
                            vertices.Add(v);
                            uvs.Add(Vector2.zero);
                            normals.Add(n);
                        }
                    }

                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var v = new Vector3(vx, vy, 0.0f);
                        var n = new Vector3(nx, ny, 0.0f);
                        v = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)) * v;
                        v.x = startPositionZ;
                        n = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)) * n;
                        vertices.Add(v);
                        uvs.Add(Vector2.zero);
                        normals.Add(n);
                    }

                    sectionCount = curveDivCount + 3;
                    break;
                }
                case MeshMode.Jump:
                {
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        vertices.Add(new Vector3(vx, vy, jumpStartPositionZ));
                        uvs.Add(Vector2.zero);
                        normals.Add(new Vector3(nx, ny, 0.0f));
                    }
                    for (var i = 0; i <= jumpDivCount; i++)
                    {
                        var rot = jumpStartDegree - i * jumpStartDegree * 2.0f / jumpDivCount;
                        var oy = Mathf.Cos(rot * Mathf.Deg2Rad) * jumpRadius + jumpBasePositionY;
                        var oz = Mathf.Sin(rot * Mathf.Deg2Rad) * jumpRadius;
                        foreach (var sectionParamsGroup in sectionParamsGroups)
                        {
                            var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                            var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                            var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                            var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                            var v = new Vector3(vx, vy, 0.0f);
                            var n = new Vector3(nx, ny, 0.0f);
                            v = Quaternion.Euler(new Vector3(rot,0.0f, 0.0f)) * v;
                            v += new Vector3(0.0f, oy, oz);
                            n = Quaternion.Euler(new Vector3(rot,0.0f, 0.0f)) * n;
                            vertices.Add(v);
                            uvs.Add(Vector2.zero);
                            normals.Add(n);
                        }
                    }
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        vertices.Add(new Vector3(vx, vy, -jumpStartPositionZ));
                        uvs.Add(Vector2.zero);
                        normals.Add(new Vector3(nx, ny, 0.0f));
                    }
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        vertices.Add(new Vector3(vx, vy, -startPositionZ));
                        uvs.Add(Vector2.zero);
                        normals.Add(new Vector3(nx, ny, 0.0f));
                    }

                    sectionCount = jumpDivCount + 5;
                    break;
                }
            }

            foreach (var sectionParamsGroup in sectionParamsGroups)
            {
                var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                var v = new Vector3(vx, vy, startPositionZ);
                var n = new Vector3(nx, ny, 0.0f);
                vertices.Add(v);
                uvs.Add(Vector2.zero);
                normals.Add(Vector3.forward);
            }

            switch (meshMode)
            {
                case MeshMode.Straight:
                {
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var v = new Vector3(vx, vy, straightEndPositionZ);
                        var n = new Vector3(nx, ny, 0.0f);
                        vertices.Add(v);
                        uvs.Add(Vector2.zero);
                        normals.Add(Vector3.back);
                    }

                    break;
                }
                case MeshMode.Curve:
                {
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var v = new Vector3(vx, vy, 0.0f);
                        var n = new Vector3(nx, ny, 0.0f);
                        v = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)) * v;
                        v.x = startPositionZ;
                        n = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)) * n;
                        vertices.Add(v);
                        uvs.Add(Vector2.zero);
                        normals.Add(Vector3.right);
                    }
                    break;
                }
                case MeshMode.Jump:
                {
                    foreach (var sectionParamsGroup in sectionParamsGroups)
                    {
                        var vx = sectionParamsGroup.baseVertex.x * sectionScale.x;
                        var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                        var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                        var v = new Vector3(vx, vy, -startPositionZ);
                        var n = new Vector3(nx, ny, 0.0f);
                        vertices.Add(v);
                        uvs.Add(Vector2.zero);
                        normals.Add(Vector3.back);
                    }

                    break;
                }
            }
            
            vertices.Add(new Vector3(0.0f, 0.0f, startPositionZ));
            uvs.Add(Vector2.zero);
            normals.Add(Vector3.forward);
            
            switch (meshMode)
            {
                case MeshMode.Straight:
                {
                    vertices.Add(new Vector3(0.0f, 0.0f, straightEndPositionZ));
                    uvs.Add(Vector2.zero);
                    normals.Add(Vector3.back);
                    break;
                }
                case MeshMode.Curve:
                {
                    vertices.Add(new Vector3(startPositionZ, 0.0f, 0.0f));
                    uvs.Add(Vector2.zero);
                    normals.Add(Vector3.right);
                    break;
                }
                case MeshMode.Jump:
                {
                    vertices.Add(new Vector3(0.0f, 0.0f, -startPositionZ));
                    uvs.Add(Vector2.zero);
                    normals.Add(Vector3.back);
                    break;
                }
            }
            for (var i = 0; i < vertices.Count; i++)
            {
                vertices[i] += new Vector3(0.0f, baseHeight, 0.0f);
                vertices[i] = Quaternion.Euler(new Vector3(0.0f, rotationY, 0.0f)) * vertices[i];
            }

            for (var i = 0; i < normals.Count; i++)
            {
                normals[i] = Quaternion.Euler(new Vector3(0.0f, rotationY, 0.0f)) * normals[i];
            }

            for (var i = 1; i < sectionParamsGroups.Length; i++)
            {
                if (sectionParamsGroups[i].baseVertex == sectionParamsGroups[i - 1].baseVertex)
                {
                    continue;
                }
                
                for (var j = 1; j < sectionCount; j++)
                {
                    var va = i + (j - 1) * sectionParamsGroups.Length;
                    var vb = i + j * sectionParamsGroups.Length;
                    var vc = i - 1 + (j - 1) * sectionParamsGroups.Length;
                    var vd = i - 1 + j * sectionParamsGroups.Length;

                    triangles.Add(va);
                    triangles.Add(vb);
                    triangles.Add(vc);
                    triangles.Add(vc);
                    triangles.Add(vb);
                    triangles.Add(vd);
                }
            }

            switch (meshMode)
            {
                case MeshMode.Jump:
                {
                    for (var i = 1; i <= jumpDivCount + 2; i++)
                    {
                        var va = (i + 1) * sectionParamsGroups.Length;
                        var vb = (i + 2) * sectionParamsGroups.Length - 1;
                        var vc = i * sectionParamsGroups.Length;
                        var vd = (i + 1) * sectionParamsGroups.Length - 1;

                        triangles.Add(va);
                        triangles.Add(vb);
                        triangles.Add(vc);
                        triangles.Add(vc);
                        triangles.Add(vb);
                        triangles.Add(vd);
                    }

                    break;
                }
            }

            for (var i = 1; i < sectionParamsGroups.Length; i++)
            {
                if (sectionParamsGroups[i].baseVertex == sectionParamsGroups[i - 1].baseVertex)
                {
                    continue;
                }
                var va = vertices.Count - sectionParamsGroups.Length * 2 + i - 2;
                var vb = vertices.Count - sectionParamsGroups.Length * 2 + i - 3;
                var vc = vertices.Count - 2;
                triangles.Add(va);
                triangles.Add(vb);
                triangles.Add(vc);
            }

            if (hasTrianglesEndSection)
            {
                for (var i = 1; i < sectionParamsGroups.Length; i++)
                {
                    if (sectionParamsGroups[i].baseVertex == sectionParamsGroups[i - 1].baseVertex)
                    {
                        continue;
                    }

                    var va = vertices.Count - sectionParamsGroups.Length + i - 3;
                    var vb = vertices.Count - sectionParamsGroups.Length + i - 2;
                    var vc = vertices.Count - 1;
                    triangles.Add(va);
                    triangles.Add(vb);
                    triangles.Add(vc);
                }
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.SetNormals(normals);
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilter.mesh = mesh;
        }


    }
}
