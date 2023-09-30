using System;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerMoreGames : MonoBehaviour
{
    public event Action onMoreGamesCloseButtonClicked;
    
    public CanvasGroup canvasGroup;
    [SerializeField] private Button closeButton;

    [Header("Other Games UI")]
    [SerializeField] private Button game1Button;
    [SerializeField] private Button game2Button;
    
    private void Awake ()
    {
        closeButton.onClick.AddListener(OnCreditsCloseButtonClicked);
        game1Button.onClick.AddListener(OnGame1ButtonClicked);
        game2Button.onClick.AddListener(OnGame2ButtonClicked);
    }

    private void OnDestroy ()
    {
        closeButton.onClick.RemoveAllListeners();
        game1Button.onClick.RemoveAllListeners();
        game2Button.onClick.RemoveAllListeners();
    }

    private void OnCreditsCloseButtonClicked () => onMoreGamesCloseButtonClicked?.Invoke();

    private void OnGame1ButtonClicked () => Application.OpenURL("https://ldjam.com/events/ludum-dare/54/$371816");
    private void OnGame2ButtonClicked () => Application.OpenURL("https://ldjam.com/events/ludum-dare/54/$371817");
}
