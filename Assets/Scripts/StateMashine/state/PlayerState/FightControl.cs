using System;
using UnityEngine;

public class FightControl : Istate
{
    private Transform _curentEnemy;
    private Transform _player;
    private CharacterController _controler;
    private Animator _animator;
    private PlayerStateMashine _myStateMashine;
    private Action myDelegate;
    
    private float speed= 1000f;
    private float _run;
    private int _isFight = Animator.StringToHash("Fight");
    public FightControl (Transform curentEnemy, Transform _player,PlayerStateMashine stateMashine)
    {
        this._controler = _player.GetComponent<CharacterController>();
        this._animator = _player.GetComponent<Animator>();
        _myStateMashine = stateMashine;
        
        _curentEnemy = curentEnemy;
        this._player = _player;
    }
    
    
    public void Enter()
    {
       _animator.SetBool(_isFight,true);
       
       myDelegate = () => _myStateMashine.ChangeState(PlayerStateMashine.Name.Normal);
       _curentEnemy.GetComponent<Enemy>().NewEnemyState += myDelegate;
    }
    
    

    public void Exit()
    {
        _animator.SetBool(_isFight, false);
        _curentEnemy.GetComponent<Enemy>().NewEnemyState -= myDelegate;
    }

    public void Update()
    {
        _player.rotation = RotateToEnemy();
        _controler.Move(GetMoveDirection());
        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetTrigger("Atack");
        }
        SetAnimation();

    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    private void SetAnimation()
    {
        Vector3 velosity = _player.InverseTransformDirection(_controler.velocity);
        _animator.SetFloat("y", velosity.z);
        _animator.SetFloat("x", velosity.x);
    }

    private Quaternion RotateToEnemy()
    {
        Vector3 directionToEnemy = _curentEnemy.position - _player.position;
        directionToEnemy.y = 0;

      
      return  Quaternion.RotateTowards(_player.rotation, Quaternion.LookRotation(directionToEnemy, Vector3.up), Time.deltaTime * speed);
        
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = _player.forward * Input.GetAxis("Vertical")+ _player.right * Input.GetAxis("Horizontal");
        
        moveDirection = moveDirection * Time.deltaTime;

        
        
        float i = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        _run = Mathf.Lerp(_run, i, Time.deltaTime);

        return moveDirection*_run+Vector3.down;
    }
}