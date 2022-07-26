using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using Assets.Scripts.Mechanics.Systems.Players;

[System.Serializable]
public class TurnSequenceMannager 
{
    public Action<Turn> OnTurnStart , OnTurnComplete;
    public Turn currentTurn;    

    public void Init(Player player){
        currentTurn = new Turn(player);
        currentTurn.Start();
        OnTurnStart?.Invoke(currentTurn);
    }

    public void StartNextTurn()
    {
        if(GameEventMannager.isPlayingEvent){
            GameEventMannager.onAnyEventDone += StartNextTurn;
            return;
        }

        if(currentTurn != null && currentTurn.IsActive){
            Debug.LogWarning("Trying to start a new turn without ending the current one");
            return;
        }

        currentTurn = SetCurrentTurn();
        currentTurn.Start();
        OnTurnStart?.Invoke(currentTurn);
    }

    void EndTurn(){
        if(currentTurn == null || !currentTurn.IsActive) return;
        currentTurn.End();
        OnTurnComplete?.Invoke(currentTurn);
    }

    public void NextTurn(){
        EndTurn();
        StartNextTurn();
    }
    private int NewCardsEventDaysCount()
    {
        return (GameManager.Instance.level * 2) + 1;
    }

    Turn SetCurrentTurn(){
        if(currentTurn.player.IsMain()){
            return new Turn(Player.Rival);     
        }
        return new Turn(Player.Main);
    }
}

public class Turn{
    public Player player;
    private bool isActive = true;

    public bool IsActive { get => isActive;}

    public void End(){
        isActive = false;
        player.OnTurnEnd();
    }

    public void Start(){
        isActive = true;
        player.OnTurnStart();
    }


    public Turn(Player player){
        this.player = player;
    }

}