using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSectionController : MonoBehaviour
{
    public bool isColored = false;

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
