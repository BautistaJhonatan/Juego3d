using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Inventory/New DataBase", order = 1)]
public class DataBase : ScriptableObject
{
    [System.Serializable]
    public struct InvetoryItem
    {
        public string Name;
        public int ID;
        public Sprite Icon;
        public Type type ;
        public bool acumulate;
        public int maxStack;
        public string description;
        public BaseItem item;

    }
    public enum Type
    {
        consumable
    }
    public InvetoryItem[] dataBase;

    private void OnValidate()
    {
        if (dataBase!=null)
        {
            for (int i = 0; i < dataBase.Length; i++)
            {
                if (dataBase[i].ID!=i)
                {
                    dataBase[i].ID = 1;
                }
            }
        }
    }
}
