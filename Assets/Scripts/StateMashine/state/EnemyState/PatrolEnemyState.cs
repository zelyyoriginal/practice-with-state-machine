using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyState : Istate
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyStateMashine _myStateMashine;

    private  List<Vector3> _patrolPath= new List<Vector3>();
    private int _currentPath = 0;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public PatrolEnemyState(NavMeshAgent agent,EnemyStateMashine stateMashine)
    {
        _agent = agent;
        _animator = agent.GetComponent<Animator>();
        _myStateMashine = stateMashine;
    }

    public void Enter()
    {
        GetPatrolPath(_agent.transform.position);
        _agent.autoBraking = false;

        _agent.SetDestination(_patrolPath[_currentPath]);
    }

    public void Exit()
    {
       
       _animator.SetFloat(Speed,0);
    }

    public void Update()
    {
        if (_agent.remainingDistance < 0.1f && !_agent.pathPending)
        {
            // Переход к следующей точке в массиве
            _currentPath = (_currentPath + 1) % _patrolPath.Count;
            
            _agent.SetDestination(_patrolPath[_currentPath]);
        }  
        _animator.SetFloat(Speed,_agent.speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _myStateMashine.NewFight(other.gameObject,_agent);
            _myStateMashine.ChangeStateName(EnemyStateMashine.Name.Fight);
        }
    }

    public void OnTriggerExit(Collider other)
    {
    }

    private void GetPatrolPath(Vector3 position)
    {

        for (int i = 0; i < 10; i++)
        {
            _patrolPath.Add(new Vector3(
                position.x+ UnityEngine.Random.Range(-3,5),
                position.y,
                position.z + UnityEngine.Random.Range(-3, 5)
            ));
        }
    }
}
