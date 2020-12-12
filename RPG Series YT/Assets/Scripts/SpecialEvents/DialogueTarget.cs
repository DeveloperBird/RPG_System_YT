using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTarget : Interactable {

    public DialogueTrigger DT;

    public override void Interact()
    {
        DT.index = 1;

        base.Interact();
    }

}
