using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Controls
{
    public class DialogControl : MonoBehaviour
    {
        [SerializeField] private float dialogSpeed;
        public static DialogControl Instance { get; private set; }
    
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void AddDialog(DialogEntry dialogEntry)
        {
            
        }
    }
}
