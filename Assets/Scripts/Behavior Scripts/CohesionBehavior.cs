using System.Collections.Generic;
using UnityEngine;

//script to align flocks movements in cohesion

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
    public class CohesionBehavior : FilteredFlockBehavior
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            // add all points together and get average
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);
            int count = 0;

            foreach (var item in filteredContext)
            {
                // instead of context, using filter
                if (Vector2.Distance(item.position, agent.transform.position) <= agent.ParentFlock.SmallRadius)
                {
                    cohesionMove += (Vector2)item.position;
                    count++;
                }
            }

            if (count != 0)
            {
                cohesionMove /= count;
            }

            // create offset from agent position
            // direction from a to b = b - a
            cohesionMove -= (Vector2)agent.transform.position;
            return cohesionMove;
        }
    }
}