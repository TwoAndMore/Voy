using UnityEngine;

public class AddLetterUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _lettersImages;

    public void AddLetter(int id) => _lettersImages[id].SetActive(true);
}
