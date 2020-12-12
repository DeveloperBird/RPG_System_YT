using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour {

    private float enemyHealth;
    private Text damageTextUI;
    [HideInInspector] public float damageAmountText;
    private Animator damageAnimator;

    public float EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
        set
        {
            enemyHealth = value;
            if(enemyHealth <= 0)
            {
                Death();
            }
        }
    }

    public EnemyProfile enemyProfile;

    private void Start()
    {
        enemyHealth = 20;
        gameObject.tag = "Enemy";
        damageTextUI = GetComponentInChildren<Text>();
        damageAnimator = damageTextUI.transform.parent.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        StopAllCoroutines();

        damageAnimator.Play("Pop");
        EnemyHealth -= damage;
        damageAmountText += damage;
        damageTextUI.text = damageAmountText.ToString();

        if (!gameObject.activeSelf) return;
        StartCoroutine(DamageTimeOut());
    }

    IEnumerator DamageTimeOut()
    {
        yield return new WaitForSeconds(1f);
        damageAnimator.Play("FadeOut");
    }

    public void Death()
    {
        if (GameManager.instance.onEnemyDeathCallBack != null) GameManager.instance.onEnemyDeathCallBack.Invoke(enemyProfile);

        gameObject.SetActive(false);
    }
}
