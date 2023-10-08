using System.IO;
using UnityEngine;

namespace ForDevelopments
{
    public class RadiantWhiteImageGenerator : MonoBehaviour
    {
        [SerializeField] private Texture2D baseTexture;

        [ContextMenu("GenerateImage")]
        public void GenerateImage()
        {
            if (baseTexture == null)
            {
                return;
            }

            var texture = new Texture2D(baseTexture.width, baseTexture.height, TextureFormat.RGBA32, true);
            var colors = baseTexture.GetPixels();
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color(1.0f, 1.0f, 1.0f, colors[i].r);
            }

            texture.SetPixels(colors);

            var png = texture.EncodeToPNG();
            File.WriteAllBytes("Assets/WhiteImage.png", png);

            Debug.Log("GENERATED");
        }
    }
}