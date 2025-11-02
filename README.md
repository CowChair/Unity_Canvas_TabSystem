# Unity Tab System

## Video (YouTube - Tab System for Unity)
[![Tab System](https://img.youtube.com/vi/ID/0.jpg)](https://www.youtube.com/watch?v=ID)

## Overview
This project is a single script example for a Tab System in Unity. The script relies on a pure C# class for linking each Button and Canvas. The script itself will fill the collection of tabs based on the order of items in the Hierarchy.

## Setup
For the setup, add this script to its own `GameObject`, and child it to `Canvas`. And then, depending on how many tabs you want, you'll need to create that many `Buttons` and additional `Canvases`.

## Code
I've provided a sample project, but this is the script in its entirety.
```
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
```

## Approach
The script tracks a single active Canvas by listening to the `OnClick` event for the button associated with each Canvas. This association is automatically done in the editor using Unity's `OnValidate` method. Doing this made it easier to ensure that each `Button` and `Canvas` coupling was correct. 

## Important Bits

This code is where the connection to `Click > Open` logic is made.
```
    foreach (Tab tab in tabs)
        tab.button.onClick.AddListener(() => Open(tab));
```

## End
And, that's it! I hope you find this helpful.

-C
