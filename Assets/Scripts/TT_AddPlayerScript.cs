using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TT_AddPlayerScript : MonoBehaviour
{
    public InputField nameInputField; // Input field for player name
    public GameObject nameSlatePrefab; // Prefab for the name slate
    public Transform nameSlateParent; // Parent for spawned name slates
    public Button addPlayerButton; // Button to trigger name slate creation
    public Button continueButton; // Button to proceed to the next screen

    public float baseWidth = 200f; // Minimum width of the name slate
    public float widthPerCharacter = 15f; // Width increment per character
    public float maxWidth = 600f; // Maximum allowable width
    public float animationDuration = 0.5f; // Duration of sliding animation
    public float verticalSpacing = 10f; // Space between name slates vertically

    [HideInInspector] public bool addedPlayer;

    private const int maxCharacters = 14; // Maximum characters allowed

    private void Start()
    {
        // Add listener to Add Player Button
        if (addPlayerButton != null)
        {
            addPlayerButton.onClick.AddListener(AddPlayer);
        }
        else
        {
            Debug.LogWarning("Add Player Button is not assigned!");
        }

        // Add listener to Continue Button
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(UpdateContinueButtonState);
        }

        // Add listener to validate input field length
        if (nameInputField != null)
        {
            nameInputField.onValueChanged.AddListener(ValidateInputLength);
        }
        else
        {
            Debug.LogWarning("Name Input Field is not assigned!");
        }

        // Initialize Continue Button state
        UpdateContinueButtonState();
    }

    private void ValidateInputLength(string input)
    {
        if (input.Length > maxCharacters)
        {
            // Truncate the input to the maximum allowed characters
            nameInputField.text = input.Substring(0, maxCharacters);
        }
    }

    public void AddPlayer()
    {
        if (nameSlateParent == null || nameInputField == null || nameSlatePrefab == null)
        {
            Debug.LogError("Required components are not assigned!");
            return;
        }

        if (nameSlateParent.childCount > 13)
        {
            return; // Limit to 14 slates
        }

        // Validate the input
        string playerName = nameInputField.text;
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return; // Ignore empty or whitespace-only input
        }

        // Create and configure a new name slate
        GameObject newSlate = CreateNameSlate(playerName);

        if (newSlate == null)
        {
            Debug.LogError("Failed to create a new slate!");
            return;
        }

        // Clear the input field
        nameInputField.text = "";

        // Assign delete functionality
        AssignDeleteFunctionality(newSlate);

        // Update Continue Button state
        UpdateContinueButtonState();

        addedPlayer = nameSlateParent.childCount > 1;
    }

    private GameObject CreateNameSlate(string playerName)
    {
        // Instantiate the new name slate prefab
        GameObject newSlate = Instantiate(nameSlatePrefab, nameSlateParent);

        // Set the player name on the slate
        Text nameText = newSlate.GetComponentInChildren<Text>();
        if (nameText != null)
        {
            nameText.text = playerName;
        }
        else
        {
            Debug.LogError("Text component not found in the name slate prefab!");
            Destroy(newSlate);
            return null;
        }

        // Adjust the size of the name slate
        RectTransform slateRect = newSlate.GetComponent<RectTransform>();
        if (slateRect != null)
        {
            float calculatedWidth = baseWidth + (playerName.Length * widthPerCharacter);
            slateRect.sizeDelta = new Vector2(Mathf.Min(calculatedWidth, maxWidth), slateRect.sizeDelta.y);
        }

        // Position and animate the slate
        PositionAndAnimateSlate(newSlate);

        return newSlate;
    }

    private void PositionAndAnimateSlate(GameObject newSlate)
    {
        RectTransform rectTransform = newSlate.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            float targetY = -((nameSlateParent.childCount - 1) * (rectTransform.sizeDelta.y + verticalSpacing));
            rectTransform.anchoredPosition = new Vector2(0, targetY);
            rectTransform.localScale = Vector3.zero;
            rectTransform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
        }
    }

    private void AssignDeleteFunctionality(GameObject newSlate)
    {
        Button deleteButton = newSlate.GetComponentInChildren<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(() =>
            {
                Destroy(newSlate);

                // Update Continue Button state
                UpdateContinueButtonState();

                addedPlayer = nameSlateParent.childCount > 1;
            });
        }
        else
        {
            Debug.LogError("Delete Button not found in the name slate prefab!");
        }
    }

    public void DeleteAllPlayers()
    {
        if (nameSlateParent == null)
        {
            Debug.LogError("Name slate parent is not assigned!");
            return;
        }

        for (int i = nameSlateParent.childCount - 1; i >= 0; i--)
        {
            Destroy(nameSlateParent.GetChild(i).gameObject);
        }

        continueButton.interactable = false;
    }

    private void UpdateContinueButtonState()
    {
        if (continueButton != null)
        {
            // Enable the button only if there are player slates
            continueButton.interactable = nameSlateParent.childCount > 0;
        }
    }
}
