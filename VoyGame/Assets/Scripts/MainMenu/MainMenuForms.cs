using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuForms : MonoBehaviour
{
   [SerializeField] private Button _button;
   [SerializeField] private TextMeshProUGUI _text2;
   [SerializeField] private Image _image;
   
   public TextMeshProUGUI text1;

   public void SetText(string text) => 
      text1.text = text;

   public void SetColor(Color32 color) => 
      _image.color = color;

   public void SetRoomValues(string name, string playerCount)
   {
      text1.text = name;
      _text2.text = playerCount;
   }
}
