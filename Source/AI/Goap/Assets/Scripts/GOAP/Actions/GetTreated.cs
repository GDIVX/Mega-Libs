using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class GetTreated : GAction
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
        GWorld.Instance.GetWorld().ModifyState("isTreated", 1);
        inventory.RemoveItem(target);
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
