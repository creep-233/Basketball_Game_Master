using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public static GameInfo instance; // 静态实例
    public int ballCount = 5;
    public Text ballSum; // 引用 BallSum 的 Text 组件
    public int levelNum; // 用于存储关卡数
    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        instance = this; // 初始化静态实例
        levelNum = PlayerPrefs.GetInt("LevelNum"); // 获取或设置关卡数，默认为 1
    }

    private void Update()
    {
        UpdateBallSumText(); // 更新 BallSum 的 Text 组件
        CheckBallCount(); // 检查球的数量
    }

    // 更新 BallSum 的 Text 组件的方法
    public void UpdateBallSumText()
    {
        ballSum.text = ballCount.ToString(); // 更新文本内容
    }

    // 检查球的数量，如果为 0，则重新加载场景并减少关卡数
    private void CheckBallCount()
    {
        if (ballCount == 0)
        {
            levelNum--; // 减少关卡数
            PlayerPrefs.SetInt("LevelNum", levelNum); // 保存关卡数到 PlayerPrefs
            PlayerPrefs.Save(); // 保存 PlayerPrefs 的修改

            if (levelNum <= 0)
            {
                LoadEndScene(); // 如果关卡数小于等于 0，加载结束场景
            }
            else
            {
                ReloadScene(); // 否则重新加载场景
            }

            audioManager.PlaySFX(audioManager.timeUp);
        }
    }

    // 重新加载场景的方法
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 加载结束场景的方法
    private void LoadEndScene()
    {
        SceneManager.LoadScene(2); // 加载结束场景
    }
}
