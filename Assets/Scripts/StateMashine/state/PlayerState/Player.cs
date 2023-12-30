using System;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]private CharacterController characterController;
    [SerializeField]private Animator _animator;
    [SerializeField]private Transform _enemy;
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private NavMeshLink _link;
    [SerializeField]private Rigidbody _rb;
    

    private Enemy _curentEnemy;
    
    private PlayerStateMashine _sm;

    void Start()
    {    
        _sm = new PlayerStateMashine(characterController,_animator,_enemy,this.transform,_agent,_link,_rb,this);
        ChangeState(PlayerStateMashine.Name.Normal);
       
        
    }

    void Update()
    {   
       _sm.Update();
    }
   
    public void ChangeState(PlayerStateMashine.Name name)
    {
        _sm.ChangeState(name);

    }


    private void OnTriggerEnter(Collider other)
    {
        _sm.TriggerEnter(other);
    }

    public void OnTriggerExit(Collider other)
    {
        _sm.OnTriggerExit(other);
    }
    
}
