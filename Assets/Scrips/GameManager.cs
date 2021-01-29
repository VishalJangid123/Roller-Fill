using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Panel_List
{
    LEVEL_COMPLETE_PANEL,
}

public enum BUTTON_ENUM
{
    PLAY_START,
    RESTART_LEVEL,
    NEXT_LEVEL,
    MAIN_MENU,
    SELECTOR_LEFT,
    SELECTOR_RIGHT,

}
public class GameManager : Singleton<GameManager>
{
    public static GameManager singleton;

    private GroundSectionController[] _groundPieces;

    [HideInInspector]
    public GameObject _levelCompletePanel;
    public UnityEngine.Object[] _backgroundTexture;
    public Color _groundColor;


    public float timeTook = 0;
    bool startTimer = false;
    private void Start()
    {
        SetupNewLevel();

        // get all background texture
        _backgroundTexture = Resources.LoadAll("Texture/Background/", typeof(Texture2D));
    }

    private void Update()
    {
        if (startTimer == true)
            timeTook += Time.deltaTime;
    }


    public void SetupNewLevel()
    {
        _groundPieces = FindObjectsOfType<GroundSectionController>();

        
        // random ground color
        Color groundColor = UnityEngine.Random.ColorHSV(0.1f, 1);
        _groundColor = UnityEngine.Random.ColorHSV(0.1f, 1);
        //Color groundColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        foreach(var piece in _groundPieces)
        {
            piece.GetComponent<Renderer>().material.SetColor("_Color",groundColor);
            piece.GetComponent<MeshRenderer>().material.SetColor("_Color",groundColor);
            Debug.Log(piece.GetComponent<Renderer>().material.color);
            Debug.Log(piece.GetComponent<MeshRenderer>().material.color);
        }

        // random background
        Color backgroundColor = UnityEngine.Random.ColorHSV(0.1f, 1);
        Camera.main.backgroundColor = backgroundColor;

        timeTook = 0;
        startTimer = true;
    }

    public void CheckLevelComplete()
    {
        _groundPieces = FindObjectsOfType<GroundSectionController>();

        bool levelFinished = true;
        for(int i = 0; i < _groundPieces.Length; i++)
        {

            if (!_groundPieces[i].isColored)
            {
                levelFinished = false;
                break;
            }
        }

        if (levelFinished)
        {
            // level finished move to next level
            Debug.Log("level finished");
            startTimer = false;
            var levelCompleteTime = startTimer.ToString().Substring(0,3);
            int activeScene = SceneManager.GetActiveScene().buildIndex;

            // add swipe count

            PlayerPrefs.SetString("level_time_" + activeScene.ToString(), levelCompleteTime.ToString());


            //string swipeC = PlayerPrefs.GetString("swipeCount");
            //List<string> swipe_C = swipeC.Split(',').ToList();


            //if(swipe_C.Count == 0)
            //{
            //    swipe_C.Add(GameManager.instance.swipeCountInLevel.ToString());
            //}
            //else
            //{
            //    int swipeCountLevelStored = swipe_C.Count;
            //    if(activeScene - 2 <= swipeCountLevelStored)
            //    {
            //        swipe_C[activeScene - 2] = GameManager.instance.swipeCountInLevel.ToString();
            //    }
            //    else
            //    {
            //        swipe_C.Add(GameManager.instance.swipeCountInLevel.ToString());
            //    }
            //}
            //string final = "";
            //if(swipe_C.Count == 1)
            //{
            //    final = swipe_C[0];
            //}
            //else
            //{
            //    foreach( var count in swipe_C)
            //    {
            //        final = final + "," + count;
            //    }
            //}

            //PlayerPrefs.SetString("swipeCount", final);

          

            if (activeScene == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);

            }
            else
            {
                PlayerPrefs.SetInt("level", activeScene);
                // show canvas here
                _levelCompletePanel.SetActive(true);
            }


        }
    }

    public void OnRestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _levelCompletePanel.SetActive(false);

    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _levelCompletePanel.SetActive(false);

        SetupNewLevel();
    }




    protected override void InternalInit()
    {
       
    }

    protected override void InternalOnDestroy()
    {
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
