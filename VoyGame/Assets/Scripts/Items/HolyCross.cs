using UnityEngine;

public class HolyCross : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => 
        Zoning(other, true);

    private void OnTriggerExit(Collider other) => 
        Zoning(other, false);

    private void Zoning(Collider enemy, bool status)
    {
        if(!enemy.CompareTag("Slimster"))
            return;
        enemy.transform.Find("Body").GetComponent<Animator>().SetBool("isReversed", status);
    }
}
