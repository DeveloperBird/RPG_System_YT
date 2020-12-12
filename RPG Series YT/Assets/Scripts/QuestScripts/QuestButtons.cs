using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtons : MonoBehaviour {

    public void QuestAccept()
    {
        QuestManager.instance.CurrentQuestDialogueTrigger.hasActiveQuest = false;
        QuestManager.instance.CurrentQuest.InitializeQuest();
        QuestManager.instance.StartQuestBuffer();
        QuestManager.instance.questUI.SetActive(false);

        GameManager.instance.UpdateTracker($"You've accepted {QuestManager.instance.CurrentQuest.questName}");
    }

    public void QuestDecline()
    {
        QuestManager.instance.StartQuestBuffer();
        QuestManager.instance.questUI.SetActive(false);
    }

}
