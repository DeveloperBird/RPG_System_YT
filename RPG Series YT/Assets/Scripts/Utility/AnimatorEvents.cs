using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour {

    public void SetGameObjectOff()
    {
        gameObject.SetActive(false);
    }

    public void ResetDamage()
    {
        BaseEnemy BE = gameObject.transform.parent.GetComponent<BaseEnemy>();
        BE.damageAmountText = 0;
    }

}
