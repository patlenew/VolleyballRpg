using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deck Visual, where you see the actual cards
// Deck logic is here also for now
public class HUD_DeckPanel : SpriteHUDPanel
{
    [Header("Deck Settings")]
    [SerializeField] private Card _cardRef;
    [SerializeField] private Transform _cardParent;

    private int _startCardCount = 5; // for Test
    private DeckData _data;
    private Stack<Card> _cards = new Stack<Card>();
    private List<Card> _cardsInHand = new List<Card>();
    private List<Card> _discardedCards = new List<Card>();

    public override void AddListeners()
    {
        base.AddListeners();

        GameEvents.PrepareDeck.Register(OnReceiveDeckData);
        GameEvents.OnStartTurn_Local.Register(OnStartTurn);
    }

    public override void RemoveListeners()
    {
        base.RemoveListeners();

        GameEvents.PrepareDeck.Remove(OnReceiveDeckData);
        GameEvents.OnStartTurn_Local.Remove(OnStartTurn);
    }

    private void OnReceiveDeckData(DeckData data)
    {
        _data = data;

        _cards = CreateDeck();
    }

    public void Reshuffle()
    {

    }

    private void OnStartTurn()
    {
        Draw(_startCardCount);
    }

    public void ClearHand()
    {
        _cardsInHand.Clear();
    }

    // TO ADD: Reshuffle when hand empty
    // Check limit Card
    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card card = _cards.Pop();
            card.Show();

            _cardsInHand.Add(card);
        }

        AdjustHand();
    }

    public void AdjustHand()
    {
        Vector3 offsetBetweenCards = Vector3.zero;
        float step = 45f / _cardsInHand.Count;
        float angle = step;

        // Test very uglu
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            //_cardsInHand[i].transform.SetLocalEulerAngleZ(angle);

            Vector3 offset = _cardsInHand[i].transform.localPosition;

            offset.z += offsetBetweenCards.z;

            if (i > _cardsInHand.Count / 2)
            {
                offset.x -= offsetBetweenCards.x;
            }
            else
            {
                offset.x += offsetBetweenCards.x;
            }

            _cardsInHand[i].transform.localPosition = offset;

            angle += step;
            offsetBetweenCards.x += 1f;
            offsetBetweenCards.z += 0.1f;

            if (i == _cardsInHand.Count / 2)
            {
                offsetBetweenCards.x = 0f;
            }
        }
    }

    #region Helpers

    private Stack<Card> CreateDeck()
    {
        Stack<Card> newDeck = new Stack<Card>();
        CardData[] cardData = _data.GetCardData();

        for (int i = 0; i < cardData.Length; i++)
        {
            Card card = CreateCard(cardData[i]);

            newDeck.Push(card);
        }

        newDeck.ToArray().Shuffle();

        return newDeck;
    }

    private Card CreateCard(CardData data)
    {
        Card card = Instantiate(_cardRef, _cardParent);
        card.Hide();
        card.SetData(data);

        return card;
    }

    #endregion
}
