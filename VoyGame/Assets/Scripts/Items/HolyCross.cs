using UnityEngine;

public class HolyCross : MonoBehaviour
{
    private void Start() => 
        gameObject.layer = LayerMask.NameToLayer("Hand Item");

    private void OnTriggerEnter(Collider other) => 
        Zoning(other, true);

    private void OnTriggerExit(Collider other) => 
        Zoning(other, false);

    private void Zoning(Collider enemy, bool status)
    {
        if(!enemy.CompareTag("Slimster"))
            return;
        
        enemy.GetComponent<Slimster>().animator.SetBool("isReversed", status);
    }
}
