using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    #region Transform

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


    #endregion

    #region Arrays


    public static void Shuffle<T>(this T[] array)
    {
        int length = array.Length;
        System.Random rand = new System.Random();

        while (length > 1)
        {
            int element = rand.Next(length--);
            T temp = array[length];
            array[length] = array[element];
            array[element] = temp;
        }
    }

    #endregion
}
