using Assets.Scripts.Behaviour.FilterScripts;

using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public abstract class FilteredFlockBehavior : FlockBehavior
    {
        [SerializeField]
        public ContextFilter Filter;
    }
}