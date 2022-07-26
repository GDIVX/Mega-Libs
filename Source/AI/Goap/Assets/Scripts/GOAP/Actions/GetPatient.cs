using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class GetPatient : GAction
{
    GameObject resource;
    public override bool PrePerform()
    {
        target = GWorld.Instance.Pull("patient");
        if (target == null)
            return false;

        resource = GWorld.Instance.Pull("cubicles");
        if (resource != null)
            inventory.AddItem(resource);
        else
        {
            GWorld.Instance.Add(gameObject , "patient");
            target = null;
            return false;
        }

        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
            target.GetComponent<GAgent>().inventory.AddItem(resource);
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
