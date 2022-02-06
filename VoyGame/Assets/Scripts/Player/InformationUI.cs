using UnityEngine;

public class InformationUI : MonoBehaviour
{
    private const KeyCode OPENINFO = KeyCode.Tab;

    [SerializeField] private GameObject _informationMenu;
    private void Update() => _informationMenu.SetActive(Input.GetKey(OPENINFO));
}
