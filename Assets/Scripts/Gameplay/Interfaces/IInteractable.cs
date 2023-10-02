using System;

namespace Gameplay.Interfaces
{
    public interface IInteractable
    {
        public event Action OnInteract; 
        void Interact();
    }
}
