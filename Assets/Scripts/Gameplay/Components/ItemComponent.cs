using System;
using UnityEngine;
using Gameplay.Interfaces;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemComponent : MonoBehaviour, IInteractable
    {
        [SerializeField] private int itemWeight;
        public int ItemWeight => itemWeight;
        
        public event Action OnInteract;
        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
}
