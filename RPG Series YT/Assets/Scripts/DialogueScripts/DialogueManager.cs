using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private readonly List<char> puncutationCharacters = new List<char>
    {
        '.',
        ',',
        '!',
        '?'
    };


    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }

    }


    public GameObject dialogueBox;

    public Text dialogueName;
    public Text dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo; //FIFO Collection

    //options stuff
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    public bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsAmount;
    public Text questionText;

    private DialogueBase currentDialogue;
    private bool isCurrentlyTyping;
    private string completeText;

    public Animator anim;
    private Sprite lastSprite;
    private bool buffer;

    public delegate void OnDialogueLineCallBack(int dialogueLine);
    public OnDialogueLineCallBack onDialogueLineCallBack;
    private int totalLineCount;

    public QuestBase CompletedQuest { get; set; }
    public bool CompletedQuestReady { get; set; }

    private void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
    }

    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue || QuestManager.instance.InQuestUI || GameManager.instance.rewardManager.InQuestReward) return;
        buffer = true;
        inDialogue = true;
        StartCoroutine(BufferTimer());

        dialogueBox.SetActive(true);
        dialogueInfo.Clear();
        currentDialogue = db;

        OptionsParser(db);

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        totalLineCount = dialogueInfo.Count;

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            if (buffer == true) return;
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndofDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        if(onDialogueLineCallBack != null)
        {
            onDialogueLineCallBack?.Invoke(totalLineCount - dialogueInfo.Count);
        }

        completeText = info.myText;
        dialogueName.text = info.character.myName;
        dialogueText.text = info.myText;
        dialogueText.font = info.character.myFont;
        info.ChangeEmotion();
        //dialoguePortrait.sprite = info.character.MyPortrait;
        lastSprite = info.character.MyPortrait;

        if (info.character.characterAOC != null)
        {
            anim.enabled = true;
            anim.runtimeAnimatorController = info.character.characterAOC;
            anim.SetBool("isTalking", true);
            anim.Play(info.characterEmotion.ToString());
        }
        else
        {
            dialoguePortrait.sprite = info.character.MyPortrait;
        }

        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    private bool CheckPunctuation(char c)
    {
        if (puncutationCharacters.Contains(c))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        foreach(char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            AudioManager.instance.PlayClip(info.character.myVoice);

            if (CheckPunctuation(c))
            {
                yield return new WaitForSeconds(0.25f);
            }
        }

        FinishTalking();
        isCurrentlyTyping = false;
    }

    IEnumerator BufferTimer()
    {
        yield return new WaitForSeconds(0.1f);
        buffer = false;
    }

    private void CompleteText()
    {
        FinishTalking();
        dialogueText.text = completeText;
    }

    private void FinishTalking()
    {
        anim.SetBool("isTalking", false);
        anim.enabled = false;
        dialoguePortrait.sprite = lastSprite;
    }

    public void EndofDialogue()
    {
        dialogueBox.SetActive(false);
        OptionsLogic();
        CheckIfDialogueQuest();
        SetItemRewards();
    }

    private void SetItemRewards()
    {
        if (CompletedQuestReady)
        {
            GameManager.instance.rewardManager.SetRewardUI(CompletedQuest);

            CompletedQuestReady = false;
        }
    }

    private void CheckIfDialogueQuest()
    {
        if(currentDialogue is DialogueQuest)
        {
            DialogueQuest DQ = currentDialogue as DialogueQuest;

            QuestManager.instance.SetQuestUI(DQ.quest);
        }
    }

    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
        }
        else
        {
            inDialogue = false;
        }
    }

    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

    private void OptionsParser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;

            optionButtons[0].GetComponent<Button>().Select();

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (inDialogue)
            {
                DequeueDialogue();
            }
        }
    }
}
