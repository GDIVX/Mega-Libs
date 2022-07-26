using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Patient : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Goal goal_waiting = new Goal("isWaiting", 1, true);
        goals.Add(goal_waiting, 3);

        Goal goal_treated = new Goal("isTreated", 1, true);
        goals.Add(goal_treated, 5);

        Goal goal_home = new Goal("atHome" , 1, true);
        goals.Add(goal_home , 3);
    }

}
