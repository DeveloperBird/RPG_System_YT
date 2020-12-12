using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour
{

    [HideInInspector] public UnityEvent eventHandler;
    [HideInInspector] public DialogueBase myDialogue;

    public void InitiateAction()
    {
        StartCoroutine(InDialogueBuffer());
    }

    IEnumerator InDialogueBuffer()
    {
        yield return new WaitForSeconds(0.01f);
        eventHandler.Invoke();
        DialogueManager.instance.CloseOptions();
        DialogueManager.instance.inDialogue = false;

        if (myDialogue != null)
        {
            DialogueManager.instance.EnqueueDialogue(myDialogue);
        }
    }
}
