using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TT_Rotate : MonoBehaviour
{
    private Button button;
    private float currentRotation = 0f; // Keeps track of rotation

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RotateButton);
    }

    void RotateButton()
    {
        currentRotation += 90f; // Increment by 90 degrees

        transform.DORotate(new Vector3(0, 0, currentRotation), 0.3f, RotateMode.Fast)
            .SetEase(Ease.InOutSine); // Smooth transition
    }
}
