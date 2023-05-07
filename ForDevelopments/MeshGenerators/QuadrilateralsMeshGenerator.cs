using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    [Serializable]
    public struct Quadrilateral
    {
        public Vector3 vertexA;
        public Vector3 vertexB;
        public Vector3 vertexC;
        public Vector3 vertexD;

        public Vector2 uvA;
        public Vector2 uvB;
        public Vector2 uvC;
        public Vector2 uvD;
    }
    
    public class QuadrilateralsMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Quadrilateral[] quadrilaterals;
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
            
            foreach(var quadrilateral in quadrilaterals)
            {
                var vectorA = quadrilateral.vertexB - quadrilateral.vertexA;
                var vectorB = quadrilateral.vertexC - quadrilateral.vertexA;
                var normal = Vector3.Cross(vectorA, vectorB).normalized;
                
                vertices.Add(quadrilateral.vertexA);
                vertices.Add(quadrilateral.vertexB);
                vertices.Add(quadrilateral.vertexC);
                vertices.Add(quadrilateral.vertexD);

                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);
                
                uvs.Add( quadrilateral.uvA);
                uvs.Add( quadrilateral.uvB);
                uvs.Add( quadrilateral.uvC);
                uvs.Add( quadrilateral.uvD);
            }

            for (var i = 0; i < quadrilaterals.Length; i++)
            {
                triangles.Add(i * 4);
                triangles.Add(i * 4 + 1);
                triangles.Add(i * 4 + 2);
                triangles.Add(i * 4 + 2);
                triangles.Add(i * 4 + 1);
                triangles.Add(i * 4 + 3);
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
