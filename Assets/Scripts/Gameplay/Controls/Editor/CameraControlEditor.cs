using UnityEditor;
using UnityEngine;

namespace Gameplay.Controls.Editor
{
    [CustomEditor(typeof(CameraControl))]
    public class CameraControlEditor : UnityEditor.Editor
    {

        private CameraControl _script;

        private void OnEnable()
        {
            _script = (CameraControl)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            if (GUILayout.Button("Reset Camera Size"))
            {
                _script.ResetCameraSize();
            }
        }
    }
}
