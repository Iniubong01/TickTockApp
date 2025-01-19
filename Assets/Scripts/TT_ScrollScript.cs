using UnityEngine;
using UnityEngine.UI;

public class TT_ScrollScript : MonoBehaviour
{
    public GameObject scrollBar;

    private float scrollPos = 0;
    private float[] pos;
    private Scrollbar scrollbarComponent;

    void Start()
    {
        // Cache the Scrollbar component
        scrollbarComponent = scrollBar.GetComponent<Scrollbar>();

        // Initialize positions based on child count
        int childCount = transform.childCount;
        pos = new float[childCount];
        for (int i = 0; i < childCount; i++)
        {
            pos[i] = i / (float)(childCount - 1);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Update scroll position when dragging
            scrollPos = scrollbarComponent.value;
        }
        else
        {
            // Snap to the nearest position
            for (int i = 0; i < pos.Length; i++)
            {
                if (Mathf.Abs(scrollPos - pos[i]) < (0.5f / (pos.Length - 1)))
                {
                    scrollbarComponent.value = Mathf.Lerp(scrollbarComponent.value, pos[i], 0.1f);
                }
            }
        }

        // Adjust scale for all items
        for (int i = 0; i < pos.Length; i++)
        {
            float distance = Mathf.Abs(scrollPos - pos[i]);
            float scale = Mathf.Lerp(0.9f, 1f, 1f - Mathf.Clamp01(distance * (pos.Length - 1)));
            transform.GetChild(i).localScale = new Vector2(scale, scale);
        }
    }
}
