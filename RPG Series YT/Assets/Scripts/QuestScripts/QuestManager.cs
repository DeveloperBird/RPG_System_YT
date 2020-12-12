using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    #region Quest Singleton
    public static QuestManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject questUI;
    public Text questName;
    public Text questDescription;
    public Button questAcceptButton;

    public QuestBase CurrentQuest { get; set; }
    public QuestDialogueTrigger CurrentQuestDialogueTrigger { get; set; }
    public bool InQuestUI { get; set; }

    public void SetQuestUI(QuestBase newQuest)
    {
        InQuestUI = true;
        CurrentQuest = newQuest;
        questUI.SetActive(true);
        questName.text = newQuest.questName;
        questDescription.text = newQuest.questDescription;
        questAcceptButton.Select();
    }

    public void StartQuestBuffer()
    {
        StartCoroutine(QuestBuffer());
    }

    private IEnumerator QuestBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        InQuestUI = false;
    }

}
