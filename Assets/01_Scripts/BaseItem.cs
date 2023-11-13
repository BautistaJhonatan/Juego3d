using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
[System.Serializable]
public abstract class BaseItem : MonoBehaviour
{
    public int id;
    public int quantity = 1;

    public DataBase.InvetoryItem itemData;

    // Start is called before the first frame update
    void Start()
    {
        SetDataById(id, quantity);
    }


    public void SetDataById(int id ,int quantity = 1)
    {
        itemData.ID = id;
        itemData.acumulate = Inventory.Instance.db.dataBase[id].acumulate;
        itemData.description = Inventory.Instance.db.dataBase[id].description;
        itemData.Icon = Inventory.Instance.db.dataBase[id].Icon;
        itemData.Name = Inventory.Instance.db.dataBase[id].Name;
        itemData.type = Inventory.Instance.db.dataBase[id].type;
        itemData.maxStack = Inventory.Instance.db.dataBase[id].maxStack;
        itemData.item = Inventory.Instance.db.dataBase[id].item;

        this.quantity = quantity;
    }
    public abstract void Use();

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(id, quantity);
            Destroy(this.gameObject);
        }
    }
}
