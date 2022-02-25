using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetButtonDown("Horizontal2"))
        {
            Debug.Log("Horizontal 2 detected");
        }

        if (Input.GetButtonDown("Jump2"))
        {
            Debug.Log("Jump 2 detected");
        }
    }
}
