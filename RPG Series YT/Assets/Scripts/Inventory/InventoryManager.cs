using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance;

    public List<Item> inventory = new List<Item>();

    public delegate void OnItemAddCallBack(Item item);
    public OnItemAddCallBack onItemAddCallBack;

    public delegate void OnItemRemoveCallBack(Item item);
    public OnItemRemoveCallBack onItemRemoveCallBack;

    //references
    public GameObject inventoryUI;
    public Button firstButton;
    public Text itemNameTooltip;
    public Text itemDescriptionTooltip;
    public ItemPopUp itemPopUp;

    [HideInInspector] public bool inInventory;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddItem(Item newItem)
    {
        inventory.Add(newItem);
        if (onItemAddCallBack != null) onItemAddCallBack.Invoke(newItem);
    }

    public void RemoveItem(Item newItem)
    {
        inventory.Remove(newItem);
        if (onItemRemoveCallBack != null) onItemRemoveCallBack.Invoke(newItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            if(inventoryUI.activeSelf == true)
            {
                firstButton.Select();
                inInventory = true;
            }
            else
            {
                inInventory = false;
            }
        }
    }

}
