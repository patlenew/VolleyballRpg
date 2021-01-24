using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// To keep in mind: maybe have different card per class if there also multiple "classes"
public class Card : MonoBehaviour
{
    [SerializeField] private Image _cardImage;
    [SerializeField] private TMP_Text _descriptionTitle;

    private CardData _data;

    public void SetData(CardData data)
    {
        _data = data;

        _cardImage.sprite = data.GetCardSprite();
        _descriptionTitle.text = data.GetDescription();
    }
}
