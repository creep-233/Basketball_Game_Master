using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        if (toggle == null)
        {
            Debug.LogError("Toggle not set!");
            return;
        }

        // 从 PlayerPrefs 中加载上次保存的状态，并设置 Toggle 的状态
        toggle.isOn = PlayerPrefs.GetInt("TrajectoryState", 0) == 1;

        // 添加监听器，以便在 Toggle 状态改变时保存状态
        toggle.onValueChanged.AddListener((value) => SaveToggleState(value));
    }

    private void SaveToggleState(bool state)
    {
        // 保存 Toggle 的状态到 PlayerPrefs
        PlayerPrefs.SetInt("TrajectoryState", state ? 1 : 0);
        PlayerPrefs.Save();
    }
}
