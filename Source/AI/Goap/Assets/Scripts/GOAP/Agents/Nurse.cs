using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Nurse : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Goal goal_treat = new Goal("treatPatient", 1, true);
        goals.Add(goal_treat, 3);

        Goal goal_rest = new Goal("isRested", 1, false);
        goals.Add(goal_rest, 1);

        Invoke("GetTired" , Random.Range(10,20));
    }

    void GetTired(){
        Debug.Log("!");
        beliefs.ModifyState("tired", 1);
        Invoke("GetTired" , Random.Range(10,20));
    }
}
