using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteract : Interactable {

    public UnityEvent events;

    public override void Interact()
    {
        if (events != null) events.Invoke();
        base.Interact();
    }

}
