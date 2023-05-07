using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileNameMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            if (meshFilter == null)
            {
                return;
            }
            

            const float halfWidth = 0.05f;
            const float height = 0.01f;

            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var trianglesA = new List<int>();
            var trianglesB = new List<int>();
            var trianglesC = new List<int>();
            var uvs = new List<Vector2>();

            mesh.subMeshCount = 3;
            
            vertices.Add(new Vector3(-halfWidth, height, halfWidth));
            vertices.Add(new Vector3(halfWidth, height, halfWidth));
            vertices.Add(new Vector3(-halfWidth, height, -halfWidth));
            vertices.Add(new Vector3(halfWidth, height, -halfWidth));
            
            vertices.Add(new Vector3(-halfWidth, height, -halfWidth));
            vertices.Add(new Vector3(halfWidth, height, -halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, -halfWidth));
            vertices.Add(new Vector3(halfWidth, 0.0f, -halfWidth));
            
            vertices.Add(new Vector3(halfWidth, height, -halfWidth));
            vertices.Add(new Vector3(halfWidth, height, halfWidth));
            vertices.Add(new Vector3(halfWidth, 0.0f, -halfWidth));
            vertices.Add(new Vector3(halfWidth, 0.0f, halfWidth));
            
            vertices.Add(new Vector3(halfWidth, height, halfWidth));
            vertices.Add(new Vector3(-halfWidth, height, halfWidth));
            vertices.Add(new Vector3(halfWidth, 0.0f, halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, halfWidth));
            
            vertices.Add(new Vector3(-halfWidth, height, halfWidth));
            vertices.Add(new Vector3(-halfWidth, height, -halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, -halfWidth));
            
            vertices.Add(new Vector3(halfWidth, 0.0f, halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, halfWidth));
            vertices.Add(new Vector3(halfWidth, 0.0f, -halfWidth));
            vertices.Add(new Vector3(-halfWidth, 0.0f, -halfWidth));

            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            uvs.Add(new Vector2(0.0f, 1.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            
            trianglesA.Add(0);
            trianglesA.Add(1);
            trianglesA.Add(2);
            trianglesA.Add(2);
            trianglesA.Add(1);
            trianglesA.Add(3);
            
            trianglesB.Add(4);
            trianglesB.Add(5);
            trianglesB.Add(6);
            trianglesB.Add(6);
            trianglesB.Add(5);
            trianglesB.Add(7);
            
            trianglesB.Add(8);
            trianglesB.Add(9);
            trianglesB.Add(10);
            trianglesB.Add(10);
            trianglesB.Add(9);
            trianglesB.Add(11);
            
            trianglesB.Add(12);
            trianglesB.Add(13);
            trianglesB.Add(14);
            trianglesB.Add(14);
            trianglesB.Add(13);
            trianglesB.Add(15);
            
            trianglesB.Add(16);
            trianglesB.Add(17);
            trianglesB.Add(18);
            trianglesB.Add(18);
            trianglesB.Add(17);
            trianglesB.Add(19);
            
            trianglesC.Add(20);
            trianglesC.Add(21);
            trianglesC.Add(22);
            trianglesC.Add(22);
            trianglesC.Add(21);
            trianglesC.Add(23);
                
            mesh.SetVertices(vertices);
            mesh.SetTriangles(trianglesA, 0);
            mesh.SetTriangles(trianglesB, 1);
            mesh.SetTriangles(trianglesC, 2);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();


            meshFilter.sharedMesh = mesh;
        }
    }
}
