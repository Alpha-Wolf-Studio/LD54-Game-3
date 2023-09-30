using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Audio;

public class ItemDisplayCanvas : MonoBehaviourSingletonInScene<ItemDisplayCanvas>
{
    public TMPro.TextMeshProUGUI itemNameText;
    public SpriteRenderer itemImage;
    
    public void OpenCanvas()
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
    }

    public void OpenCanvas(AudioClip openClip)
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
        AudioManager.Instance.PlaySfx(openClip);
    }

    public void PopulateData(string name, Sprite sprite)
    {
        itemNameText.text = name;
        itemImage.sprite = sprite;
    }

    public void CloseCanvas()
    {
        if (!gameObject.activeSelf)
            return;

        gameObject.SetActive(false);
    }

    public void CloseCanvas(AudioClip closeClip)
    {
        if (!gameObject.activeSelf)
            return;

        AudioManager.Instance.PlaySfx(closeClip);
        gameObject.SetActive(false);
    }
}
