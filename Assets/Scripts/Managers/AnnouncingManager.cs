using TMPro;
using UnityEngine;

public abstract class AnnouncingManager : MonoBehaviour
{
    private const float ANNOUNCEMENT_DISPLAY_TIME = 1.5f;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    public void Announce(string announcement)
    {
        Announcements.text = announcement;
        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);
    }

    public virtual void OnAnnouncementDisplayTimerElapsed()
    {
        Announcements.text = "";

        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }

    public virtual void Terminate()
    {
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveAllListeners();
    }
}