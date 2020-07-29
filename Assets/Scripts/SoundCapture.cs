using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCapture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (string str in Microphone.devices)
        {
            Debug.Log(str);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
