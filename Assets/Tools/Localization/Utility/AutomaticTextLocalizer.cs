using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AutomaticTextLocalizer : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string key;
    
    private void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();

        string s = Loc.ReplaceKey(key); 

        textField.text = s;

        FindObjectOfType<UiControllerSettings>().onLanguageChanged += (() => Localize());
    }

    private void Localize()
    {
        string s = Loc.ReplaceKey(key);

        textField.text = s;
    }
}
