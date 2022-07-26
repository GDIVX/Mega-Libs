using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GOAP{
    public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Dictionary<string , Queue<GameObject>> atlas;
    

    static GWorld()
    {
        world = new WorldStates();
        atlas = new Dictionary<string, Queue<GameObject>>();
    }

    private GWorld()
    {

    }

    public void Add(GameObject gameObject , string name){
        if(!atlas.ContainsKey(name)){
            atlas.Add(name , new Queue<GameObject>());
            
        }
        atlas[name].Enqueue(gameObject);
    }

    public GameObject Pull(string name){
        if(!atlas.ContainsKey(name)){
            atlas.Add(name , new Queue<GameObject>());
            return null;
        }
        if(atlas[name].Count <= 0){
            return null;
        }

        return atlas[name].Dequeue();
    }


    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}

}