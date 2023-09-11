using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    public TextAsset dialogueFile;
    public GameObject dialoguePanel;

    [Header("Audio Settings")]
    public AudioSource audioSource;

    public AudioClip[] dialogueAudioClips;



    private string[] dialogueLines;
    private int currentLine = 0;

    private void Start()
    {
        LoadDialogue();
        DisplayDialogue();
        DisplayNextLine();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }

    }

    private void LoadDialogue()
    {
        if (dialogueFile != null)
        {
            dialogueLines = dialogueFile.text.Split('\n');
        }
    }

    public void DisplayNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            string[] lineParts = dialogueLines[currentLine].Split(':');
            string characterName = lineParts[0].Trim();
            string dialogue = lineParts[1].Trim();

            characterNameText.text = characterName;
            dialogueText.text = dialogue;

            // Play audio clip if available for the current line
            if (currentLine < dialogueAudioClips.Length)
            {
                audioSource.clip = dialogueAudioClips[currentLine];
                audioSource.Play();
            }

            currentLine++;
        }
        else
        {
            EndDialogue();          
        }
    }

    private void DisplayDialogue()
    {
        dialoguePanel.SetActive(true);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        characterNameText.text = "";
        dialogueText.text = "";
        // Handle the end of the conversation (e.g., load the next scene).
    }
}
