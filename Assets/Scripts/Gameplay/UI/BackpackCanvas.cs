using System.Collections;
using Gameplay.Components;
using Gameplay.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class BackpackCanvas : MonoBehaviour
    {
        [SerializeField] private BackpackControl backpackControl;
        
        [Header("Box Amount UI")]
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private Image boxOverlay;
        [SerializeField] private Image boxFill;
        [SerializeField] private TextMeshProUGUI boxFillText;
        [SerializeField] private float boxFillChangeSpeed;

        private IEnumerator _changeBoxCapacityCoroutine;
        
        private void Awake()
        {
            backpackControl.OnItemAdded += ItemAdded;
            backpackControl.OnItemRemoved += ItemRemoved;
        }

        private void OnDestroy()
        {
            backpackControl.OnItemAdded -= ItemAdded;
            backpackControl.OnItemRemoved -= ItemRemoved;
        }

        private void ItemAdded(ItemComponent _)
        {
            float fill = GetBackpackCapacity();
            ChangeBoxFill(fill);
        }

        private void ItemRemoved(ItemComponent _)
        {
            float fill = GetBackpackCapacity();
            ChangeBoxFill(fill);
        }

        private void ChangeBoxFill(float fill)
        {
            if(_changeBoxCapacityCoroutine != null)
                StopCoroutine(_changeBoxCapacityCoroutine);

            _changeBoxCapacityCoroutine = SettingBoxFill(fill);
            StartCoroutine(_changeBoxCapacityCoroutine);
        }

        private IEnumerator SettingBoxFill(float fill)
        {
            float previousFill = boxFill.fillAmount;
            
            float t = 0;
            while (t < 1)
            {
                SetBoxFill(Mathf.Lerp(previousFill, fill, t));
                t += Time.deltaTime * boxFillChangeSpeed;
                yield return null;
            }

            SetBoxFill(fill);
        }

        private void SetBoxFill(float fill)
        {
            Color boxColor = colorGradient.Evaluate(fill);

            boxColor.a = .2f;
            boxOverlay.color = boxColor;

            boxColor.a = 1f;
            boxFill.color = boxColor;
            boxFill.fillAmount = fill;

            boxFillText.text = (int)(fill * 100) + "%";
        }
        
        private float GetBackpackCapacity() => backpackControl.CurrentItemsWeight / (float)backpackControl.MaxItemsWeight;
    }
}
