using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Controls
{
    public class InteractionControl : MonoBehaviourSingleton<InteractionControl>
    {
        
        private Camera _camera;
        private readonly Collider2D[] _collider2Ds = new Collider2D[10];
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            Vector3 origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            origin.z = 0;

            int size = Physics2D.OverlapPointNonAlloc(origin, _collider2Ds);

            if (size <= 0) return;
            
            for (int i = 0; i < size; i++)
            {
                if (_collider2Ds[i].TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                    return;
                }
            }
        }
    }
}
