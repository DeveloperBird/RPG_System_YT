using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUp : MonoBehaviour {

    public Image itemIcon;
    public Text itemDesc;
    public Text itemName;
    public Animator myAnim;
    public bool InPopUp { get; set; }

    private bool buffer;

    public void PopUpItem(Item newItem)
    {
        StartCoroutine(Buffer());

        myAnim.SetBool("FadeIn", true);

        itemName.text = newItem.myName;
        itemName.color = newItem.GetQualityColor();
        itemDesc.text = newItem.myDescription;
        itemIcon.sprite = newItem.myIcon;

        InventoryManager.instance.AddItem(newItem);
        InPopUp = true;
    }

    private void Update()
    {
        if(buffer == true) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(itemIcon.gameObject.activeSelf == true)
            {
                myAnim.SetBool("FadeIn", false);
                InPopUp = false;
            }
        }
    }

    private IEnumerator Buffer()
    {
        buffer = true;
        yield return new WaitForSeconds(0.1f);
        buffer = false;
    }

}
