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

    private const float ANNOUNCEMENT_DISPLAY_TIME = 1.5f;

    public UnityEvent GameOverMessageOverEvent;
    public UnityEvent LevelMessageOverEvent;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    private List<string> AnnouncementQueue;

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
        AnnouncementQueue = new List<string>();

        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);
    }

    private void Announce(string announcement)
    {
        Enqueue(announcement);

        if (Announcements.text != "")
            return;

        Write();
    }

    private void Enqueue(string announcement)
    {
        AnnouncementQueue.Add(announcement);
    }

    private void Write()
    {
        Announcements.text = AnnouncementQueue[0];
        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
    }

    public void OnAnnouncementDisplayTimerElapsed()
    {
        if (AnnouncementQueue[0] == GAME_OVER_MESSAGE)
            GameOverMessageOverEvent.Invoke();

        if (AnnouncementQueue[0] == LEVEL_MESSAGE + LevelManager.Level)
            LevelMessageOverEvent.Invoke();

        AnnouncementQueue.RemoveAt(0);

        if (AnnouncementQueue.Count == 0)
        {
            Announcements.text = "";
            return;
        }

        Write();
    }

    public void AnnounceGameOver()
    {
        Announce(GAME_OVER_MESSAGE);
    }

    public void AnnounceLevel(int level)
    {
        Announce(LEVEL_MESSAGE + level);
    }

    public void AnnounceSharpshooter()
    {
        Announce(SHARPSHOOTER_MESSAGE);
    }

    public void AnnounceKamikaze()
    {
        Announce(KAMIKAZE_MESSAGE);
    }

    public void AnnounceUnderdog()
    {
        Announce(UNDERDOG_MESSAGE);
    }

    public void AnnounceSleepingBeauty()
    {
        Announce(SLEEPING_BEAUTY_MESSAGE);
    }

    public void Terminate()
    {
        GameOverMessageOverEvent.RemoveAllListeners();
        LevelMessageOverEvent.RemoveAllListeners();

        AnnouncementQueue.Clear();
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }
}