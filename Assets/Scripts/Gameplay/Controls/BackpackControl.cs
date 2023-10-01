using System;
using System.Collections.Generic;
using Gameplay.Components;
using UnityEngine;

namespace Gameplay.Controls
{
    public class BackpackControl : MonoBehaviour
    {
        [SerializeField] private int maxItemsWeight;
        
        public static BackpackControl Instance { get; private set; }

        public event Action<ItemComponent> OnItemAdded;
        public event Action<ItemComponent> OnItemRemoved;

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

        public void AddItem(ItemComponent itemComponent)
        {
            if (_currentItemsWeight + itemComponent.ItemWeight > maxItemsWeight) return;
            
            _currentItemsWeight += itemComponent.ItemWeight;
            
            _itemsInBackpack.Add(itemComponent);
            OnItemAdded?.Invoke(itemComponent);
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
