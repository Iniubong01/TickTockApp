using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private TT_RushAddPlayers rush;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent; // Store original parent at start
    }

    void Start()
    {
        rush = GameObject.Find("Team Rush Add Player UI").GetComponent<TT_RushAddPlayers>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter; // Use pointerEnter if pointerDrop is unavailable

        if (target != null && target.CompareTag("TeamHolder")) // Ensure it's a valid holder
        {
            transform.SetParent(target.transform);
            transform.localPosition = Vector3.zero; // Reset position inside the holder

            // Reset RectTransform for proper fitting in Grid Layout
            RectTransform rect = GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
        }
        else
        {
            transform.SetParent(originalParent);
            transform.position = originalPosition; // Return to original place if not dropped in a valid holder
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        rush.CheckTeamBalance(); // Call balance check
    }
}
