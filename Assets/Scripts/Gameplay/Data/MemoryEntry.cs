using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "Memory Entry", menuName = "Memory/Memory Entry", order = 1)]
    public class MemoryEntry : ScriptableObject
    {
        public MemoryData Data;
    }
}
