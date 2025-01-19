using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TT_TweenManager : MonoBehaviour
{

    [Header("GameObject UI to be slided in")]
    [SerializeField] private RectTransform mainMenu, addPlayerUI, selectThemeUI, instructionUI,
                                            gameSettingsUI, gamePlayUI, countdownUI, gameOverUI;

    [SerializeField] private CanvasGroup gameSettingsCanvasGroup, countdownCanvasGroup; // Reference the CanvasGroup

    public GameObject background;
    public GameObject continueButton;

    [HideInInspector] public bool canTap;

    // Panel positions for transitions
    private readonly Vector2 onScreenPos = new Vector2(0, 0);
    private readonly Vector2 offScreenPos = new Vector2(0, 2300);

    [HideInInspector] public bool doNotShow;

    private TT_Countdown countdown;
    private TT_AddPlayerScript addPlayer;
    private TT_GamePlay gamePlay;



    void Awake()
    {        
        // Make the settings UI invisible and non-interactable
        gameSettingsCanvasGroup.alpha = 0f; // Set transparency to fully invisible
        gameSettingsCanvasGroup.interactable = false; // Disable interaction
        gameSettingsCanvasGroup.blocksRaycasts = false; // Prevent blocking clicks
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = GameObject.Find("Count Down UI").GetComponent<TT_Countdown>();
        addPlayer = GameObject.Find("Add Player UI").GetComponent<TT_AddPlayerScript>();
        gamePlay =  GameObject.Find("Game Settings").GetComponent<TT_GamePlay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Slide difficulty UI in and out
    public void GameSettingsUIOut()
    {

        selectThemeUI.DOAnchorPos(offScreenPos, 0f);

        gameSettingsCanvasGroup.DOFade(0f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            gameSettingsCanvasGroup.interactable = false;
            gameSettingsCanvasGroup.blocksRaycasts = false;
        });
            

        if(doNotShow == true)
        {
            countdownCanvasGroup.DOFade(1f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                countdownCanvasGroup.interactable = true;
                countdownCanvasGroup.blocksRaycasts = true;
            });
            
            countdown.StartCounting();
            countdown.ShowInstructionTexts();

        } else

        {
            instructionUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
        }
        
        return;
    }

    // Slide out difficulty UI and bring back main menu
    public void MenuIngameSettingsOut()
    {
        selectThemeUI.DOAnchorPos(offScreenPos, 0.25f);
        mainMenu.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

     public void MenuIngamePlayUIOut()
    {
        gamePlayUI.DOAnchorPos(offScreenPos, 0.25f);
        mainMenu.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

     public void instructionUIOutcountdownUIIn()
    {
        instructionUI.DOAnchorPos(offScreenPos, 0.25f);

        countdownCanvasGroup.DOFade(1f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            countdownCanvasGroup.interactable = true;
            countdownCanvasGroup.blocksRaycasts = true;
        });

        countdown.StartCounting();
        countdown.ShowInstructionTexts();
    }
    
     public void countdownUIOutgamePlayUIIn()
    {
        countdownCanvasGroup.DOFade(0f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            countdownCanvasGroup.interactable = false;
            countdownCanvasGroup.blocksRaycasts = false;
        });

        canTap = true;
        gamePlayUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    // Slide out main menu and bring in record UI
    public void MenuOutaddPlayerIn()
    {
        mainMenu.DOAnchorPos(offScreenPos, 0.25f);
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

        public void GamePlayUIOutAddPlayerIn()
    {
        gamePlayUI.DOAnchorPos(offScreenPos, 0.25f);
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
        countdown.HideInstructionTexts();
        countdown.ResetTime();
        canTap = false;
    }

    //////////////////////////////////////////////////////////////////
    // Slide out addPlayerUI and bring back MainMenu
    public void MenuInaddPlayerOut()
    {
        addPlayerUI.DOAnchorPos(offScreenPos, 0.25f);
        mainMenu.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    //////////////////////////////////////////////////////////////////
    /// Beginning of Game Over UI Handling

    public void GameOverInGamePlayUIOut()
    {
       StartCoroutine(kaBOom());
    }

    IEnumerator kaBOom()
    {
        yield return new WaitForSeconds(0.30f);
        gameOverUI.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack);
        gamePlayUI.DOAnchorPos(offScreenPos, 0.5f);
    }

     public void GameOverOutCountdownIn()
    {
        gamePlay.isGameOver = false;
        gameOverUI.DOScale(Vector2.zero, 0.25f).SetEase(Ease.InBack);
        

        countdownCanvasGroup.DOFade(1f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            countdownCanvasGroup.interactable = true;
            countdownCanvasGroup.blocksRaycasts = true;
        });
    }

        public void GameUIOutAddPlayerIn()
    {
        countdown.ResetTimer();
        gamePlay.isGameOver = false;
        gamePlayUI.DOAnchorPos(offScreenPos, 0.25f);
        canTap = false;
        
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

        public void GameOverUIOutAddPlayerIn()
    {
        countdown.ResetTime();
        gamePlay.isGameOver = false;
        canTap = false;
        
        gameOverUI.DOScale(Vector2.zero, 0.25f).SetEase(Ease.InBack);
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }
    

    /// End of Game Over UI Handling
    //////////////////////////////////////////////////////////////////
    
    public void addPlayerOutSelectThemeIn()
    {
        if(addPlayer.addedPlayer == false)
            return;
        
        addPlayerUI.DOAnchorPos(offScreenPos, 0.25f);
        selectThemeUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f); 
    }

    public void addPlayerOutmainMenuIn()
    {
        addPlayerUI.DOAnchorPos(offScreenPos, 0.25f);
        mainMenu.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    public void addPlayerInInstructionOut()
    {
        instructionUI.DOAnchorPos(offScreenPos, 0.25f);
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    public void selectThemeUIOutinstructionUIIn()
    {
        selectThemeUI.DOAnchorPos(offScreenPos, 0.25f);
        instructionUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    public void selectThemeUIOutaddPlayerUIIn()
    {
        selectThemeUI.DOAnchorPos(offScreenPos, 0.25f);
        addPlayerUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    // Slide out main menu and bring in rules UI
    public void MenuOutinstructionIn()
    {
        mainMenu.DOAnchorPos(offScreenPos, 0.25f);
        instructionUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    // Slide out rules UI and bring back main menu
    public void MenuIninstructionOut()
    {
        instructionUI.DOAnchorPos(offScreenPos, 0.25f);
        mainMenu.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    // Transition to gameplay UI and hide difficulty UI
    public void GamePlayInSettingsOut()
    {
        selectThemeUI.DOAnchorPos(offScreenPos, 0.25f);
        gamePlayUI.DOAnchorPos(onScreenPos, 0.25f).SetDelay(0.25f);
    }

    public void doNotShowAgain()
    {
        doNotShow = true;
    }    

    // Slide in settings UI
    public void SlideSettingsUI()
    {
        // Fade in the settings UI
        gameSettingsCanvasGroup.DOFade(1f, 0.25f).SetEase(Ease.OutQuad).OnStart(() =>
        {
            gameSettingsCanvasGroup.interactable = true;
            gameSettingsCanvasGroup.blocksRaycasts = true;
        });
    }

    public void MainSlideSettingsUI()
    {
        // Fade in the settings UI
        gameSettingsCanvasGroup.DOFade(1f, 0.25f).SetEase(Ease.OutQuad).OnStart(() =>
        {
            gameSettingsCanvasGroup.interactable = true;
            gameSettingsCanvasGroup.blocksRaycasts = true;
        });

        // Hide the continue button
        continueButton.SetActive(false);
    }

    IEnumerator hideButton()
    {
        yield return new WaitForSeconds(0.4f);

        // Show the continue button
        continueButton.SetActive(true);
    }

    public void HideSettingsUI()
    {
        // Fade out the settings UI
        gameSettingsCanvasGroup.DOFade(0f, 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            gameSettingsCanvasGroup.interactable = false;
            gameSettingsCanvasGroup.blocksRaycasts = false;
        });
    }


    // Slide out settings UI
    public void UndoSettingsUI()
    {
        // Set continue button active if it is inactive
        if(continueButton.activeSelf == false)
        {
            StartCoroutine(hideButton());
        }

        gameSettingsCanvasGroup.DOFade(0f, 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                gameSettingsCanvasGroup.interactable = false;
                gameSettingsCanvasGroup.blocksRaycasts = false;
        });
    }
}
