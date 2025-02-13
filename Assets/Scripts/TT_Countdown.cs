using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TT_Countdown : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text tipsText;

    [SerializeField] private int waitTime = 1;

    [SerializeField] private GameObject instructionTexts;

    private TT_TweenManager tweenManager;
    private TT_GamePlay gamePlay;
    private AudioSource m_audio;
    private float initialVolume;

    private int countdownTimer;

    private List<string> Tips = new List<string>
    {
        "Make the game more personal by filling in the names of the players.",
        "Do you know you can unlock themes for free by just playing loads of rounds?",
        "You can change how long the bomb takes to go off in settings.",
        "Call your friends to hop in, fun awaits!!!",
        "You can toggle between Most Likely, Name 3 Things, or both, depending on your preference.",
        "Ready for fun and a bit of flirty banter...? Spice up the game with themed dares!"
    };

    void Start()
    {
        tweenManager = GameObject.Find("Tween Manager").GetComponent<TT_TweenManager>();
        gamePlay =  GameObject.Find("Game Settings").GetComponent<TT_GamePlay>();
        m_audio = GameObject.Find("Game Audio").GetComponent<AudioSource>();

        initialVolume = m_audio.volume; // Store initial volume
    }

    private void UpdateTipsText()
    {
        tipsText.text = Tips[Random.Range(0, Tips.Count)];
    }

   IEnumerator Background()
    {
        foreach (var obj in backgrounds)
        {
            // Deactivate all backgrounds first to ensure only one is active at a time
            foreach (var bg in backgrounds)
            {
                bg.SetActive(false);
            }

            // Activate the current background
            obj.SetActive(true);

            // Wait for 1 second before moving to the next background
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator CountDown(int duration)
    {
        countdownTimer = duration;
        while (countdownTimer >= 1)
        {
            countdownText.text = countdownTimer.ToString();

            Debug.Log($"Timer: {countdownTimer}");
            yield return new WaitForSeconds(waitTime);

            countdownTimer--;
        }

        countdownText.text = "1";
        Debug.Log("Countdown Complete!");
        StartCoroutine(FadeOutAudio(1f)); // Gradual fade-out
        gamePlay.setGameplayLenght();
        
        tweenManager.countdownUIOutgamePlayUIIn();
    }

    private IEnumerator FadeOutAudio(float fadeDuration)
{
    float startVolume = m_audio.volume;

    while (m_audio.volume > 0)
    {
        m_audio.volume -= startVolume * Time.deltaTime / fadeDuration;
        yield return null;
    }

    m_audio.mute = true; // Ensure it's muted after fading out
}

    public void StartCounting()
    {
        StartCoroutine(CountDown(3));
        StartCoroutine(Background());
        UpdateTipsText();
    }

    public void ShowInstructionTexts()
    {
        instructionTexts.SetActive(true);
    }

    public void HideInstructionTexts()
    {
        instructionTexts.SetActive(false);
    }

    public void ResetTimer()
    {  
        countdownTimer = 3; // Reset countdown to default value
        StartCoroutine(CountDown(3));
        UpdateTipsText(); // Update tips text

        ResumeMusic();
    }

    public void ResetTime()
    {  
        countdownTimer = 3; // Reset countdown to default value

        ResumeMusic();
    }

    public void ResumeMusic()
    {
        m_audio.mute = false; // Unmute the audio
        m_audio.volume = initialVolume; // Reset volume to its initial value
        if (!m_audio.isPlaying)
        {
            m_audio.Play(); // Resume or start playing the audio if it's not already playing
        }
    }

}
