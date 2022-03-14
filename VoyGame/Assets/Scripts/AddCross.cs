using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AddCross : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    [SerializeField] private GameObject _crossObject;

    private void Start() =>
        GlobalEventManager.OnBiblePut.AddListener(() =>
        {
            _rig.weight = 1f;
            _crossObject.SetActive(true);
        });
}
