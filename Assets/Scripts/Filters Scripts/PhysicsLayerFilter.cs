using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to add physcis layer to filter 

namespace Assets.Scripts.Behaviour.FilterScripts
{
    [CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
    public class PhysicsLayerFilter : ContextFilter
    {
        public LayerMask Mask;

        //method to use bit shift to determine layer and add to list
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            return original.Where(x => Mask == (Mask | (1 << x.gameObject.layer))).ToList();
        }
    }
}