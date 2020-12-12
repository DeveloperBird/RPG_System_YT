using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TestScript : MonoBehaviour {

    public DialogueBase dialogue;
    public Item item;
    public QuestBase quest;

    private void Start()
    {
        //quest.InitializeQuest();
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TriggerDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            InventoryManager.instance.AddItem(item);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //InventoryManager.instance.RemoveItem(item);
        }
    }


}
