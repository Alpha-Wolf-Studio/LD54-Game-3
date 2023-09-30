using System.Collections;
using UnityEngine;

namespace Gameplay.Controls
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Camera currentCamera;
        
        [Header("Camera Configurations")]
        [SerializeField] private float minCameraSize = 2;
        [SerializeField] private float maxCameraSize = 5;
        [SerializeField] private float cameraResetSpeed = 2;
        
        private float _currentCameraSize;
        private bool _locked;

        private void Start()
        {
            _currentCameraSize = maxCameraSize;
            SetCameraZoom(maxCameraSize);
        }

        private void Update()
        {
            if (_locked) return;
            
            float wheelAxis = Input.GetAxis("Mouse ScrollWheel");
            
            if (Mathf.Abs(wheelAxis) > 0)
            {
                _currentCameraSize = Mathf.Clamp(_currentCameraSize - wheelAxis, minCameraSize, maxCameraSize);
                SetCameraZoom(_currentCameraSize);
            }
        }
        
        public void ResetCameraSize()
        {
            StartCoroutine(ResettingCamera());
        }
        
        private void SetCameraZoom(float zoom) => currentCamera.orthographicSize = zoom;

        private IEnumerator ResettingCamera()
        {
            _locked = true;

            float startCameraSize = _currentCameraSize;
            
            float t = 0;
            while (t < 1)
            {
                _currentCameraSize = Mathf.Lerp(startCameraSize, maxCameraSize, t);
                SetCameraZoom(_currentCameraSize);
                t += Time.deltaTime * cameraResetSpeed;
                yield return null;
            }
            
            _currentCameraSize = maxCameraSize;
            SetCameraZoom(maxCameraSize);
            
            _locked = false;
        }

    }
}
