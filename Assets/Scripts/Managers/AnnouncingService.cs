using System;
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
        LevelManager.EndLevelEvent.AddListener(OnEndLevel);
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
        if (AnnouncementQueue.Count == 0)
            return;

        Announcements.text = AnnouncementQueue[0];
        AnnouncementQueue.RemoveAt(0);

        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);
    }

    private void Clear()
    {
        Announcements.text = "";
    }

    public void OnAnnouncementDisplayTimerElapsed()
    {
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);

        if (Announcements.text == GAME_OVER_MESSAGE)
            GameOverMessageOverEvent.Invoke();

        if (Announcements.text == LEVEL_MESSAGE + LevelManager.Level)
            LevelMessageOverEvent.Invoke();

        if (AnnouncementQueue.Count > 0)
        {
            Write();
        }
        else
        {
            Clear();
        }
    }

    public void AnnounceGameOver()
    {
        Announce(GAME_OVER_MESSAGE);
    }

    public void AnnounceLevel(int level)
    {
        Announce(LEVEL_MESSAGE + level);
    }

    private void OnEndLevel(string destroyerTag)
    {
        switch (destroyerTag)
        {
            case Tags.PLAYER_BULLET:
                Announce(SHARPSHOOTER_MESSAGE);
                break;
            case Tags.PLAYER:
                Announce(KAMIKAZE_MESSAGE);
                break;
            case Tags.ALIEN_SHIP_BULLET:
                Announce(UNDERDOG_MESSAGE);
                break;
            case Tags.ALIEN_SHIP:
                Announce(SLEEPING_BEAUTY_MESSAGE);
                break;
        }
    }

    public void Terminate()
    {
        LevelManager.EndLevelEvent.RemoveListener(OnEndLevel);

        AnnouncementQueue.Clear();

        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }
}