using System.Collections.Generic;
using UnityEngine;

namespace ForDevelopments.MeshGenerators
{
    public class TileDistributorCoreGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private SectionParamsGroup[] sectionParamsGroups;
        [SerializeField] private RadialSectionParamsGroup[] radialSectionParamsGroups;
        [SerializeField] private Vector2 sectionScale;
        [SerializeField] private float baseHeight = 0.01f;

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

            foreach (var radialSectionParamsGroup in radialSectionParamsGroups)
            {
                var magnitude = radialSectionParamsGroup.position.magnitude;
                var deg = Mathf.Atan2(radialSectionParamsGroup.position.y, radialSectionParamsGroup.position.x) *
                          Mathf.Rad2Deg;

                foreach (var sectionParamsGroup in sectionParamsGroups)
                {
                    var vx = sectionParamsGroup.baseVertex.x * sectionScale.x * magnitude;
                    var vy = sectionParamsGroup.baseVertex.y * sectionScale.y;
                    var nx = Mathf.Cos(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                    var ny = Mathf.Sin(sectionParamsGroup.baseNormalDegree * Mathf.Deg2Rad);
                    var v = new Vector3(vx, vy, 0.0f);
                    var n = new Vector3(nx, ny, 0.0f);
                    v = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f)) * v;
                    n = Quaternion.Euler(new Vector3(0.0f, radialSectionParamsGroup.normalDegree, 0.0f)) * n;
                    vertices.Add(v);
                    normals.Add(n);
                    uvs.Add(Vector2.zero);
                }
            }
            
            for (var i = 0; i < vertices.Count; i++)
            {
                vertices[i] += new Vector3(0.0f, baseHeight, 0.0f);
            }

            for (var s = 1; s < sectionParamsGroups.Length; s++)
            {
                if (sectionParamsGroups[s].baseVertex == sectionParamsGroups[s - 1].baseVertex)
                {
                    continue;
                }

                for (var r = 0; r < radialSectionParamsGroups.Length; r++)
                {
                    var va = s + (r == 0 ? radialSectionParamsGroups.Length - 1 : r - 1) * sectionParamsGroups.Length;
                    var vb = s + r * sectionParamsGroups.Length;
                    var vc = s - 1 + (r == 0 ? radialSectionParamsGroups.Length - 1 : r - 1) *
                        sectionParamsGroups.Length;
                    var vd = s - 1 + r * sectionParamsGroups.Length;

                    triangles.Add(va);
                    triangles.Add(vb);
                    triangles.Add(vc);
                    triangles.Add(vc);
                    triangles.Add(vb);
                    triangles.Add(vd);
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
