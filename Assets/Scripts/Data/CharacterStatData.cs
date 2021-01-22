using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stat", menuName = "VolleyBall RPG/CharacterStats", order = 1)]
public class CharacterStatData : ScriptableObject
{
    public int baseHp = 10;
    public int baseCardTurns = 4;
}
