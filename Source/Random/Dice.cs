using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice 
{
    /// <summary>
    /// Roll a group of 10 sided dices. 
    /// </summary>
    /// <param name="numberOfDices">The amount of dices to roll</param>
    /// <returns>A random number int from a normal distribution</returns>
    public static int Roll(int numberOfDices , int bias = 0){
        if(numberOfDices <= 0){return 0;}
        int results = 0;

        for (var i = 1; i <= numberOfDices; i++)
        {
            int randomValue = Mathf.RoundToInt(Random.Range(1,10));

            if(bias > 0){
                if(bias > randomValue){
                    randomValue = Random.Range(randomValue , bias);
                }
                else{
                    randomValue = Random.Range(bias , randomValue);
                }
            }

            results += randomValue;
        }

        return results;
    }
}
