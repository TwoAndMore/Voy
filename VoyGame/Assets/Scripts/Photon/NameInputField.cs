using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    private const string PLAYERNAMEPREFKEY = "PlayerName";
    
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField inputField = GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(PLAYERNAMEPREFKEY))
            {
                defaultName = PlayerPrefs.GetString(PLAYERNAMEPREFKEY);
                inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }
    private void Update()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = GetComponent<TMP_InputField>().text.Length > 2;
            Image buttonImage = button.gameObject.GetComponent<Image>();
            if(button.interactable)
                buttonImage.color = new Color32(255, 255, 255, 255);
            else
                buttonImage.color = new Color32(255, 255, 255, 120);
        }
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;
        
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(PLAYERNAMEPREFKEY, value);
    }
}
