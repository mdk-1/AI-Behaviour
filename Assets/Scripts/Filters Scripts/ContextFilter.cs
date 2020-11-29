using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Behaviour.FilterScripts
{
    public abstract class ContextFilter : ScriptableObject
    {
        public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
    }
}
