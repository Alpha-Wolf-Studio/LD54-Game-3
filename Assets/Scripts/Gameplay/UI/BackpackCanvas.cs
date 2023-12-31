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

        [Header("General UI")]
        [SerializeField] private CanvasGroup generalCanvasGroup;
        [SerializeField] private float showSpeed = 2f;
        
        [Header("Box Amount UI")]
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private Image boxOverlay;
        [SerializeField] private Image boxFill;
        [SerializeField] private CanvasGroup tooltipPanel;
        [SerializeField] private TextMeshProUGUI boxFillText;
        [SerializeField] private float boxFillChangeSpeed;
        [SerializeField] private float timeBeforeHide = 1f;

        [Header("Move Out UI")] 
        [SerializeField] private Button moveOutButton;
        
        private IEnumerator _changeBoxCapacityCoroutine;
        private IEnumerator _changeBoxPanelStateCoroutine;
        
        private void Awake()
        {
            backpackControl.OnItemAdded += ItemAdded;
            backpackControl.OnItemRemoved += ItemRemoved;

            backpackControl.OnEnabled += BackpackEnable;
            backpackControl.OnDisabled += BackpackDisable;
            
            moveOutButton.onClick.AddListener(MoveOut);
        }

        private void Start()
        {
            generalCanvasGroup.alpha = 0;
            generalCanvasGroup.interactable = false;
            generalCanvasGroup.blocksRaycasts = false;
        }

        private void OnDestroy()
        {
            backpackControl.OnItemAdded -= ItemAdded;
            backpackControl.OnItemRemoved -= ItemRemoved;

            backpackControl.OnEnabled -= BackpackEnable;
            backpackControl.OnDisabled -= BackpackDisable;
            
            moveOutButton.onClick.RemoveListener(MoveOut);
        }

        private void BackpackDisable()
        {
            StartCoroutine(HidingBackpack());
        }
        
        private IEnumerator HidingBackpack()
        {
            generalCanvasGroup.interactable = false;
            generalCanvasGroup.blocksRaycasts = false;
            
            float t = 1;
            while (t > 0)
            {
                generalCanvasGroup.alpha = t;
                t -= Time.deltaTime * showSpeed;
                yield return null;
            }

            generalCanvasGroup.alpha = 0;
        }
        
        private void BackpackEnable()
        {
            StartCoroutine(ShowingBackpack());
        }

        private IEnumerator ShowingBackpack()
        {
            float t = 0;
            while (t < 1)
            {
                generalCanvasGroup.alpha = t;
                t += Time.deltaTime * showSpeed;
                yield return null;
            }

            generalCanvasGroup.alpha = 1;
            generalCanvasGroup.interactable = true;
            generalCanvasGroup.blocksRaycasts = true;
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

        public void ChangeBoxPanelState(bool active)
        {
            if(_changeBoxPanelStateCoroutine != null)
                StopCoroutine(_changeBoxPanelStateCoroutine);

            _changeBoxPanelStateCoroutine = ChangingBoxPanelState(active);
            StartCoroutine(_changeBoxPanelStateCoroutine);
        }

        private IEnumerator ChangingBoxPanelState(bool active)
        {
            float currentAlpha = Mathf.InverseLerp(0, 1, tooltipPanel.alpha);
            
            if (active)
            {
                while (currentAlpha < 1)
                {
                    tooltipPanel.alpha = currentAlpha;
                    currentAlpha += Time.deltaTime * showSpeed;
                    yield return null;
                }
                tooltipPanel.alpha = 1;
            }
            else
            {
                while (currentAlpha > 0)
                {
                    tooltipPanel.alpha = currentAlpha;
                    currentAlpha -= Time.deltaTime * showSpeed;
                    yield return null;
                }
                tooltipPanel.alpha = 0;
            }
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
            ChangeBoxPanelState(true);
            float previousFill = boxFill.fillAmount;
            
            float t = 0;
            while (t < 1)
            {
                SetBoxFill(Mathf.Lerp(previousFill, fill, t));
                t += Time.deltaTime * boxFillChangeSpeed;
                yield return null;
            }

            SetBoxFill(fill);

            yield return new WaitForSeconds(timeBeforeHide);
            ChangeBoxPanelState(false);
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

        private void MoveOut()
        {
            GameflowControl.Instance.FinishMainGame();
        }
    }
}
