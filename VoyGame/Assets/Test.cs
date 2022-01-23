using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _telep;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("pressed");
            _player.GetComponent<CharacterController>().enabled = false;
            _player.GetComponent<CharacterController>().enabled = true;
            _player.transform.position = _telep.transform.position;
            
        }
    }
}
