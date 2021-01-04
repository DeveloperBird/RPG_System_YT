using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogManager : MonoBehaviour
{
    public static QuestLogManager instance;

    public Text questDescription;
    public Text questName;
    public Transform questHolder;
    public GameObject questButtonPrefab;
    public GameObject questLogUI;
    public Text objectiveText;
    public Text npcTurnInText;
    public Image[] rewardIcons;
    public Text goldText;
    public Text expText;
    public bool InQuestLog { get; set; } 

    private QuestBase lastDisplayedQuest;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpdateQuestUI(QuestBase newQuest, string objectiveList)
    {
        lastDisplayedQuest = newQuest;

        questName.text = newQuest.questName;
        questDescription.text = newQuest.questDescription;
        npcTurnInText.text = "Turn into " + newQuest.NPCTurnIn.myName;
        objectiveText.text = objectiveList;
        goldText.text = "Gold: " + newQuest.rewards.goldReward.ToString();
        expText.text = "Exp " + newQuest.rewards.experienceReward.ToString();

        for (int i = 0; i < rewardIcons.Length; i++)
        {
            try
            {
                rewardIcons[i].gameObject.SetActive(true);
                rewardIcons[i].sprite = newQuest.rewards.itemRewards[i].myIcon;
            }
            catch
            {
                rewardIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void AddQuestToLog(QuestBase newQuest)
    {
        var questButton = Instantiate(questButtonPrefab, questHolder);

        questButton.GetComponent<QuestButton>().SetQuest(newQuest);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            questLogUI.SetActive(!questLogUI.activeSelf);

            if (questLogUI.activeSelf)
            {
                InQuestLog = true;

                try
                {
                    var firstButton = questHolder.GetChild(0).GetComponent<Button>();
                    firstButton.Select();
                    UpdateQuestUI(lastDisplayedQuest, lastDisplayedQuest.GetObjectiveList());
                }
                catch
                {
                    return;
                }
            }
            else
            {
                InQuestLog = false;
            }
        }

    }

}
