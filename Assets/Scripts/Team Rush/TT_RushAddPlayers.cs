using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class TT_RushAddPlayers : MonoBehaviour
{
    public InputField nameInputField;
    public GameObject nameSlatePrefab;
    public Transform nameSlateParent;
    public Button continueButton, shuffleButton, rotateButton;
    public Transform[] teamHolders, teamHolderParents;
    public float baseWidth = 200f, widthPerCharacter = 15f, maxWidth = 600f, animationDuration = 0.5f, verticalSpacing = 10f;
    private HashSet<string> addedPlayers = new HashSet<string>(); // Tracks added players
    private HashSet<string> assignedPlayers = new HashSet<string>(); // Tracks assigned players

    private const int maxCharacters = 13;
    private float currentRotation = 0f; // Track button rotation

    private void Start()
    {
        continueButton?.onClick.AddListener(HandleContinueButton);
        nameInputField?.onValueChanged.AddListener(ValidateInputLength);
        shuffleButton?.onClick.AddListener(ShuffleTeams);
        UpdateContinueButtonState();

            foreach (var teamHolderParent in teamHolderParents)
        {
            Button deleteButton = teamHolderParent.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() => DeleteTeamHolder(teamHolderParent));
            }
        }

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

        UpdateTeamPlayers(); // Sync teams based on the latest player list
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

        RotateButton(); // Rotate when a player is added ✅
        
    }

    private void RotateButton()
    {
        if (rotateButton != null)
        {
            currentRotation += 90f; // Rotate by 360 degrees
            rotateButton.transform.DORotate(new Vector3(0, 0, currentRotation), 0.4f, RotateMode.Fast)
                .SetEase(Ease.InOutSine);
                Debug.Log("Rotated!");
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
            string playerName = nameSlateParent.GetChild(i).GetComponentInChildren<Text>().text;
            addedPlayers.Remove(playerName);
            Destroy(nameSlateParent.GetChild(i).gameObject);
        }

        assignedPlayers.Clear(); // Clear assigned players
        foreach (var teamHolder in teamHolders)
        {
            foreach (Transform child in teamHolder)
            {
                Destroy(child.gameObject);
            }
        }

        continueButton.interactable = false;
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
                assignedPlayers.Remove(playerName); // Also remove from teams
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

    private void UpdateTeamPlayers()
    {
        Debug.Log("Updating Team Players...");

        // 1. Remove players from team holders that are no longer in the added list.
        foreach (var teamHolder in teamHolders)
        {
            List<Transform> playersToRemove = new List<Transform>();

            foreach (Transform player in teamHolder)
            {
                string playerName = player.GetComponentInChildren<Text>().text;
                if (!addedPlayers.Contains(playerName))
                {
                    playersToRemove.Add(player);
                }
            }

            foreach (Transform player in playersToRemove)
            {
                assignedPlayers.Remove(player.GetComponentInChildren<Text>().text);
                Destroy(player.gameObject);
            }
        }

        // 2. Create a combined list of already distributed players...
        List<GameObject> allPlayers = new List<GameObject>();
        foreach (var teamHolder in teamHolders)
        {
            foreach (Transform player in teamHolder)
            {
                // Add if not already in our list.
                if (!allPlayers.Contains(player.gameObject))
                    allPlayers.Add(player.gameObject);
            }
        }

        // 3. Append new players from the add-player UI (nameSlateParent) that are not already assigned.
        foreach (Transform child in nameSlateParent)
        {
            string playerName = child.GetComponentInChildren<Text>().text;
            if (assignedPlayers.Contains(playerName))
                continue; // Already have this player

            // Instantiate the new slate.
            GameObject newSlate = Instantiate(child.gameObject, Vector3.zero, Quaternion.identity);
            newSlate.transform.SetParent(null); // Unparent temporarily
            newSlate.transform.localScale = Vector3.one;

            // Hide the delete button.
            Button deleteButton = newSlate.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(false);
            }

            // Enable the Outline component.
            Outline outline = newSlate.GetComponentInChildren<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }

            // Optionally, you can call your positioning/animation method:
            // PositionAndAnimateSlate(newSlate);

            // Add this new slate to the merged list and mark it as assigned.
            allPlayers.Add(newSlate);
            assignedPlayers.Add(playerName);
        }

        // 4. Activate the appropriate team holder parents based on the total players.
        ActivateTeamHolders(allPlayers.Count);

        // 5. Re-distribute all players (existing + new) among the active team holders.
        DistributePlayers(allPlayers);
    }
    private void ActivateTeamHolders(int totalPlayers)
    {
        // Always activate the first two team holder parents.
        teamHolderParents[0].gameObject.SetActive(true);
        teamHolderParents[1].gameObject.SetActive(true);

        // Activate additional holders based on totalPlayers.
        teamHolderParents[2].gameObject.SetActive(totalPlayers > 8);
        teamHolderParents[3].gameObject.SetActive(totalPlayers > 12);
    }
    public void DistributePlayers(List<GameObject> players)
    {
        if (players.Count == 0)
        {
            Debug.LogWarning("No players to distribute.");
            return;
        }

        Debug.Log($"Distributing {players.Count} players among teams...");

        // Determine active team holders (based on the teamHolderParents' active state).
        List<Transform> activeHolders = new List<Transform>();
        foreach (var teamHolder in teamHolders)
        {
            if (teamHolder.parent.gameObject.activeSelf)
            {
                activeHolders.Add(teamHolder);
            }
            // (We do not clear children here because we are reassigning from our merged list.)
        }

        if (activeHolders.Count == 0)
        {
            Debug.LogWarning("No active team holders available.");
            return;
        }

        // Reassign each player in the combined list to an active team holder.
        for (int i = 0; i < players.Count; i++)
        {
            int teamIndex = i % activeHolders.Count;
            players[i].transform.SetParent(activeHolders[teamIndex], false);

            // Ensure the Outline is enabled.
            Outline outline = players[i].GetComponentInChildren<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }
        }

        Debug.Log("Distribution Complete.");
    }

    public void OnContinueButtonPressed()
    {
        UpdateTeamPlayers();
    }

    public void DeleteTeamHolder(Transform teamHolderParent)
    {
        if (teamHolderParent == null)
        {
            Debug.LogError("Team holder parent is not assigned!");
            return;
        }

        // Find the corresponding teamHolder
        Transform teamHolder = null;
        foreach (var holder in teamHolders)
        {
            if (holder.parent == teamHolderParent)
            {
                teamHolder = holder;
                break;
            }
        }

        if (teamHolder == null)
        {
            Debug.LogError("Team holder not found for the parent!");
            return;
        }

        // Remove all players inside the team holder
        foreach (Transform child in teamHolder)
        {
            assignedPlayers.Remove(child.GetComponentInChildren<Text>().text);
            Destroy(child.gameObject);
        }

        // Disable the teamHolderParent
        teamHolderParent.gameObject.SetActive(false);

        Debug.Log($"Deleted {teamHolderParent.name} and its contents.");
    }

    public void ShuffleTeams()
    {
        List<Transform> activeTeams = new List<Transform>();
        List<Transform> allPlayers = new List<Transform>();

        // Collect active teams and their players
        foreach (Transform teamHolder in teamHolders)
        {
            if (teamHolder.childCount > 0)
            {
                activeTeams.Add(teamHolder);
                foreach (Transform player in teamHolder)
                {
                    allPlayers.Add(player);
                }
            }
        }

        if (allPlayers.Count == 0 || activeTeams.Count == 0)
        {
            Debug.LogWarning("No players or teams available for shuffling.");
            return;
        }

        // 🔹 Shuffle the player list
        for (int i = allPlayers.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (allPlayers[i], allPlayers[randomIndex]) = (allPlayers[randomIndex], allPlayers[i]);
        }

        // 🔹 Reassign shuffled players to teams without creating/destroying objects
        for (int i = 0; i < allPlayers.Count; i++)
        {
            int teamIndex = i % activeTeams.Count;
            allPlayers[i].SetParent(activeTeams[teamIndex], false);
        }

        Debug.Log("Players shuffled successfully!");
    }
}
