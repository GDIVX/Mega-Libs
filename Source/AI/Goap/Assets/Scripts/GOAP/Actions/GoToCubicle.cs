using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GoToCubicle : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null)
            return false;
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("TreatingPatient", 1);
        GWorld.Instance.Add(target ,"cubicles");
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle" , 1);
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
