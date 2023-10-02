using Gameplay.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquishItem : ItemDisplayPanel
{
    public Animator Animator;
    public EventTrigger Trigger;
    public string State;

    private void Awake()
    {
        var pointerEvent = new EventTrigger.Entry();

        pointerEvent.eventID = EventTriggerType.PointerDown;

        pointerEvent.callback.AddListener((e) => PlayAnimation(State));

        Trigger.triggers.Add(pointerEvent);
    }

    private void PlayAnimation(string state)
    {
        Animator.SetTrigger(state);
    }
}
