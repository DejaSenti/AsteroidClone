using UnityEngine;

public class UIInstructions : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject mainOverlay;
#pragma warning restore 0649

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }
}
