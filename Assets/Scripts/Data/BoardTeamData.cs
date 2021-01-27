using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[CreateAssetMenu(fileName = "Board Team Data", menuName = "VolleyBall RPG/Board Team Data", order = 1)]
public class BoardTeamData : ScriptableObject
{
    public int baseBallPower = 5;
    public int baseReflex = 4;
}
