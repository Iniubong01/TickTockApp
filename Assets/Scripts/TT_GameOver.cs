using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TT_GameOver : MonoBehaviour
{
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text punishmentText;
    private bool hasGameOverLogicRun = false;
    

    private TT_GamePlay gamePlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gamePlay = GameObject.Find("Game Settings").GetComponent<TT_GamePlay>(); 
    }

    private List<string> gameOverTexts = new List<string>()
    {
        "You're wasted! The bomb went off in your hands!",
        "You're toast! You didn't pass the bomb quickly!",
        "Barbeque! Yummy! Haha!",
        "Tick-tock, time's up!",
        "Oops! The bomb got you.",
        "Boom! You snooze, you lose!",
        "Kaboom! That's gonna leave a mark!",
        "Time flies… and so did the bomb!",
        "Blast off! You held on too long!",
        "You were unlucky, now don't cry!",
        "Tick-tick-BOOM! Better luck next time!",
        "Oops-a-daisy! That bomb sure made a scene.",
        "Boom-shaka-laka! That's a wrap for you!",
        "Explosive mistake! Time waits for no one.",
        "Lights out! Should've passed the bomb quicker!",
        "You're a firework… just not the good kind!",
        "Oopsie! You just pulled the short fuse!",
        "Boomtown, population: You alone!",
    };

        private List<string> punishmentTexts = new List<string>()
    {
        "Lucky escape! Pass 2 penalties to any player you choose.",
        "Lucky escape! Pass 3 penalties to any player you choose.",
        "The longer your name, the more you get. You get 1 penalty for every letter.",
        "You and your right hand buddy both get one penalty.",
        "You've lost the round. That's 4 penalties for you!",
        "Oh no! You're hit! Pass 2 penalties to the player who last smiled.",
        "Double trouble! Assign 3 penalties to any two players of your choice.",
        "Penalty Party! Everyone gets 1 penalty… except you!",
        "Oopsie! The person with the shortest name takes 3 penalties!",
        "Boomerang! The player who gave you the bomb gets 2 penalties!",
        "Penalty Roulette! Spin a bottle or point to assign 5 penalties.",
        "You've been caught! The person to your left takes 2 penalties with you.",
        "Oops! Everyone wearing shoes gets 2 penalties each!",
        "Slowpoke! The last player to speak gets 3 penalties.",
        "Too late! Everyone except you gets 1 penalty, but you get 3!",
        "Penalty Surprise! Choose an odd number of penalties for yourself or someone else!",
        "Better luck next time! You get 2 penalties for every round you've survived so far.",
        "Oh no! Everyone wearing blue gets 3 penalties!",
        "Penalty jackpot! You get 5 penalties… unless you can name 3 animals in 5 seconds!",
        "Chain reaction! The player who passed the bomb to you gets 2 penalties, and so does the player before them!",
        "Penalty magnet! Every player adds 1 penalty to your total.",
        "Lucky break! You escape penalties this time!",
        "Name game! The next person to say someone's name takes 2 penalties.",
    };

            private List<string> RushPunishmentTexts = new List<string>()
    {
        "You've lost the round. Everyone in the team gets 2 penalties.",
        "You've lost the round. Everyone in the team gets 3 penalties.",
        "You're responsible for the fallout!",
    };


            private List<string> noPunishmentTexts = new List<string>()
    {
        "There will be no punishment for this round!",
        "You're off the hook for now, but stay sharp!",
        "A free pass this round—enjoy it while it lasts!",
        "No penalty this time—just keep playing smart.",
        "No punishment here, just pure fun!",
        "Phew! No consequences for this one.",
        "Breathe easy, no punishment is coming your way.",
        "Relax! There's no punishment... this time.",
        "Take a breather, no consequences for this one!",
        "You're in the safe zone this round. Well done!",
        "You're walking away clean—for now!",
        "Looks like the punishment fairy is taking a break.",
        "The game has spared you—count your blessings!",
        "Slowpoke! You're in the clear this round—don't get too comfortable.",
        "This round is punishment-free—celebrate responsibly.",
        "The skies are clear—no storms today.",
        "Luck struck! No punishment awaits you this time.",
        "You're clear for this round—stay put!",
        "This round is punishment-free—bask in your good fortune.",
        "No punishment for this round.",
    };

    public void showPunishmentTexts()
    {  
        punishmentText.text = punishmentTexts[Random.Range(0, punishmentTexts.Count)];
    }

        public void showRushPunishmentTexts()
    {  
        punishmentText.text = RushPunishmentTexts[Random.Range(0, RushPunishmentTexts.Count)];
    }

    public void noPunishment()
    {  
        punishmentText.text = noPunishmentTexts[Random.Range(0, noPunishmentTexts.Count)];
    }

    public void showGameOverText()
    {
        gameOverText.text = gameOverTexts[Random.Range(0, gameOverTexts.Count)]; 
    }

    public void ResetTexts()
    {
        punishmentText.text = "";
        gameOverText.text = "";
    }


    public void ifGameOver()
    {
        if (gamePlay.isGameOver)
        {
            if (!hasGameOverLogicRun)
            {
                if(gamePlay.isRush)
                {
                    showRushPunishmentTexts();
                }
                else
                {
                    showPunishmentTexts();
                }

                showGameOverText();
                hasGameOverLogicRun = true; // Run logic only once
            }
        }
        else
        {
            if (hasGameOverLogicRun)
            {
                ResetTexts(); // Reset only when needed
                hasGameOverLogicRun = false;
            }
        }
    }

    public void ifNoPunishment()
    {
        if (gamePlay.isGameOver)
        {
            if (!hasGameOverLogicRun)
            {
                noPunishment();
                showGameOverText();
                hasGameOverLogicRun = true; // Run logic only once
            }
        }
        else
        {
            if (hasGameOverLogicRun)
            {
                ResetTexts(); // Reset only when needed
                hasGameOverLogicRun = false;
            }
        }
    }


}
