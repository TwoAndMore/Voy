using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuForms : MonoBehaviour
{
   public TextMeshProUGUI text1;
   public TextMeshProUGUI text2;
   public Image image1;

   public void SetText(string text) => 
      text1.text = text;

   public void SetColor(Color32 color) => 
      image1.color = color;

   public void SetRoomValues(string nameStr, string playerCount)
   {
      text1.text = nameStr;
      text2.text = playerCount;
   }
}
