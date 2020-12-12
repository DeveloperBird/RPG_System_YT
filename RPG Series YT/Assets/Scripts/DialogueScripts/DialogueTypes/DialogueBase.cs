using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue Basic")]
public class DialogueBase : ScriptableObject {

    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        [TextArea(4, 8)]
        public string myText;

        public EmotionType characterEmotion;

        public void ChangeEmotion()
        {
            character.Emotion = characterEmotion;
        }
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo;
}
