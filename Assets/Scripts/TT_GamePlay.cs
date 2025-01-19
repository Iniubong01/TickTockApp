using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TT_GamePlay : MonoBehaviour
{
    [SerializeField] private Toggle nameThreeThingsToggle;
    [SerializeField] private Toggle mostLikelyToggle;
    [SerializeField] private Toggle playWithPunishmentToggle;
    [SerializeField] private Text questionText;
    [SerializeField] private GameObject mostLikelyGameObject; // Assign in the Inspector
    [SerializeField] private GameObject nameThreeGameObject; // Assign in the Inspector
    [SerializeField] private GameObject[] backgrounds;

    [SerializeField] private ParticleSystem boom;

    [SerializeField] private GameObject background;

    [ Space(10), Header ("Punishment Text"), Tooltip("GameObjects handling punishments!")]
    [SerializeField] private InputField punishmentInput;
    [SerializeField] private Text punishmentText;

    [SerializeField] private Button changeButton;  // Button to update the punishment text
    private const int maxCharacters = 40; // Maximum characters allowed

    [Header ("Managers")]
    private TT_QuestionManager questionManager;
    private TT_TweenManager tweenManager;
    private TT_GameOver gameOver;
    private TT_Countdown countdown;

    
    private Coroutine tickingCoroutine;


    [ Space(10), Header ("Slider"), Tooltip("Slider settings!")]
    public MonoBehaviour rangeSlider; // Reference to the imported range slider
    public Text minValueText;         // Regular UI Text for Min value
    public Text maxValueText;         // Regular UI Text for Max value

    private float minValue;
    private float maxValue;
    private float countdownTimer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip boomSound;

    [HideInInspector] public int timePlayed;

    [HideInInspector] public bool isGameOver, isTicking;


    void Start()
    {
        GameObject qmObject = GameObject.Find("Question Manager");
        if (qmObject == null)
        {
            Debug.LogError("Question Manager GameObject not found!");
            return;
        }

        questionManager = qmObject.GetComponent<TT_QuestionManager>();
        if (questionManager == null)
        {
            Debug.LogError("TT_QuestionManager component not found on Question Manager GameObject!");
        }

        tweenManager = GameObject.Find("Tween Manager").GetComponent<TT_TweenManager>();
        gameOver = GameObject.Find("Game Over UI").GetComponent<TT_GameOver>();
        countdown = GameObject.Find("Count Down UI").GetComponent<TT_Countdown>();


        if (nameThreeThingsToggle == null || mostLikelyToggle == null)
        {
            return;
        }

        if (punishmentInput!= null)
        {
          punishmentInput.onValueChanged.AddListener(ValidateInputLength);
        }

        if (changeButton != null)
        {
            changeButton.onClick.AddListener(Punishment);
        }

        setGameplayLenght();

        EnsureAtLeastOneToggleActive();

        // Add listeners to toggles
        nameThreeThingsToggle.onValueChanged.AddListener(delegate { EnsureAtLeastOneToggleActive(); });
        mostLikelyToggle.onValueChanged.AddListener(delegate { EnsureAtLeastOneToggleActive(); });

        // Set the initial background and question
        UpdateBackground();
        selectStyle();
    }

    private void ValidateInputLength(string input)
    {
        if (input.Length > maxCharacters)
        {
            // Truncate the input to the maximum allowed characters
           punishmentInput.text = input.Substring(0, maxCharacters);
        }
    }

    private void EnsureAtLeastOneToggleActive()
    {
        if (!nameThreeThingsToggle.isOn && !mostLikelyToggle.isOn)
        {
            mostLikelyToggle.isOn = true; // Default choice
        }
    }
    

    void Update()
    {
        // Update Text
        if (minValueText != null)
            minValueText.text = minValue.ToString(); // Adjust format as needed
        if (maxValueText != null)
            maxValueText.text = maxValue.ToString();

        minValue = GetMinValueFromSlider();
        maxValue = GetMaxValueFromSlider();

        if (tweenManager.canTap)
        {
            // Start ticking when canTap is true and ensure only one coroutine runs
            if (tickingCoroutine == null)
                tickingCoroutine = StartCoroutine(Ticking());

            // Decrease the timer
            countdownTimer -= Time.deltaTime;
            timePlayed = Mathf.CeilToInt(countdownTimer);

            if (timePlayed <= 0 && isGameOver == false)
            {
                GameOver();
            }
        }
        else if (tickingCoroutine != null)
        {
            // Stop ticking when canTap is false
            StopCoroutine(tickingCoroutine);
            tickingCoroutine = null;
        }

        if (gameOver)
            showPunishment();
    }

    private IEnumerator Ticking()
    {
        // Capture the initial timePlayed value at the start
        int initialCountdownValue = Mathf.CeilToInt(timePlayed); // Store the initial timePlayed as an int
        float countdownValue = initialCountdownValue; // Use a float for gradual decrement
        float waitTime = 1.2f; // Initial tick interval

        // Get the CanvasGroup component for transparency control
        CanvasGroup canvasGroup = background.GetComponent<CanvasGroup>(); // Replace with your GameObject
        if (canvasGroup == null)
        {
            canvasGroup = background.AddComponent<CanvasGroup>(); // Add CanvasGroup if it doesn't exist
        }

        while (tweenManager.canTap && countdownValue > 0)
        {
            PlayTickSound();

            // Fully visible when the tick sound is played
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, waitTime / 4)); // Fade in to full opacity (1)

            // Adjust waitTime based on the remaining countdownValue
            float progress = 1f - (countdownValue / initialCountdownValue); // Progress from 0 to 1
            waitTime = Mathf.Lerp(1.5f, 0.2f, progress); // Linearly interpolate waitTime based on progress

            // Clamp waitTime to ensure it stays within a reasonable range
            waitTime = Mathf.Clamp(waitTime, 0.2f, 1.5f);

            // Fade out smoothly to a lower visibility during the remainder of waitTime
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0.7f, 0f, (3 * waitTime) / 4));

            // Gradually decrease the countdown value
            countdownValue -= 0.4f; // Reduce by a smaller step size
        }


        tickingCoroutine = null; // Reset the coroutine reference when finished
    }

    // Set the CanvasGroup alpha immediately (for transparency control)
    private void SetCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha)
    {
        canvasGroup.alpha = alpha; // Adjust the alpha value of the CanvasGroup
    }

    // Gradually fade the CanvasGroup alpha over a duration
        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                // Gradually interpolate alpha value based on elapsed time
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            canvasGroup.alpha = endAlpha; // Ensure the final value is set
        }

        public void startTicking()
    {
        if (tweenManager.canTap && tickingCoroutine == null)
        {
            // Begin ticking when canTap is true
            tickingCoroutine = StartCoroutine(Ticking());
        }
        else if (!tweenManager.canTap && tickingCoroutine != null)
        {
            // Stop ticking if canTap becomes false
            StopCoroutine(tickingCoroutine);
            tickingCoroutine = null;
        }
    }
    
    public void setGameplayLenght()
    {
        countdownTimer = Random.Range(minValue, maxValue);
    }

    private float GetMinValueFromSlider()
    {
        // Replace "MinValue" with the actual property/method from your imported slider
        return (float)rangeSlider.GetType().GetProperty("valueMin")?.GetValue(rangeSlider, null);
    }

    private float GetMaxValueFromSlider()
    {
        // Replace "MaxValue" with the actual property/method from your imported slider
        return (float)rangeSlider.GetType().GetProperty("valueMax")?.GetValue(rangeSlider, null);
    }

    //////////////////////////////////////////////////////////////////
    /// Beginning of Game Over UI Handling
    public void GameOver()
    {
        PlayBoomSound();
        boom.Play();
        isGameOver= true;
        tweenManager.canTap = false;
        tweenManager.GameOverInGamePlayUIOut();
        Debug.Log("Game Over!");  

        countdown.ResumeMusic();      
    }

    public void Restart()
    {
        isGameOver= false;
        StartCoroutine(canTap());
        tweenManager.GameOverOutCountdownIn();
        Debug.Log("Restart");
        setGameplayLenght();
        countdown.ResetTimer();
    }

    IEnumerator canTap()
    {
        yield return new WaitForSeconds(2.8f);
        tweenManager.canTap = true;
    }

    /// End of Game Over UI Handling
    //////////////////////////////////////////////////////////////////

    public void ChangeBackgroundAndQuestion()
    {
        UpdateBackground();

        // Change the question based on the current toggles
        selectStyle();
    }

    public void PlayTickSound()
    {
        audioSource.PlayOneShot(tickSound);
    }    

    public void PlayBoomSound()
    {
        audioSource.PlayOneShot(boomSound);
    }

    private void UpdateBackground()
    {
        // Deactivate all backgrounds
        foreach (var background in backgrounds)
        {
            if (background != null) background.SetActive(false);
        }

        // Activate the background randomly
        if (backgrounds != null)
        {
            int index = Random.Range(0, backgrounds.Length);
            backgrounds[index].SetActive(true);
        }
    }

    public void Punishment()
    {
        string punishmentAssigned = punishmentInput.text;

        if (!string.IsNullOrEmpty(punishmentAssigned))
        {
            punishmentText.text = $"You've lost the round, you have to {punishmentAssigned}";
        }
        else
        {
            punishmentText.text = "You've lost the round, but no punishment was assigned.";
        }
    }

    public void selectStyle()
    {
        // Both toggles are on
        if (nameThreeThingsToggle.isOn && mostLikelyToggle.isOn)
        {
            if (Random.Range(0, 2) == 0) // 50% chance
            {
                // Randomly choose a question from "Name Three Things"
                DisplayRandomQuestion(questionManager.GetNameThreeThingsQuestionCount(), questionManager.GetNameThreeThingsQuestion);

                // Activate Name Three Things GameObject
                nameThreeGameObject.SetActive(true);
                mostLikelyGameObject.SetActive(false);
            }
            else
            {
                // Randomly choose a question from "Most Likely"
                DisplayRandomQuestion(questionManager.GetQuestionCount(), questionManager.GetMostLikelyQuestion);

                // Activate Most Likely GameObject
                mostLikelyGameObject.SetActive(true);
                nameThreeGameObject.SetActive(false);
            }
        }
        else if (nameThreeThingsToggle.isOn)
        {
            // Select "Name Three Things" style
            DisplayRandomQuestion(questionManager.GetNameThreeThingsQuestionCount(), questionManager.GetNameThreeThingsQuestion);

            // Activate Name Three Things GameObject
            nameThreeGameObject.SetActive(true);
            mostLikelyGameObject.SetActive(false);
        }
        else if (mostLikelyToggle.isOn)
        {
            // Select "Most Likely" style
            DisplayRandomQuestion(questionManager.GetQuestionCount(), questionManager.GetMostLikelyQuestion);

            // Activate Most Likely GameObject
            mostLikelyGameObject.SetActive(true);
            nameThreeGameObject.SetActive(false);
        }
    }

    public void showPunishment()
    {
        // Handle Play With Punishment Toggle
        if (playWithPunishmentToggle.isOn)
        {
            gameOver.ifGameOver();
        }
        else
        {
            gameOver.ifNoPunishment();
        }
    }

    private void DisplayRandomQuestion(int count, System.Func<int, string> getQuestion)
    {
        if (count > 0)
        {
            int randomIndex = Random.Range(0, count);
            questionText.text = getQuestion(randomIndex);
        }
        else
        {
            questionText.text = "No questions available.";
        }
    }

    public void playGame()
    {
        tweenManager.GamePlayInSettingsOut();
    }
}


