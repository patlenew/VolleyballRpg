using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Board Settings", menuName = "VolleyBall RPG/Board Settings", order = 5)]
public class BoardSettings : ScriptableObject
{
    public int boardDimensionSize = 3;
    public BoardTile tileRef;
}
