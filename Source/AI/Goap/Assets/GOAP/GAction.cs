using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP{

    public abstract class GAction : MonoBehaviour
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public GameObject target;
        public string targetTag;
        public float duration = 0;
        public State[] preConditions;
        public State[] afterEffects;
        public NavMeshAgent agent;
        public GInventory inventory; 
        public WorldStates beliefs;

        public Dictionary<string, int> preconditions;
        public Dictionary<string, int> effects;

        public WorldStates agentBeliefs;

        public bool running = false;

        public GAction()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        public void Awake()
        {
            agent = this.gameObject.GetComponent<NavMeshAgent>();

            if (preConditions != null)
                foreach (State w in preConditions)
                {
                    preconditions.Add(w.key, w.value);
                }

            if (afterEffects != null)
                foreach (State w in afterEffects)
                {
                    effects.Add(w.key, w.value);
                }
                
            inventory = gameObject.GetComponent<GAgent>().inventory;
            beliefs = gameObject.GetComponent<GAgent>().beliefs;
        }


        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> p in preconditions)
            {
                if (!conditions.ContainsKey(p.Key))
                    return false;
            }
            return true;
        }
        public abstract bool IsAchievable();
        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }

}