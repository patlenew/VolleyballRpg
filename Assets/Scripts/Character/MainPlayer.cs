using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private int _currentHp;
    private int _currentCardTurns;

    public void SetBaseData(CharacterStatData data)
    {
        _currentHp = data.baseHp;
        _currentCardTurns = data.baseCardTurns;
    }
}
