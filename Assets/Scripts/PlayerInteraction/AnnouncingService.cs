using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class AnnouncingService : MonoBehaviour
{
    private const string GAME_OVER_MESSAGE = "Game Over";
    private const string LEVEL_MESSAGE = "Level ";
    private const string SHARPSHOOTER_MESSAGE = "Sharpshooter!";
    private const string KAMIKAZE_MESSAGE = "Kamikaze!";
    private const string UNDERDOG_MESSAGE = "Underdog...";
    private const string SLEEPING_BEAUTY_MESSAGE = "Sleeping Beauty...";

    private const float ANNOUNCEMENT_DISPLAY_TIME_SHORT = 1.5f;
    private const float ANNOUNCEMENT_DISPLAY_TIME_LONG = 3f;

    public UnityEvent GameOverMessageOverEvent;
    public UnityEvent LevelMessageOverEvent;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    private List<string> announcementQueue;
    private List<float> displayTimes;

    private string gameOverMessage;

    private void Awake()
    {
        if (GameOverMessageOverEvent == null)
        {
            GameOverMessageOverEvent = new UnityEvent();
        }

        if (LevelMessageOverEvent == null)
        {
            LevelMessageOverEvent = new UnityEvent();
        }
    }

    public void Initialize()
    {
        announcementQueue = new List<string>();
        displayTimes = new List<float>();
        LevelManager.EndLevelEvent.AddListener(OnEndLevel);
    }

    private void Announce(string announcement, float displayTime)
    {
        Enqueue(announcement, displayTime);

        if (Announcements.text != "")
            return;

        Write();
    }

    private void Enqueue(string announcement, float displayTime)
    {
        announcementQueue.Add(announcement);
        displayTimes.Add(displayTime);
    }

    private void Write()
    {
        if (announcementQueue.Count == 0)
            return;

        Announcements.text = announcementQueue[0];

        AnnouncementDisplayTimer.StartTimer(displayTimes[0]);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);

        announcementQueue.RemoveAt(0);
        displayTimes.RemoveAt(0);
    }

    private void Clear()
    {
        Announcements.text = "";
    }

    public void OnAnnouncementDisplayTimerElapsed()
    {
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);

        if (Announcements.text == gameOverMessage)
            GameOverMessageOverEvent.Invoke();

        if (Announcements.text == LEVEL_MESSAGE + LevelManager.Level)
            LevelMessageOverEvent.Invoke();

        if (announcementQueue.Count > 0)
        {
            Write();
        }
        else
        {
            Clear();
        }
    }

    public void AnnounceGameOver(int score)
    {
        gameOverMessage = GAME_OVER_MESSAGE + "\nYour score is: " + score;
        Announce(gameOverMessage, ANNOUNCEMENT_DISPLAY_TIME_LONG);
    }

    public void AnnounceLevel(int level)
    {
        Announce(LEVEL_MESSAGE + level, ANNOUNCEMENT_DISPLAY_TIME_SHORT);
    }

    private void OnEndLevel(string destroyerTag)
    {
        switch (destroyerTag)
        {
            case Tags.PLAYER_BULLET:
                Announce(SHARPSHOOTER_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.PLAYER_BULLET_GHOST:
                Announce(SHARPSHOOTER_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.PLAYER:
                Announce(KAMIKAZE_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.PLAYER_GHOST:
                Announce(KAMIKAZE_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.ALIEN_SHIP_BULLET:
                Announce(UNDERDOG_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.ALIEN_SHIP_BULLET_GHOST:
                Announce(UNDERDOG_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.ALIEN_SHIP:
                Announce(SLEEPING_BEAUTY_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
            case Tags.ALIEN_SHIP_GHOST:
                Announce(SLEEPING_BEAUTY_MESSAGE, ANNOUNCEMENT_DISPLAY_TIME_LONG);
                break;
        }
    }

    public void Terminate()
    {
        LevelManager.EndLevelEvent.RemoveListener(OnEndLevel);

        Clear();
        announcementQueue.Clear();
        displayTimes.Clear();

        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }
}