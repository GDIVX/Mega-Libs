using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runtime data for the cards the player can find in the game
/// </summary>

public class Deck
{
    internal List<CardData> commonCards;
    internal List<CardData> uncommonCards;
    internal List<CardData> rareCards;
    internal List<CardData> exileCards;
    public CardData reseveCard;
    private DeckData data;

    public Deck(DeckData deckData)
    {
        this.data = deckData;

        commonCards = data.commonCards;
        uncommonCards = data.uncommonCards;
        rareCards = data.rareCards;
        exileCards = data.exileCards;
        reseveCard = data.Reserve;
    }

        public void AddCardOption(Card from){
        CardData data = from.data;
        if(data.UnlockCards == null) return;

        foreach (var unlock in data.UnlockCards)
        {
            switch (unlock.rarity)
            {
                case RandomSelector.Rarity.COMMON:
                    commonCards.Add(unlock);
                    break;
                case RandomSelector.Rarity.UNCOMMON:
                    uncommonCards.Add(unlock);
                    break;
                default:
                    rareCards.Add(unlock);
                    break;
            }
            
        }
    }
}
