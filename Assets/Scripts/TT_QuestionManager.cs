using System.Collections.Generic;
using UnityEngine;

public class TT_QuestionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<string> MostLikely = new List<string>()
    {
        "Who's most likely to work overtime without getting paid",
        "Who's most likely to quit their job on a whim?",
        "Who's most likely to become a CEO?",
        "Who's most likely to fall asleep during a meeting?",
        "Who's most likely to be late for work every day?",
        "Who's most likely to miss your wedding?",
        "Who's most likely to ghost someone after a first date?",
        "Who's most likely to get married in Vegas?",
        "Who's most likely to forget your birthday?",
        "Who's most likely to cry at a rom-com?",
        "Who's most likely to eat their birthday cake alone?",
        "Who's most likely to steal food off someone else's plate?",
        "Who's most likely to order takeout every day?",
        "Who's most likely to burn their dinner?",
        "Who's most likely to finish a whole pizza by themselves?",
        "Who's most likely to lose their phone?",
        "Who's most likely to start a conversation with a stranger?",
        "Who's most likely to laugh at a bad joke?",
        "Who's most likely to become famous?",
        "Who's most likely to forget their wallet at home?",
        "Who's most likely to backpack across the world?",
        "Who's most likely to get lost in their own city?",
        "Who's most likely to plan a spontaneous road trip?",
        "Who's most likely to cancel travel plans last minute?",
        "Who's most likely to move to another country?",
        "Who's most likely to sing karaoke in public?",
        "Who's most likely to dye their hair a crazy color?",
        "Who's most likely to binge-watch a series in one night?",
        "Who's most likely to accidentally send a text to the wrong person?",
        "Who's most likely to win a lottery and lose the ticket?",
        "Who's most likely to retire early?",
        "Who's most likely to adopt ten pets?",
        "Who's most likely to start their own business?",
        "Who's most likely to move off the grid?",
        "Who's most likely to run for public office?",
        "Who's most likely to trip over their own feet?",
        "Who's most likely to adopt an unusual hobby?",
        "Who's most likely to get stuck in an elevator?",
        "Who's most likely to spend hours on a pointless internet rabbit hole?",
        "Who's most likely to prank their friends?",
        "Who's most likely to start their own company?",
        "Who's most likely to get fired on their first day?",
        "Who's most likely to become a billionaire?",
        "Who's most likely to win an employee of the month award?",
        "Who's most likely to become a motivational speaker?",
        "Who's most likely to forget about an important meeting?",
        "Who's most likely to stay at the same job forever?",
        "Who's most likely to change careers five times?",
        "Who's most likely to invent something that changes the world?",
        "Who's most likely to fake being sick to avoid work?",
        "Who's most likely to have a whirlwind romance?",
        "Who's most likely to fall in love with a celebrity?",
        "Who's most likely to elope?",
        "Who's most likely to get married first?",
        "Who's most likely to get engaged and call it off?",
        "Who's most likely to stay single forever?",
        "Who's most likely to forget their partner's anniversary?",
        "Who's most likely to write a love poem?",
        "Who's most likely to propose in a very public way?",
        "Who's most likely to date their best friend?",
        "Who's most likely to organize a surprise party?",
        "Who's most likely to bail on plans last minute?",
        "Who's most likely to skip leg day?",
        "Who's most likely to win an argument?",
        "Who's most likely to start a group chat?",
        "Who's most likely to keep a secret forever?",
        "Who's most likely to borrow something and forget to return it?",
        "Who's most likely to be the peacemaker in a fight?",
        "Who's most likely to always remember everyone's birthday?",
        "Who's most likely to bring snacks to a gathering?",
        "Who's most likely to invite a random stranger to hang out?",
        "Who's most likely to visit all seven continents?",
        "Who's most likely to forget their passport at the airport?",
        "Who's most likely to travel to space?",
        "Who's most likely to plan a trip but never go?",
        "Who's most likely to spend all their money on travel?",
        "Who's most likely to go on an adventure?",
        "Who's most likely to have the unhealthiest diet?",
        "Who's most likely to live out of a van in a bush?",
        "Who's most likely to move to a tropical island?",
        "Who's most likely to befriend a local on a trip?",
        "Who's most likely to take 1,000 photos on a vacation?",
        "Who's most likely to talk to animals as if they understand?",
        "Who's most likely to buy something ridiculous online?",
        "Who's most likely to spend a day doing absolutely nothing?",
        "Who's most likely to fall asleep during a movie?",
        "Who's most likely to eat a whole cake by themselves?",
        "Who's most likely to laugh at their own jokes?",
        "Who's most likely to adopt a strange pet?",
        "Who's most likely to dress up for no reason?",
        "Who's most likely to forget where they parked their car?",
        "Who's most likely to become a memer?",
        "Who's most likely to become president?",
        "Who's most likely to retire before 40?",
        "Who's most likely to write a bestselling book?",
        "Who's most likely to live to be 100 years old?",
        "Who's most likely to own a private jet?",
        "Who's most likely to become a teacher?",
        "Who's most likely to live off the grid?",
        "Who's most likely to adopt someone's kid?",
        "Who's most likely to make a joke at the worst time?",
        "Who's most likely to overthink a small situation?",
        "Who's most likely to always be late?",
        "Who's most likely to never answer their phone?",
        "Who's most likely to befriend a celebrity?",
        "Who's most likely to accidentally start a fire?",
        "Who's most likely to trip and fall in public?",
        "Who's most likely to sing in the shower loudly?",
        "Who's most likely to lose their keys every day?",
        "Who's most likely to lock themselves out of their house?",
        "Who's most likely to talk during a movie?",
        "Who's most likely to prank someone?",
        "Who's most likely to forget what they were saying mid-sentence?",
        "Who's most likely to break a world record?",
        "Who's most likely to lose a game and blame the rules?",
        "Who's most likely to post a selfie during a natural disaster?",
        "Who's most likely to walk into the wrong bathroom?",
        "Who's most likely to forget someone's name right after meeting them?",
        "Who's most likely to text their ex while drunk?",
        "Who's most likely to get caught talking to themselves?",
        "Who's most likely to accidentally like an old Instagram post while stalking someone?",
        "Who's most likely to spill their drink on someone at a party?",
        "Who's most likely to show up to an event in the wrong outfit?",
        "Who's most likely to forget they were supposed to bring a gift to a party?",
        "Who's most likely to trip over their own shoelaces?",
        "Who's most likely to join a cult?",
        "Who's most likely to talk to a ghost if they see one?",
        "Who's most likely to survive a zombie apocalypse?",
        "Who's most likely to buy a weird gadget they'll never use?",
        "Who's most likely to name their pet something ridiculous?",
        "Who's most likely to randomly burst into song in public?",
        "Who's most likely to argue that time travel is real?",
        "Who's most likely to have a collection of something odd?",
        "Who's most likely to eat dessert before dinner?",
        "Who's most likely to order the same meal every time?",
        "Who's most likely to experiment with a crazy food combination?",
        "Who's most likely to burn water while cooking?",
        "Who's most likely to hoard snacks in their room?",
        "Who's most likely to eat an entire tub of ice cream in one sitting?",
        "Who's most likely to win a hotdog-eating contest?",
        "Who's most likely to forget they ordered food delivery?",
        "Who's most likely to eat something they dropped on the floor?",
        "Who's most likely to try a fad diet just for fun?",
        "Who's most likely to spend all day in pajamas?",
        "Who's most likely to fall asleep on the couch during a party?",
        "Who's most likely to call someone to pick up something they forgot 5 feet away?",
        "Who's most likely to leave dishes in the sink for a week?",
        "Who's most likely to procrastinate until the very last minute?",
        "Who's most likely to pretend to be busy to avoid plans?",
        "Who's most likely to spend hours scrolling on their phone?",
        "Who's most likely to cancel plans just to stay home?",
        "Who's most likely to take a nap in the middle of the day for no reason?",
        "Who's most likely to wear the same outfit two days in a row?",
        "Who's most likely to skydive?",
        "Who's most likely to swim with sharks?",
        "Who's most likely to ride the scariest rollercoaster?",
        "Who's most likely to try bungee jumping?",
        "Who's most likely to climb Mount Everest?",
        "Who's most likely to book a spontaneous trip to another country?",
        "Who's most likely to survive in the wilderness for a week?",
        "Who's most likely to jump off a cliff into water?",
        "Who's most likely to sign up for an extreme obstacle course?",
        "Who's most likely to explore a haunted house alone?",
        "Who's most likely to go viral on TikTok?",
        "Who's most likely to forget their password every time?",
        "Who's most likely to watch the entire Movie Series in one weekend?",
        "Who's most likely to build a robot?",
        "Who's most likely to buy the latest gadget the moment it's released?",
        "Who's most likely to watch YouTube videos all day?",
        "Who's most likely to accidentally send an embarrassing email at work?",
        "Who's most likely to write fan fiction about their favorite show?",
        "Who's most likely to forget there's a test?",
        "Who's most likely to fall asleep in class?",
        "Who's most likely to finish an assignment the night before?",
        "Who's most likely to write a fake doctor's note to skip school?",
        "Who's most likely to organize a study group and not show up?",
        "Who's most likely to get caught cheating?",
        "Who's most likely to become the teacher's favorite?",
        "Who's most likely to lose their school ID?",
        "Who's most likely to be rocking a face full of makeup, even for a swim?"

        // I'll stop here for the mean time!



    };

    private List<string> NameThreeThings = new List<string>()
    {
        "Name three things you would bring to a deserted island.",
        "Name three foods you could eat for the rest of your life.",
        "Name three animals you'd want as pets if money wasn't an issue.",
        "Name three animals starting with letter 'G'",
        "Name three birds",
        "Name three animals commonly found in a desert",
        "Name three things you'd never do for a million dollars.",
        "Name three things you'd do with a million dollars.",
        "Name three worst gifts you ever received",
        "Name three words starting with letter 'K'",
        "Name three things with a knob.",
        "Name three types of nut.",
        "Name three continents",
        "Name three things you can chew on",
        "Name three characters from the Bible",
        "Name three upper body parts",
        "Name three people that are smarter than you",
        "Name three European countries",
        "Name three jobs that pay well",
        "Name three capital cities",
        "Name three shoe brands",
        "Name three movies with talking animals",
        "Name three football teams",
        "Name three cleaning tools",
        "Name three things you can find in a school",
        "Name three things you can find in a market",
        "Name three things you can find in an airplane",
        "Name three things you can find in a river",
        "Name three things you can find in a serial killer's house",
        "Name three words that rhyme with 'how'",
        "Name three means of transport",
        "Name three soccer stadiums",
        "Name three boy names",
        "Name three girl names",
        "Name three African dishes",
        "Name three European dishes",
        "Name three things made out of glass",
        "Name three French words",
        "Name three places where people keep their money",
        "Name three planets",
        "Name three Spanish words",
        "Name three ways to become a millionaire",
        "Name three animals with two legs",
        "Name three things you drive",
        "Name three things you can't resist",
        "Name three Christmas songs",
        "Name three types of fish",
        "Name three things made out of rubber",
        "Name three ball sports",
        "Name three colours that are not in the rainbow",
        "Name three animals that lay eggs",
        "Name three free things",
        "Name three things made out of wood",
        "Name three individual sports",
        "Name three things that are hot",
        "Name three things that are cold",
        "Name three Asian countries",
        "Name three African countries",
        "Name three reptiles",
        "Name three tech jobs",
        "Name three mammals",
        "Name three things you can find around here",
        "Name three things that are always in your fridge.",
        "Name three songs you would play on repeat forever.",
        "Name three movies you could watch over and over again.",
        "Name three yellow foods",
        "Name three words Disney songs",
        "Name three words disney songs",
        "Name three professions",
        "Name three words starting with letter 'K'",
        "Name three words starting with letter 'C'",
        "Name three things you can't live without.",
        "Name three people you'd invite to a dinner party.",
        "Name three things you wish you knew how to do.",
        "Name three things you'd buy if you won the lottery.",
        "Name three excuses you would use to avoid going out.",
        "Name three things that always make you laugh.",
        "Name three things you'd do if you woke up with superpowers.",
        "Name three fruits starting with letter 'P'",
        "Name three things you'd never want to be famous for.",
        "Name three things that are guaranteed to embarrass you.",
        "Name three things you'd do if you were invisible for a day.",
        "Name three people you would call in a crisis.",
        "Name three things that should never be mixed together.",
        "Name three things you're afraid of, even though you know they're not dangerous.",
        "Name three things you're not afraid of, even though you know they're dangerous.",
        "Name three places you'd love to travel to.",
        "Name three countries you think are overrated.",
        "Name three things you would take on a road trip.",
        "Name three things you would do if you were a tourist in your own city.",
        "Name three places you'd hide if a zombie apocalypse happened.",
        "Name three animals starting with letter 'L'",
        "Name three countries you would never visit.",
        "Name three ways to travel the world without flying.",
        "Name three people you'd take with you on a backpacking adventure.",
        "Name three things that could ruin a first date.",
        "Name three things you'd do if you turned into the opposite gender for a day.",
        "Name three things you've done that you would never do again.",
        "Name three words starting with letter 'Q'",
        "Name three embarrassing moments you could laugh about now.",
        "Name three things that would make you scream.",
        "Name three things you've accidentally broken.",
        "Name three things you can never remember where you put.",
        "Name three bad habits you need to break.",
        "Name three things you do when no one's watching.",
        "Name three habits you have that annoy others.",
        "Name three things you're really good at, but no one knows about.",
        "Name three things you do when you're bored.",
        "Name three ways to make a boring day fun.",
        "Name three things that make life more interesting.",
        "Name three things you hope to do before you die.",
        "Name three things you've done that made you feel brave.",
        "Name three words starting with letter 'I'",
        "Name three apps you use every day.",
        "Name three things you can't live without in the digital age.",
        "Name three fictional characters you'd invite to dinner.",
        "Name three social media platforms you use the most.",
        "Name three things you'd do if you became the President for a day.",
        "Name three superpowers you wish you had.",
        "Name three animals you could use as your personal bodyguards.",
        "Name three things you'd do if you woke up with the ability to speak every language.",
        "Name three scenarios you'd create if you had the power to control time.",
        "Name three things you'd build if you were given unlimited resources.",
        "Name three things you'd change about the world if you had the power to.",
        "Name three places you'd visit if there were no rules or restrictions.",
        "Name three words starting with letter 'U'",
        "Name three things you'd do if you were the only human left on Earth.",
        "Name three things you would bring to a fight with a wild animal.",
        "Name three ways to show someone you love them without saying it.",
        "Name three things that would make you fall in love with someone instantly.",
        "Name three things you'd give to your best friend as a surprise gift.",
        "Name three things you'd want to build if you were an inventor.",
        "Name three objects you'd want in your dream home.",
        "Name three movies you'd make if you were a director.",
        "Name three activities you'd do if you were locked in a room for 24 hours.",
        "Name three things you would do on a day without any rules.",
        "Name three places you'd visit if you could teleport anywhere.",
        "Name three words starting with letter 'O'",
        "Name three things you'd do if you suddenly became a billionaire.",
        "Name three things that should be illegal.",
        "Name three moments in your life you wish you could relive.",
        "Name three things you'd use a robot for if you could.",
        "Name three things you would do if you could swap lives with someone for a week.",
        "Name three historical figures (dead or alive) you would invite to a dinner party.",
        "Name three ways the world would change if everyone could read minds.",
        "Name three things that would happen if all animals could talk.",
        "Name three things you would do if you could breathe underwater for a day.",
        "Name three fictional characters you would want as a roommate.",
        "Name three dishes you could eat for the rest of your life.",
        "Name three unusual food combinations that you actually love.",
        "Name three foods you'd never eat under any circumstances.",
        "Name three feminine names starting with letter 'E'",
        "Name three male names starting with letter 'G'",
        "Name three things you do every morning without fail.",
        "Name three modes of transportation you'd love to try.",
        "Name three things that could instantly make your day better.",
        "Name three things you would do if you could talk to animals.",
        "Name three things you think would happen if gravity was turned off for a day.",
        "Name three people who inspire you to be better.",
        "Name three things you want to achieve by the end of this year.",
        "Name three animals you think are underrated.",
        "Name three facts about space that you find mind-blowing.",
        "Name three things you think could exist on other planets.",
        "Name three male names starting with letter 'S'",
        "Name three things you would take with you if you had to survive in the wilderness for a week.",
        "Name three ways you would spend your last day on Earth.",
        "Name three people you would want to be stuck in an elevator with.",
        "Name three cartoons you loved growing up.",
        "Name three childhood friends you still keep in touch with.",
        "Name three things that remind you of your childhood.",
        "Name three memories you will always cherish from your childhood.",
        "Name three things that have stayed with you since your childhood.",
        "Name three favorite snacks you ate as a kid.",
        "Name three clothing items you can't live without.",
        "Name three colors you think always look good together.",
        "Name three things that are always in your bag or pocket when you leave the house.",
        "Name three things you think are more important than money.",
        "Name three questions you wish you had the answers to.",
        "Name three female names starting with letter 'B'",
        "Name three people you'd love to meet in person.",
        "Name three things that should never be done in public.",
        "Name three ways to waste money",
        "Name three words starting with letter 'K'",

    };
    public string GetMostLikelyQuestion(int index)
    {
        if (index >= 0 && index < MostLikely.Count)
        {
            return MostLikely[index];
        }
        return "No question available.";
    }

    public string GetNameThreeThingsQuestion(int index)
    {
        if (index >= 0 && index < NameThreeThings.Count) // Correct list used
        {
            return NameThreeThings[index]; // Correct list used
        }
        return "No question available.";
    }

    public int GetQuestionCount()
    {
        return MostLikely.Count;
    }

    public int GetNameThreeThingsQuestionCount()
    {
        return NameThreeThings.Count;
    }

}
