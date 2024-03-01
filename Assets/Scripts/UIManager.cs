using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPage;
    public GameObject pausePage;

    public void ShowSettingsPage()
    {
        settingsPage.SetActive(true);
        pausePage.SetActive(false);
    }

    public void ShowPausePage()
    {
        pausePage.SetActive(true);
        settingsPage.SetActive(false);
    }

    public void HideAllPages()
    {
        settingsPage.SetActive(false);
        pausePage.SetActive(false);
    }
}
