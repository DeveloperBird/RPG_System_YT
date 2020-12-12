using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable {

    [Header("THIS NPC")]
    public CharacterProfile targetNPC;

    [Header("Basic Dialogues Info")]
    public DialogueBase[] DB;
    [HideInInspector] public int index;
    public bool nextDialogueOnInteract;

    public bool HasCompletedQuest { get; set; }
    public DialogueBase CompletedQuestDialogue { get; set; }

    public override void Interact()
    {
        if (!DialogueManager.instance.inDialogue)
        {
            if (HasCompletedQuest)
            {
                DialogueManager.instance.EnqueueDialogue(CompletedQuestDialogue);
                DialogueManager.instance.CompletedQuestReady = true;
                HasCompletedQuest = false;
                return;
            }
        }

        if (nextDialogueOnInteract && !DialogueManager.instance.inDialogue)
        {
            DialogueManager.instance.EnqueueDialogue(DB[index]);

            if (index < DB.Length - 1)
            {
                index++;
            }
        }
        else
        {
            DialogueManager.instance.EnqueueDialogue(DB[index]);
        }
    }

    public void SetIndex(int i)
    {
        index = i;
    }

}
