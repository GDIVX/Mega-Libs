using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Hand
{

    public int Count { get => cardsIDs.Count; }
    public List<int> cardsIDs { get => _cardsIDs; set => _cardsIDs = value; }

    [ShowInInspector]
    List<int> _cardsIDs = new List<int>();

    internal List<Card> ToList()
    {
        List<Card> res = new List<Card>();
        foreach (var id in cardsIDs)
        {
            Card card = Card.GetCard(id);
            if (card != null)
            {
                res.Add(card);
            }
        }
        return res;
    }

    public void AddCard(Card card)
    {
        if (card == null)
        {
            Debug.LogWarning("Trying to add a null card to hand");
            return;
        }
        if (Has(card.ID))
        {
            Debug.LogError($"Trying to add the card {card} with ID {card.ID} to the hand more then once ");
            return;
        }
        cardsIDs.Add(card.ID);
        UIController.Instance.handGUI.AddCard(card);
    }

    internal int GetCardIDByIndex(int index)
    {
        return GetCardByIndex(index).ID;
    }

    internal Card GetCardByIndex(int index)
    {
        return Card.GetCard(cardsIDs[index]);
    }

    public bool IsEmpty()
    {
        return Count <= 0;
    }

    internal void RemoveCard(int ID)
    {
        if (!Has(ID))
        {
            Debug.LogError("Trying to remove a card that is not in the hand");
            return;
        }
        cardsIDs.Remove(ID);
        UIController.Instance.handGUI.RemoveCard(ID);
    }




    public bool Has(int ID)
    {
        return cardsIDs.Contains(ID);
    }




}
