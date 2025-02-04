using UnityEngine;
using UnityEngine.UI;

public class TT_RushParentHolder : MonoBehaviour
{
    [Header("Grid Settings")]
    public float spacingX = 10f; // Horizontal spacing between elements
    public float spacingY = 10f; // Vertical spacing between elements
    public int columns = 3;      // Number of columns in the grid

    void Start()
    {
        ArrangeChildren();
    }

    void ArrangeChildren()
    {
        // Get all child RectTransforms (excluding the parent itself)
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        RectTransform parentRect = GetComponent<RectTransform>();

        if (children.Length <= 1)
        {
            Debug.LogWarning("No child objects to arrange!");
            return;
        }

        int row = 0;
        int column = 0;

        foreach (var child in children)
        {
            // Skip the parent itself
            if (child == parentRect) continue;

            // Get the size of the child
            Vector2 childSize = child.sizeDelta;

            // Calculate the position for this child
            float xPos = column * (childSize.x + spacingX);
            float yPos = -row * (childSize.y + spacingY);

            // Set the child's anchored position
            child.anchoredPosition = new Vector2(xPos, yPos);

            // Move to the next column
            column++;

            // If the column count exceeds the set number of columns, move to the next row
            if (column >= columns)
            {
                column = 0;
                row++;
            }
        }
    }
}
