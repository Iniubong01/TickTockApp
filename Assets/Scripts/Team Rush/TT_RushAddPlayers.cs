using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class TT_RushAddPlayers : MonoBehaviour
{
    public InputField nameInputField;
    public GameObject nameSlatePrefab;
    public Transform nameSlateParent;
    public Button continueButton;
    public Transform[] teamHolders;
    public Transform[] teamHolderParents;

    public float baseWidth = 200f;
    public float widthPerCharacter = 15f;
    public float maxWidth = 600f;
    public float animationDuration = 0.5f;
    public float verticalSpacing = 10f;

    public Button shuffleButton;

    private HashSet<string> addedPlayers = new HashSet<string>(); // Tracks added players
    private List<GameObject> playerSlates = new List<GameObject>();
    private HashSet<string> assignedPlayers = new HashSet<string>(); // Tracks players already in teams

    private const int maxCharacters = 14;

    private void Start()
    {
        continueButton?.onClick.AddListener(HandleContinueButton);
        nameInputField?.onValueChanged.AddListener(ValidateInputLength);
        shuffleButton?.onClick.AddListener(ShuffleTeams);

        UpdateContinueButtonState();
    }

    private void ValidateInputLength(string input)
    {
        if (input.Length > maxCharacters)
        {
            nameInputField.text = input.Substring(0, maxCharacters);
        }
    }

    private void HandleContinueButton()
    {
        if (nameSlateParent.childCount == 0)
        {
            Debug.LogWarning("No players available!");
            return;
        }

        if (teamHolders[0].childCount == 0) // If no teams are assigned yet, assign teams
        {
            AssignTeams();
        }
        else // If teams already exist, add only new players
        {
            AddNewPlayersToTeams();
        }
    }

    public void AddPlayer()
    {
        if (!nameSlateParent || !nameInputField || !nameSlatePrefab)
        {
            Debug.LogError("Required components are not assigned!");
            return;
        }

        if (nameSlateParent.childCount > 13)
        {
            return;
        }

        string playerName = nameInputField.text.Trim();
        if (string.IsNullOrWhiteSpace(playerName) || addedPlayers.Contains(playerName))
        {
            return;
        }

        GameObject newSlate = CreateNameSlate(playerName);
        if (newSlate == null)
        {
            Debug.LogError("Failed to create a new slate!");
            return;
        }

        addedPlayers.Add(playerName);
        nameInputField.text = "";
        AssignDeleteFunctionality(newSlate);
        UpdateContinueButtonState();
    }

    private GameObject CreateNameSlate(string playerName)
    {
        GameObject newSlate = Instantiate(nameSlatePrefab, nameSlateParent);
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

        // Disable outline when first created
        Outline outline = newSlate.GetComponentInChildren<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }

        RectTransform slateRect = newSlate.GetComponent<RectTransform>();
        if (slateRect != null)
        {
            float calculatedWidth = baseWidth + (playerName.Length * widthPerCharacter);
            slateRect.sizeDelta = new Vector2(Mathf.Min(calculatedWidth, maxWidth), slateRect.sizeDelta.y);
        }

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
                string playerName = newSlate.GetComponentInChildren<Text>().text;
                Destroy(newSlate);
                addedPlayers.Remove(playerName);
                UpdateContinueButtonState();
            });
        }
        else
        {
            Debug.LogError("Delete Button not found in the name slate prefab!");
        }
    }

    private void UpdateContinueButtonState()
    {
        if (continueButton != null)
        {
            continueButton.interactable = nameSlateParent.childCount > 3;
        }
    }

    public void AssignTeams()
    {
        playerSlates.Clear();
        assignedPlayers.Clear();

        foreach (Transform child in nameSlateParent)
        {
            GameObject playerObject = child.gameObject;
            playerSlates.Add(playerObject);
            assignedPlayers.Add(playerObject.GetComponentInChildren<Text>().text);
        }

        int totalPlayers = playerSlates.Count;
        if (totalPlayers == 0)
        {
            foreach (var parent in teamHolderParents)
            {
                parent.gameObject.SetActive(false);
            }
            return;
        }

        foreach (var parent in teamHolderParents)
        {
            parent.gameObject.SetActive(false);
        }

        int maxTeams = teamHolderParents.Length;
        int activeTeams = Mathf.Min(Mathf.CeilToInt(totalPlayers / 2f), maxTeams);

        for (int i = 0; i < activeTeams; i++)
        {
            teamHolderParents[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < totalPlayers; i++)
        {
            int teamIndex = i % activeTeams;
            GameObject clonedSlate = Instantiate(playerSlates[i], teamHolders[teamIndex]);

            Button deleteButton = clonedSlate.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(false);
            }

            Outline outline = clonedSlate.GetComponentInChildren<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }
        }

        CheckTeamBalance();
    }

    private void AddNewPlayersToTeams()
    {
        foreach (Transform child in nameSlateParent)
        {
            string playerName = child.GetComponentInChildren<Text>().text;

            if (assignedPlayers.Contains(playerName))
            {
                continue; // Skip existing players
            }

            int teamIndex = assignedPlayers.Count % teamHolders.Length;
            GameObject newSlate = Instantiate(child.gameObject, teamHolders[teamIndex]);

            Button deleteButton = newSlate.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(false);
            }

            Outline outline = newSlate.GetComponentInChildren<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }

            assignedPlayers.Add(playerName);
        }

        CheckTeamBalance();
    }

    public void CheckTeamBalance()
    {
        int[] teamCounts = new int[teamHolders.Length];

        for (int i = 0; i < teamHolders.Length; i++)
        {
            teamCounts[i] = teamHolders[i].childCount;
        }
    }

    public void ShuffleTeams()
    {
        List<GameObject> allPlayers = new List<GameObject>();

        List<Transform> activeTeams = new List<Transform>();
        foreach (Transform teamHolder in teamHolders)
        {
            if (teamHolder.childCount > 0)
            {
                activeTeams.Add(teamHolder);
                foreach (Transform player in teamHolder)
                {
                    allPlayers.Add(player.gameObject);
                }
            }
        }

        if (allPlayers.Count == 0 || activeTeams.Count == 0) return;

        for (int i = allPlayers.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (allPlayers[i], allPlayers[randomIndex]) = (allPlayers[randomIndex], allPlayers[i]);
        }

        for (int i = 0; i < allPlayers.Count; i++)
        {
            int teamIndex = i % activeTeams.Count;
            allPlayers[i].transform.SetParent(activeTeams[teamIndex]);
        }

        CheckTeamBalance();
    }
}
