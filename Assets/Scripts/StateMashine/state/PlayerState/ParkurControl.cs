using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ParkurControl : Istate
{
    private NavMeshAgent _agent;
    private CharacterController _contriler;
    private Transform _transform;
    private const float _maxDistanceRay = 3f;
    private const float _sphereRadius = 1f;
    private LayerMask _layerMask= LayerMask.GetMask("Parkur");
    private LayerMask _graundLayer = LayerMask.GetMask("Graund");
    private NavMeshLink _link;
    private Animator _animator;
    private Player _player;
    private static readonly int Speed = Animator.StringToHash("Speed");


    public ParkurControl(NavMeshAgent agent, CharacterController _characterController, 
                         Transform _transform,NavMeshLink meshLink,Animator _animator,
                         Player _player)
    {
        this._agent = agent;
        _contriler = _characterController;
        this._transform = _transform;
        _link = meshLink;
        this._animator = _animator;
        this._player = _player;
    }
    public void Enter()
    {
        _agent.enabled = true;
        _contriler.enabled = false;


    }

    public void Exit()
    {
        _agent.enabled = false;
        _contriler.enabled = true;
    }

    

   public void Update()
    {   Vector3 velocity = Direction();

         _agent.Move(velocity);
       
      



        _animator.SetFloat(Speed, velocity.normalized.magnitude);
        
        Ray ray = new Ray(_transform.position, _transform.forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
                RaycastHit hit;
           
            if( Physics.SphereCast(ray, radius: _sphereRadius, out hit, maxDistance: _maxDistanceRay,_layerMask))
            {
              
                _link.startPoint = _transform.position;

                NavMesh.SamplePosition(hit.transform.position, out NavMeshHit Point, maxDistance: 1, NavMesh.AllAreas);
               
 

                 _link.endPoint = Point.position;
                _agent.SetDestination(Point.position);
            }
            else if (Physics.Raycast(_transform.position,-_transform.up,2f,_graundLayer))
            {
              
                _player.ChangeState(PlayerStateMashine.Name.Normal);
            }

          
        }
          
        if (Input.GetKeyUp(KeyCode.Space))
        {
                _agent.ResetPath();

        }
       
;              
        
    }

   public void OnTriggerEnter(Collider other)
   {
       
   }

   public void OnTriggerExit(Collider other)
   {
   }


   public Vector3 Direction()
    {
        
         var i =  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if( i.magnitude > 0.3)
        {
            Quaternion toRotation = Quaternion.LookRotation(i, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, Time.deltaTime * 1000f);
        }
        return (i*Time.deltaTime);
    }

   
    
}
