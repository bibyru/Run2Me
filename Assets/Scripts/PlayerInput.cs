using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Pause Menu");
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<CharMove>().Move(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<CharMove>().Move(1);
        }
        else
        {
            GetComponent<CharMove>().Move();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            GetComponent<CharMove>().Yump();
        }
    }
}
