using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSectionController : MonoBehaviour
{
    public bool isColored = false;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = GameManager.instance._groundColor;

    }
    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;
        GameManager.instance.CheckLevelComplete();
    }
}
