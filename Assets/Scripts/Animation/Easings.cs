using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Easings
{
    public static float EaseOutCubic(float number)
    {
        return 1 - Mathf.Pow(1 - number, 3);
    }

    public static float EaseOutCirc(float number)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(number - 1, 2));
    }

    public static float EaseInCirc(float number)
    {
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(number, 2));
    }
}
