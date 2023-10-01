using System;
using UnityEngine;
using UnityEditor;
using Gameplay.Interfaces;
using Gameplay.Components;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Runtime.Serialization;
using Gameplay.Controls;
using Gameplay.Data;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemComponent : MonoBehaviour, IInteractable, IPointerDownHandler
    {
        #region Variables
        [SerializeField] private int id;
        public int ID => id;

        [SerializeField] private int itemWeight;
        public int ItemWeight => itemWeight;

        [SerializeField] private AudioClip itemSelectedClip;
        public AudioClip ItemSelectedClip => itemSelectedClip;

        [SerializeField] private MemoryEntry itemTextData;
        public MemoryEntry ItemTextKey => itemTextData;

        [SerializeField] private GameObject gameplayPanel;
        public GameObject GameplayPanel => gameplayPanel;
        #endregion
        
        public event Action OnInteract;

        public void Interact()
        {
            OnInteract?.Invoke();           
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Interact();
            ItemDisplayCanvas.Get().OpenCanvas(gameplayPanel);
            DialogueControl.Instance.ShowDialogue(itemTextData.Data.DialogueKey);
        }
    }
}

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

            serializedObject.FindProperty("itemSelectedClip").objectReferenceValue = EditorGUILayout.ObjectField("Item Clip",
                script.ItemSelectedClip, typeof(AudioClip), true) as AudioClip;

            serializedObject.FindProperty("gameplayPanel").objectReferenceValue = EditorGUILayout.ObjectField("Item Panel"
                , script.GameplayPanel, typeof(GameObject), true) as GameObject;

            serializedObject.FindProperty("itemTextData").objectReferenceValue = EditorGUILayout.ObjectField("Item Text",
            script.ItemTextKey, typeof(MemoryEntry), true) as MemoryEntry;

            serializedObject.ApplyModifiedProperties();
        }
    }
}