using Unity.VisualScripting;
using UnityEngine;

public interface Istate 
{

    
    public void Enter();

    public void Exit();
    public void Update();
    public void OnTriggerEnter(Collider other);
    public void OnTriggerExit(Collider other);

}


   
