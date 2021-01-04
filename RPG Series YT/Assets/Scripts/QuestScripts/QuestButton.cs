using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [HideInInspector] public QuestBase myQuest;
    public Text questNameText;
    public Image uncheckBox;
    public Sprite checkedBox;

    public void SetQuest(QuestBase newQuest)
    {
        myQuest = newQuest;
        questNameText.text = newQuest.questName;
    }

    public void OnPressed()
    {
        QuestLogManager.instance.UpdateQuestUI(myQuest, myQuest.GetObjectiveList());
    }

    private void OnEnable()
    {
        if (myQuest.IsCompleted)
        {
            uncheckBox.sprite = checkedBox;
        }
    }
}
