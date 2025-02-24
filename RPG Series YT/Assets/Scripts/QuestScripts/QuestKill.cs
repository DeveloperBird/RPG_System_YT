using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kill Quest", menuName = "Quests/Kill Quest")]
public class QuestKill : QuestBase {

    [System.Serializable]
    public class Objectives
    {
        public EnemyProfile requiredEnemy;
        public int requiredAmount;
    }

    public Objectives[] objectives;

    public override void InitializeQuest()
    {
        RequiredAmount = new int[objectives.Length];

        for(int i = 0; i < objectives.Length; i++)
        {
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        GameManager.instance.onEnemyDeathCallBack += EnemyDeath;
        base.InitializeQuest();
    }

    private void EnemyDeath(EnemyProfile slainEnemy)
    {
        for(int i = 0; i < objectives.Length; i++)
        {
            if(slainEnemy == objectives[i].requiredEnemy)
            {
                CurrentAmount[i]++;
                GameManager.instance.UpdateTracker($"You've slain {CurrentAmount[i] + "/" + RequiredAmount[i] + " " + slainEnemy.enemyName}");
            }
        }

        Evaluate();
    }

    public override string GetObjectiveList()
    {
        string tempObjectiveList = "";

        for (int i = 0; i < objectives.Length; i++)
        {
            tempObjectiveList += $"You've slain {CurrentAmount[i]} / {RequiredAmount[i]} {objectives[i].requiredEnemy.enemyName}s \n";
        }

        return tempObjectiveList;
    }
}
