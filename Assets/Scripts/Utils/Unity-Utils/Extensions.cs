using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float GetScaleX(this Transform transform)
    {
        return transform.localScale.x;
    }

    public static float GetScaleY(this Transform transform)
    {
        return transform.localScale.y;
    }

    public static float GetScaleZ(this Transform transform)
    {
        return transform.localScale.z;
    }
}
