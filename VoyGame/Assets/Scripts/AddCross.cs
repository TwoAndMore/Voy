using UnityEngine;

public class AddCross : MonoBehaviour
{
    [SerializeField] private GameObject _crossObject;

    private void Start() =>
        GlobalEventManager.OnBiblePut.AddListener(() =>
        {
            _crossObject.SetActive(true);
        });
}
