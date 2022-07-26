using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

[System.Serializable]
public class Card
{
   public int foodPrice, industryPrice, MagicPrice, hitpoint, speed, armor, attack;
   public CardData data { get { return _data; } }
   public int ID { get { return _ID; } }
   public float priority { get => _priority; }
   public int playerID { get { return _playerID; } }
   public CardAbility ability { get { return _ability; } }
   public string description;

   private static Dictionary<int, Card> CardsRegistry = new Dictionary<int, Card>();


   [ShowInInspector]
   private int _ID;
   [ShowInInspector]
   private int _playerID;

   private CardAbility abilities;

   public CardDisplayer Displayer { get => displayer; }
   [ShowInInspector]
   private CardDisplayer displayer;
   private CardAbility _ability;
   [ShowInInspector]
   private CardData _data;
   [ShowInInspector]
   private float _priority;


    public Card(CardData data, int playerID)
   {

      this.foodPrice = data.foodPrice;
      this.industryPrice = data.industryPrice;
      this.MagicPrice = data.MagicPrice;
      this.hitpoint = data.creatureData.hitpoint;
      this.speed = data.creatureData.speed;
      this.armor = data.creatureData.armor;
      this.attack = data.creatureData.attack;
      this._priority = data.priority;
      this.description = data.description;

      _playerID = playerID;

      this._data = data;
      _ID = IDFactory.GetUniqueID();

      string abilityName = data.abilityScriptName;
      if (abilityName != null && abilityName != "")
      {
         _ability = System.Activator.CreateInstance(System.Type.GetType(abilityName)) as CardAbility;
         _ability.Register(ID);
      }
      Card.CardsRegistry.Add(ID, this);
   }

   internal Pile GetPile()
   {
      if(CardsMannager.Instance.discardPile.Has(this)){
         return CardsMannager.Instance.discardPile;
      }
      if(CardsMannager.Instance.drawPile.Has(this)){
         return CardsMannager.Instance.drawPile;
      }
      if(CardsMannager.Instance.exilePile.Has(this)){
         return CardsMannager.Instance.exilePile;
      }
      return null;
   }

   public static Card Copy(Card original, int playerID)
   {
      return new Card(original.data, playerID);
   }
   public static Card GetCard(int ID)
   {
      return Card.CardsRegistry[ID];
   }

   public static Card Replace(Card original, string newCardName)
   {
      Card newCard = Card.BuildCard(newCardName, original.playerID);

      return Replace(original , newCard);
   }

   public static Card Replace(Card original, Card newCard)
   {

      if(original == null || newCard == null){
         Debug.LogError("Can't replace a card with null and vice versa");
         return null;
      }

      original._data = newCard.data;

      original.foodPrice = newCard.foodPrice;
      original.industryPrice = newCard.industryPrice;
      original.MagicPrice = newCard.MagicPrice;
      original.hitpoint = newCard.hitpoint;
      original.speed = newCard.speed;
      original.armor = newCard.armor;
      original.attack = newCard.attack;
      original._priority = newCard.priority;
      original.description = newCard.description;

      CardDisplayer displayer = CardDisplayer.GetDisplayer(original.ID);
      if(displayer != null){
         displayer.Reload();
      }


      return newCard;
   }

   public static bool CardExist(int ID)
   {
      return CardsRegistry.ContainsKey(ID);
   }
   internal static Card BuildCard(string cardName, int playerID)
   {
      if (cardName != null && cardName != "")
      {
         CardData data = Resources.Load<CardData>($"Data/Cards/{cardName}");
         if (data == null)
         {
               Debug.LogError($"Can't find a card data Resources/Cards/{cardName}");
               return null;
         }
         Card card = new Card(data, playerID);
         return card;
      }
      else
      {
         Debug.LogError($"Can't find a card with the name of {cardName}");
         return null;
      }
   }

   public override string ToString()
   {
      return _data.cardName;
   }

}
