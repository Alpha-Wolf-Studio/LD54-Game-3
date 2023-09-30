using System;
using UnityEngine;
using UnityEditor;
using Gameplay.Interfaces;
using Gameplay.Components;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemComponent : MonoBehaviour, IInteractable, IPointerDownHandler
    {
        #region Variables
        [SerializeField] private int id;
        public int ID => id;
        public void SetID(int _id) { id= _id; }

        [SerializeField] private string itemNameKey;
        public string ItemNameKey => itemNameKey;
        public void SetItemNameKey(string itemName) { itemNameKey = itemName; }

        [SerializeField] private int itemWeight;
        public int ItemWeight => itemWeight;
        public void SetItemWeight(int weight) { itemWeight = weight; }

        [SerializeField] private AudioClip itemSelectedClip;
        public AudioClip ItemSelectedClip => itemSelectedClip;
        public void SetItemAudioClip(AudioClip clip) { itemSelectedClip = clip; }

        [SerializeField] private bool isMemory;
        public bool IsMemory => isMemory;
        public void SetItemMemory(bool memory) { isMemory = memory; }

        [SerializeField] private string itemTextKey;
        public string ItemTextKey => itemTextKey;
        public void SetItemTextKey(string itemText) { itemTextKey = itemText; }

        [SerializeField] private Sprite itemImage;
        public Sprite ItemImage => itemImage;
        public void SetItemImage(Sprite sprite) { itemImage = sprite; }

        [SerializeField] private GameObject gameplayPanel;
        public GameObject GameplayPanel => gameplayPanel;
        public void SetPanel(GameObject panel) { gameplayPanel = panel; }
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

            script.SetID(EditorGUILayout.DelayedIntField("Item ID", script.ID));

            script.SetItemNameKey(EditorGUILayout.DelayedTextField("Item Name Key", script.ItemNameKey));
            script.SetItemWeight(EditorGUILayout.DelayedIntField("Item Weight", script.ItemWeight));
            script.SetItemAudioClip(EditorGUILayout.ObjectField("Item Clip", script.ItemSelectedClip, typeof(AudioClip), 
                true) as AudioClip);

            script.SetPanel(EditorGUILayout.ObjectField("Item Image", script.GameplayPanel, typeof(GameObject), true) as GameObject);

            script.SetItemMemory(EditorGUILayout.Toggle("Is Item Memory", script.IsMemory));

            if (!script.IsMemory)
                return;

            script.SetItemTextKey(EditorGUILayout.DelayedTextField("Item Text Key", script.ItemTextKey));
            script.SetItemImage(EditorGUILayout.ObjectField("Item Image", script.ItemImage, typeof(Sprite), true) as Sprite);

            serializedObject.ApplyModifiedProperties();
        }
    }
}