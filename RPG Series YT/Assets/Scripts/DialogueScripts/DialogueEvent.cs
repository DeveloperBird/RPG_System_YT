using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DialogueTrigger))]
public class DialogueEvent : Interactable {

    [Header("Target Info")]
    public int targetIndex;
    public int targetLine;

    public UnityEvent unityEvent;

    private DialogueTrigger dialogueTrigger;

    private bool hasAdded;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        interactRange = dialogueTrigger.interactRange;
    }

    public override void Interact()
    {
        if (hasAdded || DialogueManager.instance.inDialogue) return;

        if (dialogueTrigger.index == targetIndex)
        {
            DialogueManager.instance.onDialogueLineCallBack += GenericEvent;
            hasAdded = true;
        }

        base.Interact();
    }

    public void GenericEvent(int line)
    {
        if(line == targetLine)
        {
            unityEvent.Invoke();

            DialogueManager.instance.onDialogueLineCallBack -= GenericEvent;
        }
    }




}
