using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{
    public Sprite icon;
    public Image image;
    public Vector2 position => new Vector2(transform.position.x, transform.position.z);
}
