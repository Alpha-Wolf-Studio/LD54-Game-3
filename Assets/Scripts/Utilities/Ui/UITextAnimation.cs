using TMPro;
using UnityEngine;

namespace Utilities.Ui
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextAnimation : MonoBehaviour
    {

        [Header("Scale Animation Configuration")]
        [SerializeField] private float speed = .2f;
        [SerializeField] private float scaleDifference = .05f;
        
        private TextMeshProUGUI _text;
        
        private Vector2 _textStartScale;
        private Vector2 _textCurrentScale;
        private bool _scalingDown;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if (_scalingDown)
            {
                _textCurrentScale = _text.rectTransform.localScale;
                _textCurrentScale.x -= Time.deltaTime * speed;
                _textCurrentScale.y -= Time.deltaTime * speed;

                if (_textCurrentScale.x < 1 - scaleDifference)
                {
                    _textCurrentScale.x = 1 - scaleDifference;
                    _textCurrentScale.y = 1 - scaleDifference;
                    _scalingDown = false;
                }
            }
            else
            {
                _textCurrentScale = _text.rectTransform.localScale;
                _textCurrentScale.x += Time.deltaTime * speed;
                _textCurrentScale.y += Time.deltaTime * speed;

                if (_textCurrentScale.x > 1)
                {
                    _textCurrentScale.x = 1;
                    _textCurrentScale.y = 1;
                    _scalingDown = true;
                }
            }

            _text.rectTransform.localScale = _textCurrentScale;
        }
    }
}
