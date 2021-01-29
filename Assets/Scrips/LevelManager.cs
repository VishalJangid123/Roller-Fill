using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject levelName;
    void Start()
    {
        int totalLevel = SceneManager.GetAllScenes().Length;
        int levelCompleted = PlayerPrefs.HasKey("level") ? PlayerPrefs.GetInt("level") : 1;

        for (int i=0; i <= levelCompleted; i++)
        {
            GameObject g = Instantiate(levelName);
            g.transform.SetParent(this.transform);
            g.GetComponentInChildren<Text>().text = "Level " + i.ToString();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
