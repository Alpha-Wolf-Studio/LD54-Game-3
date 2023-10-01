using System;
using UnityEngine;
using UnityEditor;
using Gameplay.Interfaces;
using Gameplay.Components;
using UnityEngine.EventSystems;
using Gameplay.Controls;
using Gameplay.Data;
using Gameplay.UI;
using Audio;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
    public class ItemComponent : MonoBehaviour, IInteractable, IPointerDownHandler
    {
        #region Variables
        [SerializeField] private int id;
        public int ID => id;

        [SerializeField] private int itemWeight;
        public int ItemWeight => itemWeight;

        [SerializeField] private AudioClip itemSelectedClip;
        public AudioClip ItemSelectedClip => itemSelectedClip;

        [SerializeField] private AudioClip itemReturnClip;
        public AudioClip ItemReturnClip => itemReturnClip;

        [SerializeField] private MemoryEntry itemTextData;
        public MemoryEntry ItemTextKey => itemTextData;

        [SerializeField] private ItemDisplayPanel gameplayPanel;
        public ItemDisplayPanel GameplayPanel => gameplayPanel;

        private bool _stored;
        private SpriteRenderer _itemRenderer;
        
        #endregion
        
        public event Action OnInteract;

        private void Awake()
        {
            _itemRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact()
        {
            OnInteract?.Invoke();

            if (!_stored && itemSelectedClip)
                AudioManager.Instance.PlaySfx(itemSelectedClip);
            else if (_stored && itemReturnClip)
                AudioManager.Instance.PlaySfx(ItemReturnClip);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Interact();

            if (!_stored)
            {
                ItemDisplayCanvas.Get().SetCanvas(gameplayPanel, () => ChangeState(true), () => DialogueControl.Instance.HideCurrentDialogue());
                DialogueControl.Instance.ShowDialogue(itemTextData.Data.DialogueKey);
            }
            else
            {
                ChangeState(false);
            }
        }

        private void ChangeState(bool stored)
        {
            _stored = stored;

            if (_stored)
            {
                BackpackControl.Instance.AddItem(this);
                
                Color rendererColor = _itemRenderer.color;
                rendererColor.a = .2f;
                _itemRenderer.color = rendererColor;
            }
            else
            {
                BackpackControl.Instance.RemoveItem(this);
                
                Color rendererColor = _itemRenderer.color;
                rendererColor.a = 1f;
                _itemRenderer.color = rendererColor;
            }
            
            DialogueControl.Instance.HideCurrentDialogue();
        }
    }
}

#if UNITY_EDITOR

namespace Gameplay.Editors
{
    [CustomEditor(typeof(ItemComponent))]
    public class ItemComponentEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (ItemComponent)target;

            serializedObject.FindProperty("id").intValue = EditorGUILayout.DelayedIntField("Item ID", script.ID);

            serializedObject.FindProperty("itemWeight").intValue = EditorGUILayout.DelayedIntField("Item Weight", 
                script.ItemWeight);

            serializedObject.FindProperty("itemSelectedClip").objectReferenceValue = EditorGUILayout.ObjectField("Item Select Clip",
                script.ItemSelectedClip, typeof(AudioClip), true) as AudioClip;

            serializedObject.FindProperty("itemReturnClip").objectReferenceValue = EditorGUILayout.ObjectField("Item Return Clip",
                script.ItemReturnClip, typeof(AudioClip), true) as AudioClip;

            serializedObject.FindProperty("gameplayPanel").objectReferenceValue = EditorGUILayout.ObjectField("Item Panel"
                , script.GameplayPanel, typeof(ItemDisplayPanel), true) as ItemDisplayPanel;

            serializedObject.FindProperty("itemTextData").objectReferenceValue = EditorGUILayout.ObjectField("Item Text",
            script.ItemTextKey, typeof(MemoryEntry), true) as MemoryEntry;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif