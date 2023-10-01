using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Audio;
using Gameplay.UI;

public class Xilophone : ItemDisplayPanel
{
    public List<Key> keys = new List<Key>();

    private void Awake()
    {
        for(short i=0;i<keys.Count; i++)
        {
            var pointerEvent = new EventTrigger.Entry();

            pointerEvent.eventID = EventTriggerType.PointerClick;

            pointerEvent.callback.AddListener((e) => PressedKey(keys[i].sound));

            keys[i].trigger.triggers.Add(pointerEvent);
        }
    }

    public void PressedKey(AudioClip clip)
    {
        AudioManager.Instance.PlaySfx(clip);
    }
}

[System.Serializable]
public class Key
{
    public EventTrigger trigger;
    public AudioClip sound;
}
