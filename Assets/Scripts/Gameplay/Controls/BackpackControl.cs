using System;
using System.Collections.Generic;
using Audio;
using Gameplay.Components;
using UnityEngine;

namespace Gameplay.Controls
{
    public class BackpackControl : MonoBehaviour
    {
        [SerializeField] private int maxItemsWeight;
        [SerializeField] private AudioClip storeItemClip;
        
        public static BackpackControl Instance { get; private set; }

        public event Action<ItemComponent> OnItemAdded;
        public event Action<ItemComponent> OnItemRemoved;
        public event Action OnEnabled;
        public event Action OnDisabled;
        
        public int CurrentItemsWeight => _currentItemsWeight;
        public int MaxItemsWeight => maxItemsWeight;

        private readonly List<ItemComponent> _itemsInBackpack = new List<ItemComponent>();
        public List<ItemComponent> ItemsInBackpack => _itemsInBackpack;
        private int _currentItemsWeight = 0;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void EnableControl() => OnEnabled?.Invoke();
        public void DisableControl() => OnDisabled?.Invoke();
        
        public void AddItem(ItemComponent itemComponent)
        {
            if (_currentItemsWeight + itemComponent.ItemWeight > maxItemsWeight) return;
            
            _currentItemsWeight += itemComponent.ItemWeight;
            
            _itemsInBackpack.Add(itemComponent);
            OnItemAdded?.Invoke(itemComponent);
            
            AudioManager.Instance.PlaySfx(storeItemClip);
        }
        
        public void RemoveItem(ItemComponent itemComponent)
        {
            if (!_itemsInBackpack.Contains(itemComponent)) return;
            
            _currentItemsWeight -= itemComponent.ItemWeight;
            _itemsInBackpack.Remove(itemComponent);
            OnItemRemoved?.Invoke(itemComponent);
        }
    }
}
