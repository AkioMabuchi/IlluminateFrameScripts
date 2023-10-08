using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Views.Instances
{
    [RequireComponent(typeof(MeshFilter))]
    public class DeskBody : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        [SerializeField] private Mesh meshSmall;
        [SerializeField] private Mesh meshMedium;
        [SerializeField] private Mesh meshLarge;

        private void Reset()
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        public void ChangeMesh(FrameSize frameSize)
        {
            switch (frameSize)
            {
                case FrameSize.Small:
                {
                    meshFilter.mesh = meshSmall;
                    break;
                }
                case FrameSize.Medium:
                {
                    meshFilter.mesh = meshMedium;
                    break;
                }
                case FrameSize.Large:
                {
                    meshFilter.mesh = meshLarge;
                    break;
                }
            }
        }
    }
}