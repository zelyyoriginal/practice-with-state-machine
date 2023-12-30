using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMashine
{
    public enum Name
    {
        WithoutControl,
        Normal,
        Fight,
        Parkur

    }

    private Dictionary<Name, Istate> _state;
    private Istate _curentState;
  

    public PlayerStateMashine(CharacterController _controler, Animator _animator,
                              Transform _enemy,Transform _myTransform,NavMeshAgent _agent,
                              NavMeshLink meshLink,Rigidbody _rb,Player _player)
    {
        _state =  new Dictionary<Name, Istate>
        {           

                    [Name.WithoutControl] = new WithoutControl(),
                    [Name.Normal] = new NormalControl(_controler,_animator,_rb,this),
                  //  [Name.Fight]  = new FightControl(_enemy, _controler, _animator,_myTransform),
                    [Name.Parkur] = new ParkurControl(_agent,_controler, _myTransform,meshLink,_animator,_player),

        };

      
    }




    public void ChangeState(Name i)
    {
        _curentState?.Exit();
        _curentState = _state[i];
        _curentState.Enter();


    }

   public void TriggerEnter(Collider other)
    {
        _curentState.OnTriggerEnter(other);
    }

   public void OnTriggerExit(Collider other)
   {
       _curentState.OnTriggerExit(other);
   }
    public void NewFight(Transform enemy, Transform player)
    {
        _state[Name.Fight] = new FightControl(enemy, player,this);
    }

    public void Update()
    {
        _curentState.Update();
    }
}

