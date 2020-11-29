using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to add a filter list of all agents in same flock

namespace Assets.Scripts.Behaviour.FilterScripts
{
    [CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
    public class SameFlockFilter : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            return original.Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock == agent.ParentFlock).ToList();
        }
    }
}