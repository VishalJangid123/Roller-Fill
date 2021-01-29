using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // set random texture and color
        transform.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)GameManager.instance._backgroundTexture[Random.Range(0, GameManager.instance._backgroundTexture.Length)];
        transform.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0, 1);
    }

    
}
