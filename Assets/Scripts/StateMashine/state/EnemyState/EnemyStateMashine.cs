using System.Collections.Generic;
using DefaultNamespace.StateMashine.state.EnemyState;
using UnityEngine;
using UnityEngine.AI;




    public  class EnemyStateMashine
    {
        public enum Name
        {
            Patrol,
            Fight,
            die

        };

        protected Istate _curentState;
        protected Dictionary<Name, Istate> _state;


        public EnemyStateMashine(NavMeshAgent _agent,Transform MyZone, GameObject Ragdoll)
        {
            _state = new Dictionary<Name, Istate>()
            {
                [Name.Patrol] = new PatrolEnemyState(_agent,this),
                [Name.die]    = new DieEnemyState(MyZone, _agent,Ragdoll)
                

            };


        }

        public void ChangeStateName(Name name)
        {
            _curentState?.Exit();
            _curentState = _state[name];
            _curentState.Enter();

        }

        public void NewFight(GameObject player, NavMeshAgent agent)
        {
            _state[Name.Fight] = new FightEnemyState(player, agent);
        }

        public void Update()
        {
            _curentState.Update();
        }


        public void TrigerEnter(Collider other)
        {
            _curentState.OnTriggerEnter(other);
        }
    }


