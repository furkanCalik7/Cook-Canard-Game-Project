using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAlternateButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAlternateText;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private Transform pressAkeyToRebindUI;

    void Awake()
    {
        Instance = this;

        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        quitButton.onClick.AddListener(() =>
        {
            Hide();
        });

        MoveUpButton.onClick.AddListener(() =>
        {
            GameInput.Instance.Rebind(GameInput.Binding.MoveUp, HidePressAKeyToRebindUI);
        });

        MoveUpButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.MoveUp, HidePressAKeyToRebindUI);
                });
        MoveDownButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.MoveDown, HidePressAKeyToRebindUI);
                });
        MoveRightButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.MoveRight, HidePressAKeyToRebindUI);
                });
        MoveLeftButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.MoveLeft, HidePressAKeyToRebindUI);
                });
        InteractButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.Interact, HidePressAKeyToRebindUI);
                });

        InteractAlternateButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.InteractAlternate, HidePressAKeyToRebindUI);
                });

        PauseButton.onClick.AddListener(() =>
                {
                    ShowPressAKeyToRebindUI();
                    GameInput.Instance.Rebind(GameInput.Binding.Pause, HidePressAKeyToRebindUI);
                });
        Hide();
    }

    void Start()
    {

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
    }

    void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    void UpdateVisual()
    {
        soundEffectsText.text = "Sound effects: " + (SoundManager.Instance.GetVolume() * 10).ToString("0");
        musicText.text = "Music: " + (MusicManager.Instance.GetVolume() * 10).ToString("0");
        MoveUpText.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveDown);
        MoveRightText.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveRight);
        MoveLeftText.text = GameInput.Instance.GetBindingString(GameInput.Binding.MoveLeft);
        InteractText.text = GameInput.Instance.GetBindingString(GameInput.Binding.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingString(GameInput.Binding.InteractAlternate);
        PauseText.text = GameInput.Instance.GetBindingString(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressAKeyToRebindUI()
    {
        pressAkeyToRebindUI.gameObject.SetActive(true);
    }

    private void HidePressAKeyToRebindUI()
    {
        pressAkeyToRebindUI.gameObject.SetActive(false);
        UpdateVisual();
    }
}
