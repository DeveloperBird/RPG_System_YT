using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject {

    public string questName;
    [TextArea(5, 10)]
    public string questDescription;

    public int[] CurrentAmount { get; set; }
    public int[] RequiredAmount { get; set; }

    public bool IsCompleted { get; set; }

    public CharacterProfile NPCTurnIn;
    public DialogueBase completedQuestDialogue;

    [System.Serializable]
    public class Rewards
    {
        public Item[] itemRewards;
        public int experienceReward;
        public int goldReward;
    }

    public Rewards rewards;

    public virtual void InitializeQuest()
    {
        IsCompleted = false;
        CurrentAmount = new int[RequiredAmount.Length];
        QuestLogManager.instance.AddQuestToLog(this);
    }

    public void Evaluate()
    {
        for(int i = 0; i < RequiredAmount.Length; i++)
        {
            if(CurrentAmount[i] < RequiredAmount[i])
            {
                //quest is not complete return!
                return;
            }
        }

        for(int i = 0; i < GameManager.instance.allDialogueTriggers.Length; i++)
        {
            if(GameManager.instance.allDialogueTriggers[i].targetNPC == NPCTurnIn)
            {
                GameManager.instance.allDialogueTriggers[i].HasCompletedQuest = true;
                GameManager.instance.allDialogueTriggers[i].CompletedQuestDialogue = completedQuestDialogue;
                break;
            }
        }

        IsCompleted = true;
        DialogueManager.instance.CompletedQuest = this;
    }

    public virtual string GetObjectiveList()
    {
        return null;
    }
}
