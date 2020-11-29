using System.Collections.Generic;
using UnityEngine;

//script to chase agents in another flock within distance

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Pursuit")]
    public class PursuitBehavior : FilteredFlockBehavior
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            //create list of filtered agents
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);

            if (filteredContext.Count == 0)
            {
                return Vector2.zero;
            }

            Vector2 move = Vector2.zero;

            //foreach filtered agent in list caluclate disance and move direction
            foreach (Transform item in filteredContext)
            {
                float distance = Vector2.Distance(item.position, agent.transform.position);
                float disancePercent = distance / agent.ParentFlock.NeighborRadius;
                float inverseDistancePercent = 1 - disancePercent;
                float weight = inverseDistancePercent / filteredContext.Count;

                Vector2 direction = (item.position - agent.transform.position) * weight;

                move += direction;
            }
            //return movement as direction
            return move;
        }
    }
}
