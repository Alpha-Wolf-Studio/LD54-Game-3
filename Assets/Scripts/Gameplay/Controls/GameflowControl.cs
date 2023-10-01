using Gameplay.Controls;
using Gameplay.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameflowControl : MonoBehaviourSingleton<GameflowControl>
{
    [Header("Panel Data")]
    public CanvasGroup IntroPanel;
    public CanvasGroup EndingPanel;
    public EndingPanelDataHolder EndingPanelDataHolder;

    [Header("Memory Data")]
    public string IntroKey;
    public string BadEndingKey;
    public List<MemoryEntry> endingEntries = new List<MemoryEntry>();

    private List<MemoryEntry> activeEndingEntries = new List<MemoryEntry>();
    private int currentEndingEntryIndex = 0;
    private FadeHelper fadeHelper;

    public void Start()
    {
        fadeHelper = new FadeHelper();
        InteractionControl.Instance.enabled = false;
        DialogueControl.Instance.ShowDialogue(IntroKey);
    }

    public void FinishMainGame()
    {
        for(short i = 0; i< BackpackControl.Instance.ItemsInBackpack.Count; i++)
        {
            for(short j = 0; j < endingEntries.Count; j++)
            {
                if (endingEntries[j].Data.RelatedID == BackpackControl.Instance.ItemsInBackpack[i].ID)
                    activeEndingEntries.Add(endingEntries[j]);
            }
        }

        EndGame();
    }

    public void StartGame()
    {
        fadeHelper.Fade(Instance, IntroPanel, FadeHelper.FadeDirection.FadeOut, 1.0f, 
            () => DisableGO(IntroPanel.gameObject));
        InteractionControl.Instance.enabled = true;
        
        DialogueControl.Instance.HideCurrentDialogue();
    }

    private void DisableGO(GameObject go)
    {
        go.SetActive(false);
    }

    public void EndGame()
    {
        fadeHelper.Fade(Instance, EndingPanel, FadeHelper.FadeDirection.FadeIn, 1.0f, () => SetEndingPanelData());
    }

    private void SetEndingPanelData()
    {
        if(endingEntries.Count <= 0)
        {
            EndingPanelDataHolder.BackwardsButton.gameObject.SetActive(false);
            EndingPanelDataHolder.ForwardButton.gameObject.SetActive(false);
            
            DialogueControl.Instance.ShowDialogue(BadEndingKey);
        }
        else
        {
            EndingPanelDataHolder.BackwardsButton.onClick.AddListener(() =>
            {
                if(currentEndingEntryIndex > 0)
                {
                    EndingPanelDataHolder.ItemCloseUpImage.sprite = activeEndingEntries[currentEndingEntryIndex - 1].Data.DialogueImage;
                    DialogueControl.Instance.ShowDialogue(activeEndingEntries[currentEndingEntryIndex - 1].Data.PostGameDialogueKey);
                }
                else
                {
                    EndingPanelDataHolder.ItemCloseUpImage.sprite = activeEndingEntries[activeEndingEntries.Count].Data.DialogueImage;
                    DialogueControl.Instance.ShowDialogue(activeEndingEntries[activeEndingEntries.Count].Data.PostGameDialogueKey);
                }
            });

            EndingPanelDataHolder.ForwardButton.onClick.AddListener(() =>
            {
                if (currentEndingEntryIndex < activeEndingEntries.Count)
                {
                    EndingPanelDataHolder.ItemCloseUpImage.sprite = activeEndingEntries[currentEndingEntryIndex + 1].Data.DialogueImage;
                    DialogueControl.Instance.ShowDialogue(activeEndingEntries[currentEndingEntryIndex + 1].Data.PostGameDialogueKey);
                }
                else
                {
                    EndingPanelDataHolder.ItemCloseUpImage.sprite = activeEndingEntries[0].Data.DialogueImage;
                    DialogueControl.Instance.ShowDialogue(activeEndingEntries[0].Data.PostGameDialogueKey);
                }
            });
            
            EndingPanelDataHolder.ItemCloseUpImage.sprite = activeEndingEntries[0].Data.DialogueImage;
            DialogueControl.Instance.ShowDialogue(activeEndingEntries[0].Data.PostGameDialogueKey);
        }
    }
}

public class FadeHelper
{
    public enum FadeDirection { FadeIn, FadeOut };

    public void Fade(MonoBehaviour behaviour, CanvasGroup group, FadeDirection direction, float duration, Action evt = null)
    {
        float desiredAlpha = 0.5f;

        if (direction == FadeDirection.FadeIn) desiredAlpha = 1.0f;
        else desiredAlpha = 0.0f;

        behaviour.StartCoroutine(FadeCanvas(group, desiredAlpha, duration, evt));
    }

    private IEnumerator FadeCanvas(CanvasGroup canvas, float desiredAlpha, float duration, Action evt = null)
    {
        float elapsedTime = 0;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            canvas.alpha = Mathf.Lerp(canvas.alpha, desiredAlpha, elapsedTime / duration);

            yield return null;
        }

        evt?.Invoke();
    }
}
