using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdDice
{
    public int sides = 10;
    public int threshold = 7;
    public bool criticalSuccsus = true;
    public bool criticalFaliure = true;
    public bool criticalOveride = true;


    public int[] RollDiceArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = Roll();
        }

        return array;
    }

    public int Roll()
    {
        return Random.Range(1, sides + 1);
    }

    public ThresholdDice.RollBreakdown DiceCheck(int rating , int ResultModifier = 0)
    {
        var arr = RollDiceArray(rating);
        var res = GetDiceCheckResults(arr ,ResultModifier);
        return new ThresholdDice.RollBreakdown(arr, res, rating);
    }


    int GetDiceCheckResults(int[] rolls , int modifier)
    {
        System.Array.Sort(rolls);
        var highest = rolls[rolls.Length - 1];
        var lowest = rolls[0];

        if(lowest == 1){
            if(highest == sides && criticalOveride){
                return 1;
            }
            if(criticalFaliure)
                return 0;
        }
        if(highest >= threshold){
            if(highest == sides && criticalSuccsus){
                return 2 + modifier;
            }
            return 1 + modifier;
        }
        return modifier;
    }

    public class RollBreakdown
    {

        public int[] diceRollArray { get; private set; }
        public int Result { get; private set; }
        public int rating { get; private set; }

        public RollBreakdown(int[] diceRollArray, int result, int rating)
        {
            this.diceRollArray = diceRollArray;
            Result = result;
            this.rating = rating;
        }

    }
}
