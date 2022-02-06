using UnityEngine;

public class QuestItemsInventory : MonoBehaviour
{
    [System.Serializable] public struct QuestItem
    {
        public string name;
        public bool isFinded;
    }
    
    [SerializeField] private GameObject[] _items;

    public QuestItem[] questItemsArray = new QuestItem[7];

    private void Awake()
    {
        questItemsArray[0].name = "Water Bottle";
        questItemsArray[1].name = "Fire Lamp";
        questItemsArray[2].name = "Earth Sword";
        questItemsArray[3].name = "Air dagger";
        questItemsArray[4].name = "Bible";
        questItemsArray[5].name = "Cross";
        questItemsArray[6].name = "Match";

        for (int i = 0; i < questItemsArray.Length; i++) 
            questItemsArray[i].isFinded = true;
    }

    public void AddItem(int ID)
    {
        questItemsArray[ID].isFinded = true;
        _items[ID].SetActive(true);
    }

    public bool HaveElementalItems()
    {
        if (questItemsArray[0].isFinded && questItemsArray[1].isFinded && questItemsArray[2].isFinded && questItemsArray[3].isFinded)
            return true;
        
        return false;
    }
}
