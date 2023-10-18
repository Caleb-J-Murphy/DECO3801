using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueMaster : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI dialogueText;
    public Sprite[] avatars;
    public string[] dialogues;
    private int currentDialogueIndex = 0;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    
    public bool isOpening;

    private bool startedDialogue = false;

    void Start()
    {
        if (!isOpening) {
            audioSource = gameObject.AddComponent<AudioSource>();
            StartCoroutine(ChangeDialogue());
        }
    }
    
    void Update()
    {
        if (isOpening && !startedDialogue ) {
            if (Input.GetButtonDown("b")) {
                audioSource = gameObject.AddComponent<AudioSource>();
                StartCoroutine(ChangeDialogue());
                startedDialogue = true;
        }
        }
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
        //Next sceen
        SceneManager.LoadScene("FinalTailgatingEnvironment");
    }

    public void HideDialogue()
    {
        avatarImage.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }
}
