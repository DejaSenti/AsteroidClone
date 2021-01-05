using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class AnnouncingManager : MonoBehaviour
{
    private const float ANNOUNCEMENT_DISPLAY_TIME = 1.5f;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    public void Announce(string announcement, UnityAction action)
    {
        Announcements.text = announcement;
        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(delegate { OnAnnouncementDisplayTimerElapsed(action); });
    }

    private void OnAnnouncementDisplayTimerElapsed(UnityAction action)
    {
        Announcements.text = "";

        action.Invoke();

        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(delegate { OnAnnouncementDisplayTimerElapsed(action); });
    }

    public virtual void Terminate()
    {
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveAllListeners();
    }
}