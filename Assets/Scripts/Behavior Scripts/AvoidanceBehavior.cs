using System.Collections.Generic;
using UnityEngine;

//script for agents to avoid objects

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
    public class AvoidanceBehavior : FilteredFlockBehavior
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // getting average 
            Vector2 avoidanceMove = Vector2.zero;
            int numAvoid = 0;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);

            foreach (Transform item in filteredContext)
            {
                // instead of context, using filter
                if (Vector2.Distance(item.position, agent.transform.position) <= agent.ParentFlock.AvoidanceRadius)
                {
                    numAvoid++;
                    avoidanceMove += (Vector2)(agent.transform.position - item.position);
                }
            }

            if (numAvoid > 0)
            {
                avoidanceMove /= numAvoid;
            }

            return avoidanceMove;
        }
    }
}