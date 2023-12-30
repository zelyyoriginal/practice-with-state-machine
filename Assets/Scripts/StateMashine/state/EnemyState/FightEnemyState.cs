
using System;
using UnityEngine;
using UnityEngine.AI;

public  class FightEnemyState : Istate
{
    private const float _atackRenge = 1.5f;
    private const float _pursuitDistanse = 8f;
    private static readonly int Atack = Animator.StringToHash("Atack");
    
    private Enemy _enemy;
    private GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    public FightEnemyState(GameObject player, NavMeshAgent agent)
        {
            _player = player;
            _agent = agent;
            _enemy = agent.gameObject.GetComponent<Enemy>();
            
            
            _animator = agent.gameObject.GetComponent<Animator>();
        }
        public void Enter()
        {
            _agent.stoppingDistance = _atackRenge;
            _animator.SetBool("IsFight",true);
        }

        public void Exit()
        {
            _agent.stoppingDistance = 0f;
            _animator.SetBool("IsFight",false);
        }

        public void Update()
        {
            _agent.SetDestination(_player.transform.position);
            _animator.SetFloat("Speed",_agent.speed);
            if (IsPlayerCloser(_atackRenge))
            {
                _animator.SetTrigger(Atack);
            }
            
            else if(!IsPlayerCloser(_pursuitDistanse))

            {
                _enemy.ChangeState(EnemyStateMashine.Name.Patrol);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnTriggerExit(Collider other)
        {
        }

        private bool IsPlayerCloser(float distanse)
        {

            float Curentdistanse = Vector3.Distance(_agent.transform.position, _player.transform.position);
            return Curentdistanse <= distanse;
        }
}
