using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public Text questName;
    public Image[] questRewardIcons;
    public GameObject questRewardUI;
    public Button claimButton;

    public bool InQuestReward { get; set; }

    public void SetRewardUI(QuestBase quest)
    {
        InQuestReward = true;

        questRewardUI.SetActive(true);

        claimButton.Select();

        questName.text = quest.questName;

        for(int i = 0; i < quest.rewards.itemRewards.Length; i++)
        {
            questRewardIcons[i].gameObject.SetActive(true);
            questRewardIcons[i].sprite = quest.rewards.itemRewards[i].myIcon;
        }
    }

    public void Claim()
    {
        QuestBase currentQuest = DialogueManager.instance.CompletedQuest;

        for (int i = 0; i < currentQuest.rewards.itemRewards.Length; i++)
        {
            InventoryManager.instance.AddItem(currentQuest.rewards.itemRewards[i]);
        }

        questRewardUI.SetActive(false);

        for (int i = 0; i < questRewardIcons.Length; i++)
        {
            questRewardIcons[i].gameObject.SetActive(true);
        }

        StartCoroutine(QuestRewardBuffer());
    }

    IEnumerator QuestRewardBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        InQuestReward = false;
    }
}
