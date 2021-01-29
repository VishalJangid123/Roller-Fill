using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class ButtonController : MonoBehaviour
{
    ButtonAccess[] _buttonController;
    [SerializeField]
    int totalNumberOfLevels = 0;
    [SerializeField]
    int completedLevels = 0;
    [SerializeField]
    List<string> swipeCount;
    [SerializeField]
    int[] levels;
    [SerializeField]
    int level_index;

    public Text _levelText;
    public Text _swipeCountText;

    GameObject left_btn_selector, right_btn_selector;
    
    private void Start()
    {
        _buttonController = FindObjectsOfType<ButtonAccess>();
        swipeCount = new List<string>();

        foreach(var button in _buttonController)
        {
            if(button._desiredButton == BUTTON_ENUM.PLAY_START)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => StartGame());
            }

            if (button._desiredButton == BUTTON_ENUM.SELECTOR_LEFT)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => OnLeftButtonClicked());
                left_btn_selector = button.gameObject;
            }

            if (button._desiredButton == BUTTON_ENUM.SELECTOR_RIGHT)
            {
                button.gameObject.GetComponent<Lean.Gui.LeanButton>().OnClick.AddListener(() => OnRightButtonClicked());
                right_btn_selector = button.gameObject;

            }
        }

        // get total number of levels
        totalNumberOfLevels = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        completedLevels = PlayerPrefs.HasKey("level") ? PlayerPrefs.GetInt("level") : 1;
        getSwipeCount();
        level_index = completedLevels;
        setSelectorText(level_index);

    }

    private void Update()
    {
        if (level_index <= 1)
        {
            left_btn_selector.GetComponent<Lean.Gui.LeanButton>().interactable = false;

        }
        else
        {
            left_btn_selector.GetComponent<Lean.Gui.LeanButton>().interactable = true;

        }

        if (level_index >= completedLevels)
        {
            right_btn_selector.GetComponent<Lean.Gui.LeanButton>().interactable = false;

        }
        else
        {
            right_btn_selector.GetComponent<Lean.Gui.LeanButton>().interactable = true;

        }
        
        

        



    }

    private void OnRightButtonClicked()
    {
        level_index = level_index + 1;
        setSelectorText(level_index);
    }

    private void OnLeftButtonClicked()
    {
        level_index = level_index - 1;
        setSelectorText(level_index);
    }

    public void StartGame()
    {
        //get the last level completed



        /*
        int load_level =  1;
        if (PlayerPrefs.HasKey("level"))
        {
            load_level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
        }
        */

        UnityEngine.SceneManagement.SceneManager.LoadScene(level_index);
        GameManager.instance.SetupNewLevel();
    }

    void getSwipeCount()
    {
        for( int i=1; i < completedLevels; i++)
        {
            swipeCount.Add( (PlayerPrefs.GetString("level_time_" + i.ToString() ).ToString()));
        }
       
    }

    void setSelectorText(int index)
    {
        _levelText.text = "Level " + (index).ToString();

        if (index == completedLevels)
            _swipeCountText.text = "";
        else
            _swipeCountText.text = swipeCount[index - 1] + " Seconds";

    }


}
