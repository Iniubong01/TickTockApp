using UnityEngine;
using UnityEngine.EventSystems;

public class TT_Tap : MonoBehaviour
{
    private TT_GamePlay gamePlay;
    private TT_TweenManager tweenManager;

    void Start()
    {
        tweenManager = GameObject.Find("Tween Manager").GetComponent<TT_TweenManager>();

        gamePlay = GameObject.Find("Game Settings").GetComponent<TT_GamePlay>();

        if (gamePlay == null)
        {
            Debug.LogError("TT_GamePlay component not found on Game Settings!");
        }
    }

    void Update()
    {
        // Detect a tap or click
        if (Input.GetMouseButtonDown(0)  && tweenManager.canTap == true)
        gamePlay.ChangeBackgroundAndQuestion();
        return;        
    }

    // Check if the tap is over a UI element
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
