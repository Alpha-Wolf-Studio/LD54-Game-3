#if UNITY_EDITOR

using System;
using Gameplay.Data;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Controls.Editor
{
    [CustomEditor(typeof(DialogControl))]
    public class DialogControlEditor : UnityEditor.Editor
    {
        private DialogControl _script;
        
        private void OnEnable()
        {
            _script = (DialogControl)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            if (GUILayout.Button("Open Test Dialog"))
            {
                DialogData data = new DialogData()
                {
                    DialogColor = Random.ColorHSV(),
                    DialogKey = "Este item lo recibi de tu mama la noche que te hice una hermana."
                };
                _script.ShowDialog(data);
            }
            
            if (GUILayout.Button("Close Test Dialog"))
            {
                _script.HideCurrentDialog();
            }
        }
    }
}

#endif

