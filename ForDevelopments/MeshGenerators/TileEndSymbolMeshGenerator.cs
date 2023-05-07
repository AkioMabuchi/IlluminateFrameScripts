using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileEndSymbolMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Vector2 scale = Vector2.one;
        [SerializeField] private Vector2 offsetCurve = Vector2.zero;
    
        [SerializeField] private float thickness;
        [SerializeField] private float curveStartDegree;
        [SerializeField] private float curveEndDegree;
        [SerializeField] private int curveDivCount;
        [SerializeField] private float normalDegree;
        [SerializeField] private float startHeight;
        [SerializeField] private float endHeight;
        [SerializeField] private float lineStart;
        [SerializeField] private float lineEnd;

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

            var curvePoints = new List<Vector2>();

            for (var i = 0; i <= curveDivCount; i++)
            {
                var rad = Mathf.Lerp(curveEndDegree, curveStartDegree, (float) i / curveDivCount) * Mathf.Deg2Rad;

                var px = Mathf.Cos(rad) * (1 + thickness);
                var py = Mathf.Sin(rad) * (1 + thickness);

                curvePoints.Add(new Vector2(px, py));
            }
        
            for (var i = 0; i <= curveDivCount; i++)
            {
                var rad = Mathf.Lerp(curveStartDegree, curveEndDegree, (float) i / curveDivCount) * Mathf.Deg2Rad;

                var px = Mathf.Cos(rad) * (1 - thickness);
                var py = Mathf.Sin(rad) * (1 - thickness);

                curvePoints.Add(new Vector2(px, py));
            }
        
            curvePoints.Add(curvePoints[0]);
        
            var curveTriangleIndexes = new List<Vector3Int>();
        
            for (var i = 0; i <= curveDivCount; i++)
            {
                curveTriangleIndexes.Add(new Vector3Int(i, i + 1, curveDivCount * 2 - i));
                curveTriangleIndexes.Add(new Vector3Int(i, curveDivCount * 2 - i, curveDivCount * 2 - i + 1));
            }
        

            foreach (var point in curvePoints)
            {
                vertices.Add(new Vector3(point.x * scale.x, startHeight, point.y * scale.y));
                normals.Add(Vector3.up);
                uvs.Add(Vector2.zero);
            }

            for (var i = 1; i < curvePoints.Count; i++)
            {
                if (curvePoints[i] == curvePoints[i - 1])
                {
                    continue;
                }

                var deg = Mathf.Atan2(curvePoints[i].y - curvePoints[i - 1].y, curvePoints[i].x - curvePoints[i - 1].x) * Mathf.Rad2Deg;

                var n = Quaternion.Euler(new Vector3(0.0f, -deg, 0.0f)) *
                        Quaternion.Euler(new Vector3(0.0f, 0.0f, normalDegree)) * Vector3.right;

                var pa = new Vector2(curvePoints[i - 1].x * scale.x, curvePoints[i - 1].y * scale.y);
                var pb = new Vector2(curvePoints[i].x * scale.x, curvePoints[i].y * scale.y);

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
                vertices[i] += new Vector3(offsetCurve.x, 0.0f, offsetCurve.y);
            }
        
            foreach (var triangleIndex in curveTriangleIndexes)
            {
                triangles.Add(triangleIndex.x);
                triangles.Add(triangleIndex.y);
                triangles.Add(triangleIndex.z);
            }
        
            var lineStartVerticesCount = vertices.Count;
            var linePoints = new List<Vector2>
            {
                new(-thickness, lineStart),
                new(thickness, lineStart),
                new(thickness, lineEnd),
                new(-thickness, lineEnd),
                new(-thickness, lineStart)
            };

            var lineTriangleIndexes = new List<Vector3Int>
            {
                new(0, 1, 3),
                new(1, 2, 3)
            };
        
            foreach (var point in linePoints)
            {
                vertices.Add(new Vector3(point.x * scale.x, startHeight, point.y * scale.y));
                normals.Add(Vector3.up);
                uvs.Add(Vector2.zero);
            }
        
            for (var i = 1; i < linePoints.Count; i++)
            {
                if (linePoints[i] == linePoints[i - 1])
                {
                    continue;
                }

                var deg = Mathf.Atan2(linePoints[i].y - linePoints[i - 1].y, linePoints[i].x - linePoints[i - 1].x) * Mathf.Rad2Deg;

                var n = Quaternion.Euler(new Vector3(0.0f, -deg, 0.0f)) *
                        Quaternion.Euler(new Vector3(0.0f, 0.0f, normalDegree)) * Vector3.right;

                var pa = new Vector2(linePoints[i - 1].x * scale.x, linePoints[i - 1].y * scale.y);
                var pb = new Vector2(linePoints[i].x * scale.x, linePoints[i].y * scale.y);

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

            foreach (var triangleIndex in lineTriangleIndexes)
            {
                triangles.Add(triangleIndex.x + lineStartVerticesCount);
                triangles.Add(triangleIndex.y + lineStartVerticesCount);
                triangles.Add(triangleIndex.z + lineStartVerticesCount);
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
