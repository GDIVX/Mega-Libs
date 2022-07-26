using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Register : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
