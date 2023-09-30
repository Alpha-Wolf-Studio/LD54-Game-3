using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "Dialog Entry", menuName = "Dialog/Dialog Entry", order = 1)]
    public class DialogEntry : ScriptableObject
    {
        public string dialogKey;
        public Color dialogColor = Color.white;
        public Sprite dialogImage;
    }
}
