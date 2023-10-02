using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Components
{
    [RequireComponent(typeof(IInteractable))]
    public class InteractableAnimationController : MonoBehaviour
    {

        [SerializeField] private Animator animator;
        
        private IInteractable _interactable;
        private readonly int _interactTrigger = Animator.StringToHash("Interact");
        
        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
            _interactable.OnInteract += OnInteract;
        }

        private void OnDestroy()
        {
            _interactable.OnInteract -= OnInteract;
        }

        private void OnInteract() => animator.SetTrigger(_interactTrigger);
    }
}
