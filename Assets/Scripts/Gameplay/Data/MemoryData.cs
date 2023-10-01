using UnityEngine;

namespace Gameplay.Data
{
    [System.Serializable]
    public class MemoryData
    {
        public int RelatedID;
        public string DialogueKey;
        public string PostGameDialogueKey;
        public Color DialogueColor = Color.white;
        public Sprite DialogueImage;
    }
}
