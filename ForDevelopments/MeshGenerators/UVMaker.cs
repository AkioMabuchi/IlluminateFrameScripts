using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class UVMaker: MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilterInput;
        [SerializeField] private MeshFilter meshFilterOutput;
        
        
        [ContextMenu("Generate Mesh")]
        private void GenerateMesh()
        {
            var generatedGameObject = new GameObject();
            var meshFilter = generatedGameObject.AddComponent<MeshFilter>();
            generatedGameObject.AddComponent<MeshRenderer>();
            
            if (meshFilterInput == null || meshFilterOutput == null)
            {
                return;
            }

            var mesh = meshFilterInput.sharedMesh;
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;
            var normals = mesh.normals;
            var uvs = mesh.uv;

            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minZ = float.MaxValue;
            var maxZ = float.MinValue;

            foreach (var vertex in vertices)
            {
                minX = Mathf.Min(vertex.x, minX);
                maxX = Mathf.Max(vertex.x, maxX);
                minZ = Mathf.Min(vertex.z, minZ);
                maxZ = Mathf.Max(vertex.z, maxZ);
            }

            for (var i = 0; i < uvs.Length && i < vertices.Length; i++)
            {
                var u = Mathf.InverseLerp(minX, maxX, vertices[i].x);
                var v = Mathf.InverseLerp(minZ, maxZ, vertices[i].z);
                uvs[i] = new Vector2(u, v);
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            meshFilterOutput.mesh = mesh;
        }
    }
}