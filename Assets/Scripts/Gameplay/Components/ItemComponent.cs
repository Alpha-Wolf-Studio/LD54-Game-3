using System;
using UnityEngine;
using UnityEditor;
using Gameplay.Interfaces;
using Gameplay.Components;

namespace Gameplay.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemComponent : MonoBehaviour, IInteractable
    {
        #region Variables
        private string itemNameKey;
        public string ItemNameKey => itemNameKey;
        public void SetItemNameKey(string itemName)
        {
            itemNameKey = itemName;
        }

        private int itemWeight;
        public int ItemWeight => itemWeight;
        public void SetItemWeight(int weight)
        {
            itemWeight = weight;
        }

        private AudioClip itemSelectedClip;
        public AudioClip ItemSelectedClip => itemSelectedClip;
        public void SetItemAudioClip(AudioClip clip)
        {
            itemSelectedClip = clip;
        }

        private bool isMemory;
        public bool IsMemory => isMemory;
        public void SetItemMemory(bool memory)
        {
            isMemory = memory;
        }

        private string itemTextKey;
        public string ItemTextKey => itemTextKey;
        public void SetItemTextKey(string itemText)
        {
            itemTextKey = itemText;
        }

        private Sprite itemImage;
        public Sprite ItemImage => itemImage;
        public void SetItemImage(Sprite sprite)
        {
            itemImage = sprite;
        }
        #endregion

        public event Action OnInteract;
        public void Interact()
        {
            OnInteract?.Invoke();
            ItemDisplayCanvas.Get().OpenCanvas();
            ItemDisplayCanvas.Get().PopulateData(Loc.ReplaceKey(ItemNameKey), ItemImage);
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

            script.SetItemNameKey(EditorGUILayout.DelayedTextField("Item Name Key", script.ItemNameKey));
            script.SetItemWeight(EditorGUILayout.DelayedIntField("Item Weight", script.ItemWeight));
            script.SetItemAudioClip(EditorGUILayout.ObjectField("Item Clip", script.ItemSelectedClip, typeof(AudioClip), 
                true) as AudioClip);

            script.SetItemMemory(EditorGUILayout.Toggle("Is Item Memory", script.IsMemory));

            if (!script.IsMemory)
                return;

            script.SetItemTextKey(EditorGUILayout.DelayedTextField("Item Text Key", script.ItemTextKey));
            script.SetItemImage(EditorGUILayout.ObjectField("Item Image", script.ItemImage, typeof(Sprite), true) as Sprite);
        }
    }
}