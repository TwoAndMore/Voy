using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 1f;

    private float xRotation;
    
    private void Awake() => 
        Cursor.lockState = CursorLockMode.Locked;

    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        
        if(PauseMenu.isPaused)
            return;
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
