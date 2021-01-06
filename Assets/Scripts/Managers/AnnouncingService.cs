using TMPro;
using UnityEngine;

public class AnnouncingService : MonoBehaviour
{
    private const string GAME_OVER_MESSAGE = "Game Over";
    private const string LEVEL_MESSAGE = "Level ";

    private const float ANNOUNCEMENT_DISPLAY_TIME = 1.5f;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    public void AnnounceGameOver()
    {
        Announce(GAME_OVER_MESSAGE);
    }

    public void AnnounceLevel(int level)
    {
        Announce(LEVEL_MESSAGE + level);
    }

    private void Announce(string announcement)
    {
        Announcements.text = announcement;
        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);
    }

    public void OnAnnouncementDisplayTimerElapsed()
    {
        Announcements.text = "";

        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }

    public void Terminate()
    {
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveAllListeners();
    }
}