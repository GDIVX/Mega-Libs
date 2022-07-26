using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("patientWaiting", 1);
        GWorld.Instance.Add(gameObject , "patient");
        beliefs.ModifyState("atHospital" , 1);
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
