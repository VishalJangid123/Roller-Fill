using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private ButtonAccess[] _buttons;
    public PanelAcess[] _panels;



    void Start()
    {
        _buttons = FindObjectsOfType<ButtonAccess>();
        _panels = FindObjectsOfType<PanelAcess>();
        Debug.Log(_buttons);
        foreach (var button in _buttons)
        {
            if (button._desiredButton == BUTTON_ENUM.RESTART_LEVEL)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => GameManager.instance.OnRestartLevel());
            }
            if (button._desiredButton == BUTTON_ENUM.NEXT_LEVEL)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => GameManager.instance.OnNextLevel());
            }
            if (button._desiredButton == BUTTON_ENUM.MAIN_MENU)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => GameManager.instance.OnNextLevel());
            }

        }

        foreach (var panel in _panels)
        {
            if (panel._desiredPanel == Panel_List.LEVEL_COMPLETE_PANEL)
            {
                GameManager.instance._levelCompletePanel = panel.gameObject;
                GameManager.instance._levelCompletePanel.SetActive(false);
            }
        }
    }

    
}
