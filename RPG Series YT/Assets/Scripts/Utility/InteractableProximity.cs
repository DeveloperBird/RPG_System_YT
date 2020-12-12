using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableProximity : MonoBehaviour {

    public float interactRange = 2;

    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, GameManager.instance.player.position) < interactRange)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        //code
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

}
