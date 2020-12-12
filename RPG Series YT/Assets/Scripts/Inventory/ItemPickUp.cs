using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {

    public Item item;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.myIcon;
    }

    public override void Interact()
    {
        //InventoryManager.instance.AddItem(item);
        InventoryManager.instance.itemPopUp.PopUpItem(item);
        Destroy(gameObject);
        base.Interact();
    }

}
