using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerValues.score -= 50;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerValues.score += 50;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerValues.life += 50;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerValues.life -= 20;
        }

    }
}
