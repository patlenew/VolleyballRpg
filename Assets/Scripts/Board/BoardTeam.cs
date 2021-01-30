using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: Probably create teams randomly
public class BoardTeam : MonoBehaviour
{
    [SerializeField] private BoardCharacterData[] _boardCharacters;
    [SerializeField] private BoardTeamData _currentData;
    [SerializeField] private DeckData _deck;

    public BoardCharacterData[] GetCharactersData()
    {
        return _boardCharacters;
    }
}
