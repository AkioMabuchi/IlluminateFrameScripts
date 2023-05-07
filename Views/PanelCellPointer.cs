using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BoardCellPointer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private Material materialValid;
        [SerializeField] private Material materialInvalid;
        private void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private bool _isSelected;
        private bool _isValid;

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            
            EnableMeshRenderer();
        }

        public void SetValid(bool isValid)
        {
            _isValid = isValid;
            
            EnableMeshRenderer();
        }

        private void EnableMeshRenderer()
        {
            meshRenderer.enabled = _isSelected && _isValid;
        }

        private void ChangeMeshRendererMaterial()
        {
           //  meshRenderer.material = _isValid ? new Material(materialValid) : new Material(materialInvalid);
        }
    }
}
