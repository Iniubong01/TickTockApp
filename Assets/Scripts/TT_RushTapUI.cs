using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TT_RushTapUI : MonoBehaviour
{
    // UI Texts for pairing and questions
    public Text describeToText;    // e.g., "Ini describe to Pezi"
    public Text questionText;        // Displays the question word (from questionManager)
    public Text passToText;          // Displays "Pass the device to:"
    public Text nameText;            // Displays the current player’s name (describer)

    [SerializeField] private Button backButton;

    // Question/transitions UI elements
    public Transform currentQuestion, nextQuestion;
    [SerializeField] private Color[] colours;
    [SerializeField] private Image background;
    public CanvasGroup questionCanvasGroup;
    
    public float transitionDuration = 0.5f;
    public float fadeDuration = 0.5f;
    private bool isTransitioning = false, showQuestionToggle = true, questionChange = true, continueRoutine = true;

    // References to other managers
    private TT_TweenManager tweenManager;
    private TT_QuestionManager questionManager;
    private TT_GamePlay gamePlay;


    // Team holders that contain the name slate prefabs 
    public Transform[] teamHolderParents; 

    // Pairing logic fields:
    // currentTeamIndex: used to cycle through active teams  
    private int currentTeamIndex = 0;
    private Dictionary<int, int> teamPairIndices = new Dictionary<int, int>();

    void Start()
    {
        tweenManager = GameObject.Find("Tween Manager").GetComponent<TT_TweenManager>();
        gamePlay = GameObject.Find("Game Settings").GetComponent<TT_GamePlay>();
        questionManager = GameObject.Find("Question Manager").GetComponent<TT_QuestionManager>();
        UpdateColours();
        backButton.gameObject.SetActive(false);

        if (gamePlay == null)
        {
            Debug.LogError("TT_GamePlay component not found on Game Settings!");
        }

        // Initialize pairing indices for each team holder (assuming one team per teamHolder)
        for (int i = 0; i < teamHolderParents.Length; i++)
        {
            teamPairIndices[i] = 0;
        }
    }

    void Update()
    {
        // On mouse click (if not transitioning and not clicking on UI) change question and update pairing.
        if (Input.GetMouseButtonDown(0) && !isTransitioning && !IsPointerOverUI() && tweenManager.canTap && gamePlay.isRush)
        {
            ChangeQuestion(showQuestionToggle);
            showQuestionToggle = !showQuestionToggle;
        }
    }

    public void ChangeQuestion(bool shouldShowQuestion)
    {
        if (currentQuestion == null || nextQuestion == null || isTransitioning)
            return;

        isTransitioning = true;
        StartCoroutine(TransitionNextQuestion(shouldShowQuestion));
    }
    private int showQuestionCallCount = 0;
    private bool isFirstTap = true; // For tracking first tap

    IEnumerator TransitionNextQuestion(bool shouldShowQuestion)
    {
        RectTransform currentRect = currentQuestion.GetComponent<RectTransform>();
        RectTransform nextRect = nextQuestion.GetComponent<RectTransform>();

        float exitPositionX = -1655;
        float enterStartPositionX = -1655;
        float overshootPosition = 60f;
        Vector2 targetPosition = new Vector2(-400f, 510.1962f);

        // Animate current question out.
        currentRect.DOAnchorPos(new Vector2(exitPositionX, currentRect.anchoredPosition.y), transitionDuration)
            .SetEase(Ease.InCubic).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
        
        });

        yield return new WaitForSeconds(transitionDuration * 0.9f);
        currentQuestion.gameObject.SetActive(false);

        if (questionChange && !showQuestionToggle)
        {
            showQuestion();
            if (continueRoutine)
            {
                RushRestartGame();
                continueRoutine = false;
            }
            else
            {
                UpdatePairingUI();
            }

            UpdateColours();
            Debug.LogWarning("Changing words!");
        }

        nextQuestion.gameObject.SetActive(true);

        if (!backButton.gameObject.activeSelf)
        {
            backButton.gameObject.SetActive(true);
        }

        if (isFirstTap)
        {
            // No overshoot position for the first tap
            nextRect.anchoredPosition = targetPosition;
            isFirstTap = false; // Set to false after first use
        }
        else
        {
            // Use overshoot movement from second tap onward
            nextRect.anchoredPosition = new Vector2(enterStartPositionX, targetPosition.y);

            nextRect.DOAnchorPos(new Vector2(targetPosition.x + overshootPosition, targetPosition.y), transitionDuration * 0.8f)
                .SetEase(Ease.OutCubic);

            yield return new WaitForSeconds(transitionDuration * 0.8f);
        }

        nextRect.DOAnchorPos(targetPosition, transitionDuration * 0.2f)
            .SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            questionChange = true;
        });

        yield return new WaitForSeconds(transitionDuration * 0.2f);
        isTransitioning = false;
        SwapQuestions();
    }


    void SwapQuestions()
    {
        (currentQuestion, nextQuestion) = (nextQuestion, currentQuestion);
        // When questions swap, update the pairing UI.
    }

    public void showQuestion()
    {
        questionText.text = questionManager.callRushTheme();
        questionChange = false;
    }

    public void UpdateColours()
    {
        if (background != null)
        {
            int index = Random.Range(0, colours.Length);
            Color selectedColor = colours[index];
            selectedColor.a = 1f;
            background.color = selectedColor;
            questionText.color = selectedColor;
            nameText.color = selectedColor;
        }
    }

    public void goBack()
    {
        if (currentQuestion == null || nextQuestion == null || isTransitioning)
            return;

        isTransitioning = true;
        questionChange = false;
        showQuestionToggle = false;
        StartCoroutine(TransitionPreviousQuestion());
    }

    IEnumerator TransitionPreviousQuestion()
    {
        RectTransform currentRect = currentQuestion.GetComponent<RectTransform>();
        RectTransform previousRect = nextQuestion.GetComponent<RectTransform>();

        float exitPositionX = 1655;
        float enterStartPositionX = -1655;
        float overshootPosition = 60f;
        Vector2 targetPosition = new Vector2(-400f, 510.1962f);

        currentRect.DOAnchorPos(new Vector2(exitPositionX, currentRect.anchoredPosition.y), transitionDuration)
            .SetEase(Ease.InCubic);

        yield return new WaitForSeconds(transitionDuration * 0.9f);
        currentQuestion.gameObject.SetActive(false);

        nextQuestion.gameObject.SetActive(true);
        previousRect.anchoredPosition = new Vector2(enterStartPositionX, targetPosition.y);

        previousRect.DOAnchorPos(new Vector2(targetPosition.x + overshootPosition, targetPosition.y), transitionDuration * 0.8f)
            .SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(transitionDuration * 0.8f);

        previousRect.DOAnchorPos(targetPosition, transitionDuration * 0.2f)
            .SetEase(Ease.InOutCubic);

        yield return new WaitForSeconds(transitionDuration * 0.2f);
        isTransitioning = false;
        SwapQuestions();
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
                return true;
        }
        return false;
    }

    public void OnIDontKnowButtonPressed()
    {
        ChangeWordWithFade();
    }

    void ChangeWordWithFade()
    {
        if (questionCanvasGroup == null)
            return;
        questionCanvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            showQuestion();
            questionCanvasGroup.DOFade(1, fadeDuration);
        });
    }


    // Retrieves the list of player names from a team parent.
    // It finds the child GameObject named "Holder" in the specified teamHolderParent and then collects the text from each nameslate prefab.
    private List<string> GetTeamPlayersFromParent(int parentIndex)
    {
        List<string> players = new List<string>();

        if (parentIndex < 0 || parentIndex >= teamHolderParents.Length)
        {
            Debug.LogWarning("Team parent index out of range.");
            return players;
        }
        
        // Find the child GameObject named "Holder"
        Transform holder = teamHolderParents[parentIndex].Find("Holder");
        
        
        if (holder == null)
        {
            Debug.LogWarning($"No child named 'Holder' found in teamHolderParent at index {parentIndex}");
            return players;
        }
        
        // Go over all children in the Holder (nameslate prefabs).
        foreach (Transform nameslate in holder)
        {
            // Try to get the Text component from this nameslate.
            Text t = nameslate.GetComponentInChildren<Text>();
            if (t != null)
            {
                Debug.Log($"TeamParent {parentIndex} found player: {t.text}");
                players.Add(t.text);
            }
            else
            {
                Debug.LogWarning($"No Text component found in a child of Holder in teamHolderParent {parentIndex}");
            }
        }
        return players;
    }

    // Updates the pairing UI texts based on the current active team.
    // Cycles through active team parent holders in a round-robin manner.
    public void UpdatePairingUI()
    {
        // Collect active team parent indices.
        List<int> activeParentIndices = new List<int>();
        for (int i = 0; i < teamHolderParents.Length; i++)
        {
            if (teamHolderParents[i].gameObject.activeInHierarchy)
                activeParentIndices.Add(i);
        }

        if (activeParentIndices.Count == 0)
        {
            Debug.LogWarning("No active team parent holders available for pairing.");
            return;
        }

        // Determine the current active team parent using a round-robin approach.
        int activeIndex = currentTeamIndex % activeParentIndices.Count;
        int parentIndex = activeParentIndices[activeIndex];

        // Retrieve the list of players from the selected team.
        List<string> teamPlayers = GetTeamPlayersFromParent(parentIndex);
        if (teamPlayers.Count < 2)
        {
            Debug.LogWarning($"Not enough players in team parent holder {parentIndex} for pairing.");
            return;
        }

        // Retrieve the pairing index for this team.
        int describerIndex = teamPairIndices.ContainsKey(parentIndex) ? teamPairIndices[parentIndex] : 0;
        int receiverIndex = (describerIndex + 1) % teamPlayers.Count;

        string describer = teamPlayers[describerIndex];
        string receiver = teamPlayers[receiverIndex];


        describeToText.text = $"{describer} describe to {receiver}";  // Updated first
        passToText.text = "Pass the device to:";
        nameText.text = describer;  // Updated after `describeToText` to maintain order

        Debug.Log($"Pairing updated for team parent {parentIndex}: {describer} -> {receiver}");

        // Advance to the next pairing for this team.
        teamPairIndices[parentIndex] = receiverIndex;

        // Move to the next team in the next update cycle.
        currentTeamIndex = (currentTeamIndex + 1) % activeParentIndices.Count;
    }


        public void RushRestartGame()
    {
        // Reset pairing index
        currentTeamIndex = 0;
        
        // Clear previous pairing history
        teamPairIndices.Clear();

        // Reset UI text to default state
        passToText.text = "Pass the device to:";
        nameText.text = "Name";  // Default name for the first player
        describeToText.text = "";

        // Restart the pairing routine
        UpdatePairingUI();
    }
}
