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

    public static void SetEulerAngleX(this Transform transform, float x)
    {
        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetEulerAngleY(this Transform transform, float y)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    public static void SetEulerAngleZ(this Transform transform, float z)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }

    public static void SetLocalEulerAngleX(this Transform transform, float x)
    {
        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAngleY(this Transform transform, float y)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAngleZ(this Transform transform, float z)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
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

    // Only works for nxn size grid 
    public static void ShuffleGrid<T>(this T[,] array)
    {
        int lengthRow = array.GetLength(1);
        System.Random rand = new System.Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = rand.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            T temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
    }

    public static void Copy<T>(this T[,] array, out T[,] newArray)
    {
        int rowCount = array.GetLength(0);
        int colCount = array.GetLength(1);

        newArray = new T[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                newArray[i, j] = array[i, j];
            }
        }
    }

    public static T RandomElement<T>(this T[,] array)
    {
        int rowCount = array.GetLength(0);
        int colCount = array.GetLength(1);

        return array[Random.Range(0, rowCount), Random.Range(0, colCount)];
    }

    #endregion
}
