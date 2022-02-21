using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemsInventory : MonoBehaviour
{
    [System.Serializable] public struct QuestItem
    {
        public string name;
        public bool isFinded;
    }
    
    [SerializeField] private GameObject[] _items;

    public QuestItem[] questItemsArray = new QuestItem[15];

    private void Awake()
    {
        questItemsArray[0].name = "Water Bottle";
        questItemsArray[1].name = "Fire Lamp";
        questItemsArray[2].name = "Earth Sword";
        questItemsArray[3].name = "Air dagger";
        questItemsArray[4].name = "Bible";
        questItemsArray[5].name = "Cross";
        questItemsArray[6].name = "Match";
        
        //Letters
        questItemsArray[7].name = "Paper1";
        questItemsArray[8].name = "Paper2";
        questItemsArray[9].name = "Paper3";
        questItemsArray[10].name = "Paper4";
        questItemsArray[11].name = "Paper5";
        questItemsArray[12].name = "Paper6";
        questItemsArray[13].name = "Paper7";
        questItemsArray[14].name = "Paper8";

        for (int i = 0; i < questItemsArray.Length; i++) 
            questItemsArray[i].isFinded = true;
    }
    
    [PunRPC]
    public void AddItem(int ID)
    {
        questItemsArray[ID].isFinded = true;
        
        _items[ID].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public bool HaveElementalItems()
    {
        if (questItemsArray[0].isFinded && questItemsArray[1].isFinded && questItemsArray[2].isFinded && questItemsArray[3].isFinded)
            return true;
        
        return false;
    }
}
