using System.Collections.Generic;
using UnityEngine;

//script to keep agent within a defined radius

namespace Assets.Scripts.Behaviour.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
    public class StayInRadius : FlockBehavior
    {
        [Header("Radius Calibration")]
        [SerializeField]
        private Vector2 center = Vector2.zero;
        [SerializeField]
        private float radius = 15;

        //method to override the calcuation of the movement speed and position of agent
        public override Vector2 CalculateMoveSpeed(FlockAgent agent, List<Transform> context)
        {
            // direction to the center
            // magnitude will = distance
            Vector2 centerOffset = center - (Vector2)agent.transform.position;
            float centerOffsetMagnitude = centerOffset.magnitude / radius;

            if (centerOffsetMagnitude < 0.9f)
            {
                return Vector2.zero;
            }

            return centerOffset * centerOffsetMagnitude * centerOffsetMagnitude;
        }
    }
}
