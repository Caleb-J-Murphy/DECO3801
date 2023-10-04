using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueMaster : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI dialogueText;
    public Sprite[] avatars;
    public string[] dialogues;
    private int currentDialogueIndex = 0;
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ChangeDialogue());
    }

    IEnumerator ChangeDialogue()
    {
        while (currentDialogueIndex < dialogues.Length)
        {
            avatarImage.sprite = avatars[currentDialogueIndex];
            dialogueText.text = dialogues[currentDialogueIndex];

            

            // Wait for audio clip to finish before the next iteration
            audioSource.clip = audioClips[currentDialogueIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);

            currentDialogueIndex++;
        }
    }

    public void HideDialogue()
    {
        avatarImage.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }
}
