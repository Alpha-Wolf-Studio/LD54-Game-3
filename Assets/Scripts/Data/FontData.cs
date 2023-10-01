using TMPro;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Font Data", menuName = "Font/Font Data", order = 1)]
    public class FontData : ScriptableObject
    {
        public TMP_FontAsset fontAsset;
        public Material normalMaterial;
        public Material highlightMaterial;
    }
}
