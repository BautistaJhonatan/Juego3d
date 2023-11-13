using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GraphicRaycaster graphRay;
    public DataBase db;
    public int slotsCount = 35;
    public bool isOpen;

    [SerializeField]
    public Player player;

    [SerializeField]
    private GameObject inventoryToggle;

    [SerializeField]
    private Transform slotPrefab;

    [SerializeField]
    private Transform itemPrefab;


    public DeletionPrompt deletionPront;
    public DescriptionUI descriptionUI;
    [SerializeField]
    private List<ItemUI> items = new List<ItemUI>();
    bool itemsDeleteModeEnabled;


    [SerializeField]
    private Transform slotsContainer;

    private List<Transform> slots = new List<Transform>();
    private ItemUI item;


    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            Transform newSlot = Instantiate(slotPrefab, slotsContainer);
            slots.Add(newSlot);
        }
        isOpen= true;
        ToggleInventory();


        AddItem(0, 1);
        AddItem(0, 1);
        AddItem(1, 4);

        
    }
    public void UpdateParent(ItemUI item , Transform newParent)
    {
        item.exParent = newParent;
        item.transform.SetParent(newParent);
        item.transform.parent.GetComponent<Image>().fillCenter = true;
        item.transform.localPosition = Vector3.zero;
        item.EnableDeletion(itemsDeleteModeEnabled);
    }
    public void AddItem(int id , int quantity)
    {
        ItemUI preexistenValidItem = items.Find(item => item.id == id && item.itemData.maxStack >= item.quantity +quantity);
        if (preexistenValidItem !=null)
        {
            preexistenValidItem.quantity += quantity;
            return;
        }
        for (int i = 0; i < slots.Count; i++)
        {
            ItemUI itemInSlot = slots[i].childCount == 0 ? null : slots[i].GetChild(0).GetComponent<ItemUI>();
            if (itemInSlot ==null)
            {
                ItemUI itemCopy = Instantiate(itemPrefab, transform).GetComponent<ItemUI>();
                itemCopy.InitializeItem(id, quantity);
                items.Add(itemCopy);

                UpdateParent(itemCopy, slots[i]);
                break;
            }else if (itemInSlot.id ==id && itemInSlot.itemData.maxStack >= itemInSlot.quantity + quantity)
            {
                itemInSlot.quantity += quantity;
                break;
            }   
        }

    }
    public void DeleteItem(ItemUI item, int quantity,bool byUse)
    {
        ItemUI itemToDelete = items.Find(it=> it ==item);
        itemToDelete.quantity -= quantity;

        if (!byUse)
        {
            Debug.Log(item.itemData.Name);
            BaseItem spawnedItem= Instantiate(item.itemData.item);
            spawnedItem.transform.position = player.itemSpawn.position;
            spawnedItem.SetDataById(item.id, quantity);
        }
        if (itemToDelete.quantity <=0)
        {
            itemToDelete.exParent.GetComponent<Image>().fillCenter = false;
            items.Remove(itemToDelete);
            Destroy(itemToDelete.gameObject);
        }
    }
    public void TooggleDeleteMode()
    {
        itemsDeleteModeEnabled = !itemsDeleteModeEnabled;
        foreach (ItemUI item in items)
        {
            item.EnableDeletion(itemsDeleteModeEnabled);
        }
    }
    public void ToggleInventory()
    {
        if (isOpen && itemsDeleteModeEnabled)
            TooggleDeleteMode();

            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            inventoryToggle.SetActive(!isOpen);
            isOpen = !isOpen;
        
    }
    public void ShowDescription(ItemUI item)
    {
        descriptionUI.gameObject.SetActive(true);
        descriptionUI.Show(item);
    }
    public void HideDescription()
    {
        descriptionUI.gameObject.SetActive(false);
    }
    public void ShowDeletionPrompt(ItemUI item)
    {
        deletionPront.gameObject.SetActive(true);
        deletionPront.SetSliderData(item);
    }
    public void AddMoreSpace(int slotsToAdd)
    {
        for (int i = 0; i < slotsToAdd; i++)
        {
            Transform newSlot = Instantiate(slotPrefab, slotsContainer);
            slots.Add(newSlot);
        }
    }
}



