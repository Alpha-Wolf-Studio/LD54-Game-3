#if UNITY_EDITOR

using System;
using Gameplay.Data;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Controls.Editor
{
    [CustomEditor(typeof(DialogueControl))]
    public class DialogueControlEditor : UnityEditor.Editor
    {
        private DialogueControl _script;
        
        private void OnEnable()
        {
            _script = (DialogueControl)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            if (GUILayout.Button("Open Test Dialogue"))
            {
                MemoryData data = new MemoryData()
                {
                    DialogueColor = Random.ColorHSV(),
                    DialogueKey = "Este item lo recibi de tu mama la noche que te hice una hermana."
                };
                _script.ShowDialogue(data.DialogueKey);
            }
            
            if (GUILayout.Button("Close Test Dialogue"))
            {
                _script.HideCurrentDialogue();
            }
        }
    }
}

#endif

