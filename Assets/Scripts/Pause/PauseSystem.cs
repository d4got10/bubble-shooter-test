using System.Linq;
using UnityEngine;

namespace BubbleShooter
{
    public class PauseSystem : MonoBehaviour
    {
        [ContextMenu("Pause")]
        public void Pause()
        {
            Object[] listeners = GetListeners();
            foreach (var listener in listeners)
                ((IPauseListener)listener).Pause();
        }

        [ContextMenu("Unpause")]
        public void Unpause()
        {
            Object[] listeners = GetListeners();
            foreach (var listener in listeners)
                ((IPauseListener)listener).Unpause();
        }

        private Object[] GetListeners()
        {
            return FindObjectsOfType<Object>().Where(t => t is IPauseListener).ToArray();
        }
    }
}