using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Create a GUI of a card with active controls by the player. Can call for the card ability.
/// </summary>

public class CardDisplayer : CardGUI ,IClickable
{
    public Action OnUpdateDisplayerListener;
    private static Dictionary<int, CardDisplayer> CardDisplayerRegestry = new Dictionary<int, CardDisplayer>();


    public static new GameObject Create(){
        GameObject _gameObject = CardGUI.Create();
        _gameObject.AddComponent<CardDisplayer>();
        return _gameObject;
    }

    public override void SetID(int ID){
        if(IsIDTaken(ID)){
            Debug.LogError($"Trying to assign a card with ID {ID} to more then one inspector");
            base.SetID(0);
            return;
        }
        base.SetID(ID);
        CardDisplayer.CardDisplayerRegestry[ID] = this;
    }


    public override void Clear(){
        CardDisplayer.CardDisplayerRegestry[_ID] = null;
        base.Clear();
    }

    internal static CardDisplayer GetDisplayer(int ID)
    {
        return CardDisplayerRegestry[ID];
    }

    public void OnLeftClick()
    {
        IClickable CurrentSelectedID = GameManager.CurrentSelected;
        if((object)CurrentSelectedID != this && CurrentSelectedID != null){
            //something else is selected
            //clear selection
            CurrentSelectedID.OnDeselect();
        }

        //Select this displayer
        OnSelect();
    }

    public void OnRightClick(){}

    public void OnSelect()
    {
        if(GameManager.CurrentSelected == this) {return;}

        //Move the card forward to mark him as selected
        LeanTween.scale(gameObject ,new Vector3(1.2f,1.2f,1.2f) , .2f);
        transform.LeanMoveLocalY(50 , .5f).setEase(LeanTweenType.easeInExpo);

        UIController.Instance.handGUI.RearrangeCards(
            GetComponent<RectTransform>().rect.width*.8f);
        transform.SetAsLastSibling();

        //Show availble actions on the board
        var interactions = Card.GetCard(ID).ability.GetBoardInteractions();
        WorldController.Instance.overlayController.PaintTheMap(interactions);

        GameManager.CurrentSelected = this;
    }

    public void OnDeselect()
    {
        if(GameManager.CurrentSelected != this) {return;}

        GameManager.CurrentSelected = null;
        UIController.Instance.handGUI.RearrangeCards();
        WorldController.Instance.overlayController.Clear();
        
        //if the card no longer exist (was used) there is not displayer to manipulate
        if(!Card.CardExist(ID)) return;

        UIController.Instance.handGUI.ArrangeCard(ID);
        LeanTween.scale(gameObject , new Vector3(1,1,1), .2f);
        transform.LeanMoveLocalY(0 , .5f).setEase(LeanTweenType.easeInOutSine);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnLeftClick();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        var CurrentSelected = GameManager.CurrentSelected as CardDisplayer;
        if(CurrentSelected != null) return;

        LeanTween.scale(gameObject ,new Vector3(1.2f,1.2f,1.2f) , .2f);
        UIController.Instance.handGUI.RearrangeCards(
            GetComponent<RectTransform>().rect.width*.8f);
        transform.SetAsLastSibling();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        var CurrentSelected = GameManager.CurrentSelected as CardDisplayer;
        if(CurrentSelected != null) return;

        UIController.Instance.handGUI.RearrangeCards();
        UIController.Instance.handGUI.ArrangeCard(ID);
        LeanTween.scale(gameObject , new Vector3(1,1,1), .2f);
    }
    private bool IsIDTaken(int ID)
    {
        if(!CardDisplayerRegestry.ContainsKey(ID)){
            return false;
        }
        return CardDisplayerRegestry[ID] != null;
    }
}
