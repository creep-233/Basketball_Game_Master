using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeybindingsManager : MonoBehaviour
{
    public Text keybindingsText;
    private Dictionary<string, string> keybindings = new Dictionary<string, string>();
    private bool isWaitingForKey = false;
    private string actionNameToChange;

    private void Start()
    {
        // 初始化键绑定
        InitializeKeybindings();

        // 显示当前的键绑定
        UpdateKeybindingsDisplay();
    }

    private void InitializeKeybindings()
    {
        // 从 PlayerPrefs 中加载当前的键绑定，如果不存在则使用默认值
        keybindings["MoveForward"] = PlayerPrefs.GetString("Keybinding_MoveForward", "W");
        keybindings["MoveBackward"] = PlayerPrefs.GetString("Keybinding_MoveBackward", "S");
        keybindings["MoveLeft"] = PlayerPrefs.GetString("Keybinding_MoveLeft", "A");
        keybindings["MoveRight"] = PlayerPrefs.GetString("Keybinding_MoveRight", "D");
        keybindings["Jump"] = PlayerPrefs.GetString("Keybinding_Jump", "Space");
        keybindings["Throw"] = PlayerPrefs.GetString("Keybinding_Throw", "Mouse0"); // 默认为长按鼠标左键
    }

    private void UpdateKeybindingsDisplay()
    {
        // 显示当前的键绑定
        keybindingsText.text = "Current Keybindings:\n";
        foreach (var pair in keybindings)
        {
            keybindingsText.text += pair.Key + ": " + pair.Value + "\n";
        }
    }

    public void ChangeKeybinding(string actionName)
    {
        if (!isWaitingForKey)
        {
            // 等待用户输入新的按键
            isWaitingForKey = true;
            actionNameToChange = actionName;
        }
    }

    private void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // 获取用户按下的按键并保存到 PlayerPrefs 中
                string keyName = e.keyCode.ToString();
                keybindings[actionNameToChange] = keyName;
                PlayerPrefs.SetString("Keybinding_" + actionNameToChange, keyName);
                PlayerPrefs.Save();

                // 更新显示当前的键绑定
                UpdateKeybindingsDisplay();

                isWaitingForKey = false;
            }
        }
    }
}
