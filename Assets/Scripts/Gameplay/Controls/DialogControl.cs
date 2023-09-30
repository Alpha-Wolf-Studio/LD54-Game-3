using System.Collections;
using Gameplay.Data;
using TMPro;
using UnityEngine;

namespace Gameplay.Controls
{
    public class DialogControl : MonoBehaviour
    {
        [SerializeField] private float dialogSpeed;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Animator dialogAnimator;
        public static DialogControl Instance { get; private set; }

        private readonly int _showDialogHash = Animator.StringToHash("Show");

        private IEnumerator _showDialogCoroutine;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void ShowDialog(DialogData dialogData)
        {
            if(_showDialogCoroutine != null)
                StopCoroutine(_showDialogCoroutine);

            _showDialogCoroutine = ShowingDialog(dialogData);
            StartCoroutine(_showDialogCoroutine);
        }
        
        public void HideCurrentDialog()
        {
            if(_showDialogCoroutine != null)
                StopCoroutine(_showDialogCoroutine);
            
            HideDialogBox();
        }

        private IEnumerator ShowingDialog(DialogData dialogData)
        {
            ShowDialogBox();

            string text = dialogData.DialogKey;
            //string text = Loc.ReplaceKey(dialogData.DialogKey);
            string currentText = "";

            int currentIndex = 0;
            
            WaitForSeconds wait = new WaitForSeconds(1 / dialogSpeed);
            
            while (currentText.Length != text.Length)
            {
                currentText += text[currentIndex];
                dialogText.text = currentText;
                yield return wait;
                currentIndex++;
            }
        }
        
        private void ShowDialogBox() => dialogAnimator.SetBool(_showDialogHash, true);
        private void HideDialogBox() => dialogAnimator.SetBool(_showDialogHash, false);

    }
}
