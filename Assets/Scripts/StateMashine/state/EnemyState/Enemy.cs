using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject Zone;
    [SerializeField] private GameObject _ragdoll;
    private CombatZone _myZone;
    private EnemyStateMashine _sm;
    public event Action NewEnemyState;

    private void Start()
    {
       SetZone();

        _sm = new EnemyStateMashine(_agent,_myZone.transform, _ragdoll);
        ChangeState(EnemyStateMashine.Name.Patrol);
    }

    private void Update()
    {
        _sm.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        _sm.TrigerEnter(other);
        
        if (other.gameObject.CompareTag("Weapon"))
        {
            ChangeState(EnemyStateMashine.Name.die);
        }
        
    }

    public void ChangeState(EnemyStateMashine.Name name)
    {
          NewEnemyState?.Invoke();
          _sm.ChangeStateName(name);
    }

    private void SetZone()
    {
        GameObject i = Instantiate(Zone, transform.position, Quaternion.identity);

        _myZone = i.GetComponent<CombatZone>();
        _myZone.enter += OnTriggerEnter;
        _myZone.enemy = this.transform;
    }

   
    
}
