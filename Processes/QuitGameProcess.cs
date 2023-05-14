// ReSharper disable once RedundantUsingDirective
using UnityEngine;

namespace Processes
{
    public class QuitGameProcess
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}