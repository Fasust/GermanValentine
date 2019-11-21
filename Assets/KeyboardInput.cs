using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour 
{
    public float Horizontal;
    public float Vertical;

    // Update is called once per frame
    void Update()
    {
         Horizontal = Input.GetAxisRaw("Horizontal");
         Vertical = Input.GetAxisRaw("Vertical");

    }
}
