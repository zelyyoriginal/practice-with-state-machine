
using System;
using DefaultNamespace;
using UnityEngine;

public class NormalControl : Istate
{
    private CharacterController _playerController;
    private float _run;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private PlayerStateMashine _playerStateMashine;
    private bool _isCanParkur = false;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public NormalControl(CharacterController playerController,Animator animator,Rigidbody _rb, PlayerStateMashine stateMashine)
    { 
        this._playerController = playerController;
        this._animator = animator;
        _rigidbody = _rb;
        _playerStateMashine = stateMashine;
    }

    public void Enter()
    {
       
    }

    public void Exit()
    {
       
        _animator.SetFloat(Speed, 0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isCanParkur) 
        {
            _playerStateMashine.ChangeState(PlayerStateMashine.Name.Parkur);
        
        }
        Vector3 moveDirection = GetMoveDirection();
        _playerController.Move(moveDirection+Vector3.down);
        Rotate(_animator, moveDirection);

        _animator.SetFloat(Speed, _playerController.velocity.magnitude);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag: "Parkur"))
        {
            _isCanParkur = true;
        }

        if (other.gameObject.CompareTag(tag: "CombatZone"))
        {
             var Target=  other.GetComponent<CombatZone>().enemy;
            _playerStateMashine.NewFight(Target,_playerController.transform);
            _playerStateMashine.ChangeState(PlayerStateMashine.Name.Fight);
            
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Parkur"))
        {
            _isCanParkur = false;
        }
    }

    private void Rotate(object animator, Vector3 moveDirection)
    {

        if (_playerController.velocity.magnitude > 0.3)
        {
           Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
          _playerController.transform.rotation = Quaternion.RotateTowards(_playerController.transform.rotation, toRotation, Time.deltaTime * 1000f);
        }
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Time.deltaTime;

        float i = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        _run = Mathf.Lerp(_run, i, Time.deltaTime);
        return moveDirection*_run;
    }
}
