using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Rest : GAction
{
        public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("tired");
        return true;
    }

    public override bool IsAchievable()
    {
        return true;
    }
}
