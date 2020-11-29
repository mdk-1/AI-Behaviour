using System.Collections.Generic;
using UnityEngine;

//script to create a waypoint path for agents to follow

namespace Assets.Scripts.Behaviour
{
    public class Path : MonoBehaviour
    {
        [Header("Waypoint Path Cailbration")]
        public List<Transform> waypoints;
        public float radius;
        public float stayInRadiusPrecent = 0.9f;

        [SerializeField]
        private Vector3 gizmoSize = Vector3.one;

        private void OnDrawGizmos()
        {
            if (waypoints == null || waypoints.Count == 0)
            {
                return; // exit method
            }

            for (int i = 0; i < waypoints.Count; i++)
            {
                Transform waypoint = waypoints[i];

                if (waypoint == null)
                {
                    continue; // go to next interation of this loop (if 1 is 0, i becomes 1 ect)
                }
                //debug tool to add colour squares to waypoints
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(waypoint.position, gizmoSize);

                if (i + 1 < waypoints.Count && waypoints[i + 1] != null)
                {
                    // check if waypoint is valid
                    Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position); // draw line to next waypoint
                }
            }
        }
    }
}
