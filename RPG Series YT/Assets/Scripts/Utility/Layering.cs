using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layering : InteractableProximity {

    SpriteRenderer spriteRenderer;

    public float offset;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        if(transform.position.y + offset < GameManager.instance.player.transform.position.y)
        {
            spriteRenderer.sortingLayerName = "ForeGround";
        }
        else
        {
            spriteRenderer.sortingLayerName = "BackGround";
        }


        base.Interact();
    }


}
