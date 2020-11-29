using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to steer the cohesion between agents together

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
    public class SteeredCohesionBehavior : FilteredFlockBehavior
    {
        [Header("Cohesion Calibration")]
        public float AgentSmoothTime = 0.5f;
        private Vector2 currentVelocity;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            // if no neighbours, return no adjustment
            if (!context.Any())
            {
                return Vector2.zero;
            }

            // all add points together and average
            Vector2 cohesionMove = Vector2.zero;

            // if (filter == null) { filteredContext = context} else {filter.Filter(agent,context)}
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);
            int count = 0;

            foreach (var item in filteredContext)
            {
                // instead of context
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
            cohesionMove -= (Vector2)agent.transform.position;
            cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, AgentSmoothTime);
            return cohesionMove;
        }
    }
}
