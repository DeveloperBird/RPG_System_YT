using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        allDialogueTriggers = FindObjectsOfType<DialogueTrigger>();
    }

    public DialogueTrigger[] allDialogueTriggers;

    public Transform player;

    public delegate void OnEnemyDeathCallBack(EnemyProfile enemyProfile);
    public OnEnemyDeathCallBack onEnemyDeathCallBack;

    public RewardManager rewardManager;

    public Text objectiveText;
    public Animator objectiveAnim;

    public void UpdateTracker(string newText)
    {
        objectiveText.text = newText;
        objectiveAnim.Play("ObjectivePopUp");
    }

}
