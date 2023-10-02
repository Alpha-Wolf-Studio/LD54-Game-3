using TMPro;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UI Data", menuName = "UI/UI Data", order = 1)]
    public class UIData : ScriptableObject
    {
        [Header("Text Data")]
        public TMP_FontAsset fontAsset;
        public Material normalMaterial;
        public Material highlightMaterial;

        [Header("Audio Data")] 
        public AudioClip highlightAudio;
        public AudioClip selectAudio;
    }
}
