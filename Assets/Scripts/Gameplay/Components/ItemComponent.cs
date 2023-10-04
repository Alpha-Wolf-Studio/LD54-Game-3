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
using Data;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
    public class ItemComponent : MonoBehaviour, IInteractable, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
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
        
        [SerializeField] private GameObject worldPanel;
        public GameObject WorldPanel => worldPanel;

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
                int weightPostStore = BackpackControl.Instance.CurrentItemsWeight + itemWeight;
                bool canBeStored = weightPostStore <= BackpackControl.Instance.MaxItemsWeight;
                int weighOverflow = canBeStored ? 0 : weightPostStore - BackpackControl.Instance.MaxItemsWeight;
                
                StoreData storeWeightData = new StoreData()
                {
                    CanBeStored = canBeStored,
                    WeightOverflow = weighOverflow,
                    StoreAction = () => ChangeState(true),
                    CancelAction = () => DialogueControl.Instance.HideCurrentDialogue()
                };
                
                ItemDisplayCanvas.Get().SetCanvas(gameplayPanel, storeWeightData, this);
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
                worldPanel.SetActive(false);
                
                Color rendererColor = _itemRenderer.color;
                rendererColor.a = 1f;
                _itemRenderer.color = rendererColor;
            }
            
            DialogueControl.Instance.HideCurrentDialogue();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_stored)
                worldPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_stored)
                worldPanel.SetActive(false);
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

            serializedObject.FindProperty("worldPanel").objectReferenceValue = EditorGUILayout.ObjectField("World Canvas Panel",
                script.WorldPanel, typeof(GameObject), true) as GameObject;
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif