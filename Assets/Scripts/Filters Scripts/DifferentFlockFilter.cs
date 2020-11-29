using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to create a filtered list of agents that will be different from currently assigned flock agent list

namespace Assets.Scripts.Behaviour.FilterScripts
{
    [CreateAssetMenu(menuName = "Flock/Filter/Different Flock")]
    public class DifferentFlockFilter : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            return original.Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock != agent.ParentFlock).ToList();
        }
    }
}
