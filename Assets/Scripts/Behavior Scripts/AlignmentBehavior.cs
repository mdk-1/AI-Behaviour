using System.Collections.Generic;
using UnityEngine;

//script to align agents in flock 

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
    public class AlignmentBehavior : FilteredFlockBehavior
    {
        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            if (context.Count == 0)
            {
                // maintain same direction
                return agent.transform.up;
            }

            // add all directions together and average
            Vector2 alignmentMove = Vector2.zero;
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);

            int count = 0;

            foreach (Transform item in filteredContext)
            {
                // instead of context, using filter
                if (Vector2.Distance(item.position, agent.transform.position) <= agent.ParentFlock.SmallRadius)
                {
                    alignmentMove += (Vector2)item.transform.up;
                    count++;
                }
            }

            if (count != 0)
            {
                alignmentMove /= context.Count;
            }

            return alignmentMove;
        }
    }
}