using UnityEngine;

namespace Gameplay.Components
{
    public class ItemImageSwapComponent : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        private SpriteRenderer _spriteToSwap;
        public int CurrentIndex { get; private set; }

        private void Awake()
        {
            _spriteToSwap = GetComponent<SpriteRenderer>();
        }

        public void Swap(int index)
        {
            _spriteToSwap.sprite = sprites[index];
            CurrentIndex = index;
        }
    }
}
