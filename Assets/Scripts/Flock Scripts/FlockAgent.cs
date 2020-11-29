using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Behaviour

{
    // gameobject must have Collider2D attached
    [RequireComponent(typeof(Collider2D))]
    public class FlockAgent : MonoBehaviour
    {
        //future proofing for inheritance
        // reference to parentflock
        public Flock ParentFlock;
        //reference to agent colliders
        public Collider2D AgentCollider;
        //reference to animator
        private Animator animator;

        //create instance of flock
        public void Initialize(Flock flock)
        {
            ParentFlock = flock;
        }

        //references to agent collider and animator
        private void Start()
        {
            AgentCollider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }
        // handle transitions within animator if distance conditions are met
        private void Update()
        {
            if (animator != null)
            {
                animator.SetFloat("Minimal distance to agent in flock", GetMinimalDistanceToAgentInFlock());
                animator.SetFloat("Minimal distance to agent in another flock", GetMinimalDistanceToAgentInAnotherFlock());
            }
        }
        //method for basic movement based on position over time
        public void Move(Vector2 velocity)
        {
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        //method to update movement position over time
        public void UpdatePosition(Vector2 velocity)
        {
            if (velocity.sqrMagnitude > ParentFlock.AgentMaxSpeed)
            {
                velocity = velocity.normalized * ParentFlock.AgentMaxSpeed;
            }

            transform.up = (Vector3)velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        //method to create list of all nearby object positions excluding agents 
        //return object collider to list in radius
        public List<Transform> GetNearbyObjectsByRadius(float radius)
        {
            return Physics2D.OverlapCircleAll(transform.position, radius).Where(x => x != AgentCollider).Select(x => x.transform).ToList();
        }
        //method to calculate distance to agents assgined to same flock
        private float GetMinimalDistanceToAgentInFlock()
        {
            //create local list and call nearby objects function
            //lamba expression to add flockagents to list
            List<Transform> nearbyObjects = GetNearbyObjectsByRadius(ParentFlock.NeighborRadius).Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock == ParentFlock).ToList();
            //if found return nearby objects with distance to position
            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }
            //return neighbor radius
            return ParentFlock.NeighborRadius;
        }
        //method to calculate distance to agents assgined to different flock
        private float GetMinimalDistanceToAgentInAnotherFlock()
        {
            //create local list and call nearby objects function
            //lamba expression to add flockagents to list
            List<Transform> nearbyObjects = GetNearbyObjectsByRadius(ParentFlock.NeighborRadius).Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock != ParentFlock).ToList();
            //if found return nearby objects with distance to position
            if (nearbyObjects.Any())
            {
                return nearbyObjects.Select(obj => Vector2.Distance(obj.position, transform.position)).Min();
            }
            //return neighbor radius
            return ParentFlock.NeighborRadius;
        }
    }
}
