using System;
using UnityEngine;

namespace DefaultNamespace
{ 
    public class CombatZone : MonoBehaviour
    {
        public event Action<Collider> enter;
        public Transform enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                enter?.Invoke(other);
               
            }
        }

        private void OnDestroy()
        {
            enter = null;
        }
    }
    
}