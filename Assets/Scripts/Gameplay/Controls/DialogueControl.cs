using System.Collections;
using Gameplay.Data;
using TMPro;
using UnityEngine;

namespace Gameplay.Controls
{
    public class DialogueControl : MonoBehaviour
    {
        [SerializeField] private float dialogueSpeed = 20;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Animator dialogueAnimator;
        public static DialogueControl Instance { get; private set; }

        private readonly int _showDialogueHash = Animator.StringToHash("Show");

        private IEnumerator _showDialogueCoroutine;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void ShowDialogue(string dialogueKey)
        {
            if(_showDialogueCoroutine != null)
                StopCoroutine(_showDialogueCoroutine);

            _showDialogueCoroutine = ShowingDialogue(dialogueKey);
            StartCoroutine(_showDialogueCoroutine);
        }
        
        public void HideCurrentDialogue()
        {
            if(_showDialogueCoroutine != null)
                StopCoroutine(_showDialogueCoroutine);
            
            HideDialogueBox();
        }

        private IEnumerator ShowingDialogue(string dialogueKey)
        {
            ShowDialogueBox();

            string text = dialogueKey;
            //string text = Loc.ReplaceKey(dialogKey);
            string currentText = "";

            int currentIndex = 0;
            
            WaitForSeconds wait = new WaitForSeconds(1 / dialogueSpeed);
            
            while (currentText.Length != text.Length)
            {
                currentText += text[currentIndex];
                dialogueText.text = currentText;
                yield return wait;
                currentIndex++;
            }
        }
        
        private void ShowDialogueBox() => dialogueAnimator.SetBool(_showDialogueHash, true);
        private void HideDialogueBox() => dialogueAnimator.SetBool(_showDialogueHash, false);

    }
}
