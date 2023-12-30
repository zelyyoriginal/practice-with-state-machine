using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.StateMashine.state.EnemyState
{
    public class DieEnemyState : Istate
    {
        private Transform _myZone;
        private NavMeshAgent _agent;
        private GameObject _ragdoll;

        public DieEnemyState(Transform MyZone, NavMeshAgent agent,GameObject Ragdol)
        {
            _myZone = MyZone;
            _agent = agent;
            _ragdoll = Ragdol;
        }
        public void Enter()
        {
           Object.Destroy(_myZone.gameObject);
           _agent.isStopped = true;
           Object.Instantiate(_ragdoll, _agent.transform.position, _agent.transform.rotation);
           Object.Destroy(_agent.gameObject);
        }

        public void Exit()
        { 
        }

        public void Update()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnTriggerExit(Collider other)
        {
        }
    }
}