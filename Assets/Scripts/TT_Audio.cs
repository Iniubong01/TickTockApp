using UnityEngine;

public class TT_Audio : MonoBehaviour
{   
    [Space(10), Header ("Audio Slots"), Tooltip("Audio setting slots")] 
    [SerializeField] public AudioSource t_audioSource;
    [SerializeField] private AudioClip clickSound;


    public void Click()
    {
        t_audioSource.PlayOneShot(clickSound);
    }
}
