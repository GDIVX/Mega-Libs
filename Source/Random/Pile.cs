using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Pile 
{
    public int Size{get{return cards.Count;}}
    public PileType pileType;
    [ShowInInspector]
    Stack<Card> cards;
    public Action<Pile> OnValueChange;

public Pile(PileType type){
        cards = new Stack<Card>();
        this.pileType = type;
        OnValueChange?.Invoke(this);
    }

public Pile(Stack<Card> cards , PileType type){
    this.cards = cards;
    this.pileType = type;
    Shuffle();

    OnValueChange?.Invoke(this);
    }

    public Card Draw(){
        
        if(cards.Count != 0)
        {
            OnValueChange?.Invoke(this);
            return cards.Pop();
        }
        else{
            if(CardsMannager.Instance.discardPile.IsEmpty()){
                Prompt.ToastCenter("<color=red><b>No More Cards to draw!</color><b>" , 1.5f , 30);
                OnValueChange?.Invoke(this);
                return null;
            }
            CardsMannager.Instance.ReformPiles();
            OnValueChange?.Invoke(this);
            return CardsMannager.Instance.drawPile.Draw();
        }
    }

    public void Drop(Card card){
        if(cards == null){
            cards = new Stack<Card>();
        }

        cards.Push(card);
        OnValueChange?.Invoke(this);
    }    

    public void Shuffle()
    {
        Card[] arr = cards.ToArray();

        //shuffle the pile as normal
        for(int i = arr.Length - 1; i > 0; i-- ){
            float relativePosition = 1 - arr[i].priority;
            int max = Mathf.RoundToInt((arr.Length - 1) * relativePosition);

            int rand = 0;
            if(max <= i){
                rand = UnityEngine.Random.Range(max, i);
            }
            else{
                rand = UnityEngine.Random.Range(i , max);
            }

            Card temp = arr[i];
            arr[i] = arr[rand];
            arr[rand] = temp;
        }

        //replace this deck with the new array
        cards = new Stack<Card>(arr);
    }

    internal bool IsEmpty()
    {
        return cards.Count == 0;
    }

    internal void TransferFrom(Pile otherPile)
    {
        for (int i = 0; i < otherPile.Size; i++)
        {
            cards.Push(otherPile.cards.Pop());
        }

        OnValueChange?.Invoke(this);
        otherPile.OnValueChange?.Invoke(otherPile);
    }

    public bool Has(Card card){
        return cards.Contains(card);
    }

    internal void Remove(Card card)
    {
        List<Card> list = new List<Card>(cards);
        list.Remove(card);
        cards = new Stack<Card>(list);
        OnValueChange?.Invoke(this);
    }
    public enum PileType
    {
        Draw, Discard , Exile
    }
}
