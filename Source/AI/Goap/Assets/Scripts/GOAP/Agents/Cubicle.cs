using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;

public class Cubicle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GWorld.Instance.Add(gameObject , "cubicles");
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
    }
}
