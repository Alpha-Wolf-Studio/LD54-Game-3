using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "Dialog Entry", menuName = "Dialog/Dialog Entry", order = 1)]
    public class DialogueEntry : ScriptableObject
    {
        public DialogueData Data;
    }
}
