using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    private GroundSectionController[] _groundPieces;

    private void Start()
    {
        SetupNewLevel();
    }

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
    private void SetupNewLevel()
    {
        _groundPieces = FindObjectsOfType<GroundSectionController>();
    }

    void CheckLevelComplete()
    {
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
            Debug.Log("level finshed");
        }
    }
}
