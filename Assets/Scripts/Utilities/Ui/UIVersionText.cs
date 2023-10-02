using System;
using TMPro;
using UnityEngine;

namespace Utilities.Ui
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIVersionText : MonoBehaviour
    {
        private TextMeshProUGUI _versionText;

        private void Awake()
        {
            _versionText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _versionText.text = Application.version;
        }
    }
}
