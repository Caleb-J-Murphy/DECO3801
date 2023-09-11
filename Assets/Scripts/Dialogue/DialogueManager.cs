using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public Dialogue[] dialogues;

    private int currentDialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        DisplayDialogue(dialogues[currentDialogueIndex]);
    }

    public void DisplayNextLine()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            DisplayDialogue(dialogues[currentDialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        characterNameText.text = dialogue.characterName;
        dialogueText.text = dialogue.dialogueLines[0];
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // Handle the end of the conversation (e.g., load the next scene).
    }
}
