using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimonGameManager : MonoBehaviour
{
    [System.Serializable]
    public class SimonStep
    {
        public string word;
        public Color color;
        public string colorName;
    }

    [Header("Affichage")]
    public TextMeshPro displayText;

    [Header("Séquences")]
    public int totalSequences = 3;
    public float displayTime = 1.5f;
    public float delayBetween = 0.5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip errorClip;
    public AudioClip successClip;

    [Header("État d'interaction")]
    public bool canInteract = false;

    private string[] colorNames = new string[] { "Red", "Yellow", "Green", "Blue" };
    private Dictionary<string, Color> colorMap;
    private List<SimonStep> currentSequence;
    private int currentStepIndex;
    private int currentSequenceCount;

    private bool waitingForInput;
    private bool isProcessingSequence;

    [Header("Animations Murs")]
    [SerializeField] private Animator leftWallAnimator;
    [SerializeField] private Animator rightWallAnimator;
    [SerializeField] private string animationLeft = "WallLeftSimonGame";
    [SerializeField] private string animationRight = "WallRightSimonGame";
    public float reverseSpeed = -1f;

    void Start()
    {
        SetupColorMap();
    }

    public void StartSimonGame()
    {
        currentSequenceCount = 0;
        StartCoroutine(PlayNextSequence());
    }

    void SetupColorMap()
    {
        colorMap = new Dictionary<string, Color>
        {
            { "Red", Color.red },
            { "Yellow", Color.yellow },
            { "Green", Color.green },
            { "Blue", Color.blue }
        };
    }

    IEnumerator PlayNextSequence()
    {
        isProcessingSequence = true;
        canInteract = false;

        int wordCount = 4 + currentSequenceCount;
        currentSequence = GenerateSequence(wordCount);
        currentStepIndex = 0;
        waitingForInput = false;

        foreach (var step in currentSequence)
        {
            displayText.text = step.word;
            displayText.color = step.color;
            yield return new WaitForSeconds(displayTime);
            displayText.text = "";
            yield return new WaitForSeconds(delayBetween);
        }

        displayText.text = "À toi de jouer !";
        displayText.color = Color.white;
        waitingForInput = true;
        canInteract = true;
        isProcessingSequence = false;
    }

    List<SimonStep> GenerateSequence(int count)
    {
        List<SimonStep> sequence = new List<SimonStep>();
        for (int i = 0; i < count; i++)
        {
            string word = colorNames[Random.Range(0, colorNames.Length)];
            string colorName = colorNames[Random.Range(0, colorNames.Length)];
            sequence.Add(new SimonStep
            {
                word = word,
                colorName = colorName,
                color = colorMap[colorName]
            });
        }
        return sequence;
    }

    public void HandleInput(string pressedColor)
    {
        if (!waitingForInput || isProcessingSequence || !canInteract)
            return;

        var expected = currentSequence[currentStepIndex].colorName;

        if (pressedColor == expected)
        {
            currentStepIndex++;
            if (currentStepIndex >= currentSequence.Count)
            {
                currentSequenceCount++;
                waitingForInput = false;
                canInteract = false;

                if (currentSequenceCount >= totalSequences)
                {
                    displayText.text = "Bravo !";
                    displayText.color = Color.green;
                    if (audioSource != null && successClip != null)
                        audioSource.PlayOneShot(successClip);

                    EndRiddle();
                }
                else
                {
                    StartCoroutine(WaitAndStartNext());
                }
            }
        }
        else
        {
            waitingForInput = false;
            canInteract = false;
            StartCoroutine(DisplayErrorThenRestart());
        }
    }

    IEnumerator DisplayErrorThenRestart()
    {
        isProcessingSequence = true;
        if (audioSource != null && errorClip != null)
            audioSource.PlayOneShot(errorClip);

        displayText.text = "❌ Error ❌";
        displayText.color = Color.red;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(PlayNextSequence());
    }

    IEnumerator WaitAndStartNext()
    {
        if (audioSource != null && successClip != null)
            audioSource.PlayOneShot(successClip);

        displayText.text = "Bien joué !";
        displayText.color = Color.green;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(PlayNextSequence());
    }

    private void EndRiddle()
    {
        PlayReverse();
        StartCoroutine(ResetWallSpeedAfter(1.5f));
    }

    private void PlayReverse()
    {
        if (leftWallAnimator != null && !string.IsNullOrEmpty(animationLeft))
        {
            leftWallAnimator.Play(animationLeft, 0, 1f);
            leftWallAnimator.speed = reverseSpeed;
        }

        if (rightWallAnimator != null && !string.IsNullOrEmpty(animationRight))
        {
            rightWallAnimator.Play(animationRight, 0, 1f);
            rightWallAnimator.speed = reverseSpeed;
        }
    }

    private IEnumerator ResetWallSpeedAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        if (leftWallAnimator != null) leftWallAnimator.speed = 1f;
        if (rightWallAnimator != null) rightWallAnimator.speed = 1f;
    }
}
