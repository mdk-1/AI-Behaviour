using Assets.Scripts.Behaviour;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to calibrate the use of flocking compisite behaviors on FSM in animator
//using unity state machine behaviour

[CreateAssetMenu(menuName = "Flock/FSMBehavior/Flocking")]
public class CompositeStateBehavior : StateMachineBehaviour
{
    //class for behaviour groups
    [System.Serializable]
    public class BehaviourGroup
    {
        public FlockBehavior Behaviour;
        public float Weight;
    }
    //list of behaviour groups
    public List<BehaviourGroup> Behaviours;
    //reference to flockagent
    private FlockAgent flockAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //declare flockagent
        flockAgent = animator.gameObject.GetComponent<FlockAgent>();

        //create a new instance of behaviour for each state behaviour
        foreach (BehaviourGroup behaviorGroup in Behaviours)
        {
            if (behaviorGroup.Behaviour != null)
            {
                behaviorGroup.Behaviour = Instantiate(behaviorGroup.Behaviour);
            }
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //determine neighbours, weight and position and update local
        List<Transform> nearbyObjects = flockAgent.GetNearbyObjectsByRadius(flockAgent.ParentFlock.NeighborRadius);
        float totalWeight = Behaviours.Sum(behaviorGroup => behaviorGroup.Weight);
        Vector2 newVelocity = Vector2.zero;
        //calcuate movement for each flockagent
        foreach (BehaviourGroup behaviorGroup in Behaviours)
        {
            Vector2 partialVelocity = behaviorGroup.Behaviour.CalculateMoveSpeed(flockAgent, nearbyObjects).normalized;
            partialVelocity *= flockAgent.ParentFlock.DriveFactor;
            newVelocity += (partialVelocity * behaviorGroup.Weight) / totalWeight;
        }
        // flockAgent.Velocity += Vector2.Lerp(flockAgent.Velocity, newVelocity, newVelocity.magnitude / flockAgent.ParentFlock.NeighborRadius);
        // flockAgent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, nearbyAgents.Count / 6f); // closer to 1 is white, closer to 6 is red+++++++++
        flockAgent.UpdatePosition(newVelocity);
    }
}