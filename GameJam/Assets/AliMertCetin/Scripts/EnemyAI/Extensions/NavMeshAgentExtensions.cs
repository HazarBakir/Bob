using UnityEngine;
using UnityEngine.AI;

namespace AliMertCetin.Scripts.EnemyAI.Extensions
{
    public static class NavMeshAgentExtensions
    {
        public static bool IsReachedDestination(this NavMeshAgent agent)
        {
            return agent.pathPending == false &&
                   agent.remainingDistance <= agent.stoppingDistance &&
                   (agent.hasPath == false || agent.velocity.sqrMagnitude < Mathf.Epsilon);
        }
    }
}