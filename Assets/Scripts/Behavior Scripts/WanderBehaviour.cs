using System.Collections.Generic;
using UnityEngine;

//method to generate a random path for agent to wander

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Wander")]
    public class WanderBehaviour : FilteredFlockBehavior
    {
        private Path path = null;
        private int currentWaypoint = 0;
        private Vector2 waypointDirection = Vector2.zero;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            if (path == null)
            {
                FindPath(agent, context);
            }

            return FollowPath(agent); // return class variable
        }

        //method to check alignment on path
        public bool InRadius(FlockAgent agent)
        {
            waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

            return waypointDirection.magnitude < path.radius;
        }

        //method for agent to follow found path
        public Vector2 FollowPath(FlockAgent agent)
        {
            if (path == null)
            {
                return Vector2.zero;
            }

            if (InRadius(agent))
            {
                currentWaypoint++; // go to next waypoint

                if (currentWaypoint >= path.waypoints.Count)
                {
                    // if at last waypoint
                    currentWaypoint = 0; // reset waypoint
                }

                return Vector2.zero; // dont have to move if at waypoint already
            }

            return waypointDirection; // return class variable
        }

        //method for agent to find path
        public void FindPath(FlockAgent agent, List<Transform> context)
        {
            List<Transform> filteredContext = Filter == null ? context : Filter.Filter(agent, context);

            if (filteredContext.Count == 0)
            {
                return;
            }

            int randomPath = Random.Range(0, filteredContext.Count);
            path = filteredContext[randomPath].GetComponentInParent<Path>();
        }

        
    }
}
