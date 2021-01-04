using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rbody;
    public float speed;
    public Animator anim;

    

    public bool CheckFreeze()
    {
        bool isFrozen;

        if (DialogueManager.instance.inDialogue)
        {
            isFrozen = true;
        }
        else if (InventoryManager.instance.inInventory)
        {
            isFrozen = true;
        }
        else if (InventoryManager.instance.itemPopUp.InPopUp)
        {
            isFrozen = true;
        }
        else if (QuestManager.instance.InQuestUI)
        {
            isFrozen = true;
        }
        else if (QuestLogManager.instance.InQuestLog)
        {
            isFrozen = true;
        }
        else
        {
            isFrozen = false;
        }

        return isFrozen;
    }

    private void Update()
    {

        if (CheckFreeze()) return;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!anim.GetBool("isAttacking"))
        {
            if (movement != Vector2.zero)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", movement.x);
                anim.SetFloat("input_y", movement.y);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("isAttacking", true);
        }

        rbody.MovePosition(rbody.position + movement * Time.deltaTime * speed);
    }

}
