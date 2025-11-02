using System;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [SerializeField] private Tab[] tabs;

    private Tab activeTab;

    private void Awake()
    {
        HideTabs();
        SetupActive();
    }

    private void HideTabs()
    {
        foreach (Tab tab in tabs)
            tab.Hide();
    }

    private void SetupActive()
    {
        if (tabs.Length != 0)
        {
            activeTab = tabs[0];
            activeTab.Show();
        }
    }

    private void OnEnable()
    {
        foreach (Tab tab in tabs)
            tab.button.onClick.AddListener(() => Open(tab));
    }

    private void OnDisable()
    {
        foreach (Tab tab in tabs)
            tab.button.onClick.RemoveListener(() => Open(tab));
    }

    private void Open(Tab tab)
    {
        activeTab.Hide();
        activeTab = tab;
        activeTab.Show();
    }

    private void OnValidate()
    {
        ValidateTabs();
    }

    private void ValidateTabs()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        Canvas[] canvases = GetComponentsInChildren<Canvas>();

        for (int i = 0; i < canvases.Length; i++)
        {
            if (i < buttons.Length)
            {
                tabs[i] = new Tab(buttons[i], canvases[i]);
            }
            else
            {
                break;
            }
        }
    }
}

[Serializable]
public class Tab
{
    public Button button;
    public Canvas canvas;

    public Tab(Button button, Canvas canvas)
    {
        this.button = button;
        this.canvas = canvas;
    }

    public void Show() => ToggleComponents(true);

    public void Hide() => ToggleComponents(false);

    private void ToggleComponents(bool active)
    {
        canvas.enabled = active;
        button.interactable = !active;
    }
}
