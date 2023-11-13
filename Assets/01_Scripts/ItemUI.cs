using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private DataBase db;

    [SerializeField]
    private GameObject deleteButton;

    public int id;
    public int quantity;

    [HideInInspector]
    public DataBase.InvetoryItem itemData;
    [HideInInspector]
    public Transform exParent;

    TextMeshProUGUI quantityText;
    Image iconImage;
    Vector3 DragOffset;

    private void Awake()
    {
        quantityText = transform.GetComponentInChildren<TextMeshProUGUI>();
        iconImage = GetComponent<Image>();

        exParent = transform.parent;
        if (exParent.GetComponent<Image>()) 
            exParent.GetComponent<Image>().fillCenter = true;

        InitializeItem(id, quantity);

    }

    // Update is called once per frame
    void Update()
    {
        if (quantityText !=null)
        {
            quantityText.text = quantity.ToString();
        }
    }
    public void InitializeItem(int id ,int quantity)
    {
        itemData.ID = id;
        itemData.acumulate = db.dataBase[id].acumulate;
        itemData.description = db.dataBase[id].description;
        itemData.Icon = db.dataBase[id].Icon;
        itemData.Name = db.dataBase[id].Name;
        itemData.type = db.dataBase[id].type;
        itemData.maxStack = db.dataBase[id].maxStack;
        itemData.item = db.dataBase[id].item;

        deleteButton.SetActive(false);
        iconImage.sprite = itemData.Icon;

        this.quantity = quantity;

    }
    public void EnableDeletion(bool enable)
    {
        deleteButton.SetActive(enable);
    }
    public void Delete()
    {
        Inventory.Instance.HideDescription();
        if (quantity>1)
            Inventory.Instance.ShowDeletionPrompt(this);
        else
            Inventory.Instance.DeleteItem(this, 1, false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            Inventory.Instance.ShowDeletionPrompt(this);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Inventory.Instance.HideDescription();
            itemData.item.Use();
            Inventory.Instance.DeleteItem(this, 1, true);

        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.HideDescription();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Inventory.Instance.HideDescription();
        quantityText.enabled = false;
        exParent = transform.parent;
        exParent.GetComponent<Image>().fillCenter = false;
        transform.SetParent(Inventory.Instance.transform);
        DragOffset = transform.position - Input.mousePosition;

    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + DragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        quantityText.enabled = true;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        Transform slot = null;

        Inventory.Instance.graphRay.Raycast(eventData,raycastResults);

        foreach (RaycastResult hit in raycastResults)
        {
            var hitObj = hit.gameObject;

            if (hitObj.CompareTag("Slot")&& hit.gameObject.transform.childCount == 0)
            {
                slot =hit.gameObject.transform;
                break;
            }
            if (hitObj.CompareTag("Item_UI"))
            {
                if (hitObj!= this.gameObject)
                {
                    ItemUI hitobjItemData = hitObj.GetComponent<ItemUI>();
                    if (hitobjItemData.itemData.ID!=id)
                    {
                        slot = hitobjItemData.transform.parent;
                        Inventory.Instance.UpdateParent(hitobjItemData,exParent);
                        break;
                    }
                    else
                    {
                        if (itemData.acumulate && hitobjItemData.quantity +quantity <=itemData.maxStack)
                        {
                            quantity += hitobjItemData.quantity;
                            slot = hitobjItemData.transform.parent;
                            Inventory.Instance.DeleteItem(hitobjItemData, hitobjItemData.quantity, true);
                            break;
                        }
                        else
                        {
                            slot = hitobjItemData.transform.parent;
                            Inventory.Instance.UpdateParent(hitobjItemData, exParent);
                            break;
                        }
                    }
                }
            }
            Inventory.Instance.UpdateParent(this, slot != null ? slot : exParent);
        }
    }

    
}
