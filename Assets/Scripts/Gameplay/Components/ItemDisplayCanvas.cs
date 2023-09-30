using UnityEngine;
using UnityEngine.UI;
using Audio;
using System.Collections.Generic;
using System.Linq;

public class ItemDisplayCanvas : MonoBehaviourSingletonInScene<ItemDisplayCanvas>
{
    public void OpenCanvas(GameObject go)
    {
        GameObject gameObj = CheckInstantiation(go);

        if (gameObj.activeSelf)
            return;

        gameObj.SetActive(true);
    }

    public void OpenCanvas(GameObject go, AudioClip openClip)
    {
        GameObject gameObj = CheckInstantiation(go);

        if (gameObj.activeSelf)
            return;

        gameObj.SetActive(true);

        AudioManager.Instance.PlaySfx(openClip);
    }

    public void CloseCanvas(GameObject go)
    {
        if (!go.activeSelf)
            return;

        go.SetActive(false);
    }

    public void CloseCanvas(GameObject go, AudioClip closeClip)
    {
        if (!go.activeSelf)
            return;

        AudioManager.Instance.PlaySfx(closeClip);
        go.SetActive(false);
    }

    private GameObject CheckInstantiation(GameObject go)
    {
        for (short i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == go.name + "(Clone)")
                return transform.GetChild(i).gameObject;
        }

        return Instantiate(go, transform);
    }
}