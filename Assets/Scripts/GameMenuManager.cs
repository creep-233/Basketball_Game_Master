using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    // Start is called before the first frame update    
    [SerializeField] public Text scoreText; // 用于显示分数的 Text UI 元素
    [SerializeField] public Text ballNumText; // 用于显示篮球数量的 Text UI 元素
    private int score;
    private int ballNum;
    private AudioManager audioManager;

    void Start(){
        audioManager = AudioManager.instance;                       
    }
    void Update()
    {
        // 检查 PlayerPrefs 中的值是否发生了变化，如果发生了变化，则更新 UI 文本
        // 访问全局变量
        score = GlobalManager.Instance.Score;
        ballNum = GlobalManager.Instance.BallNum;

        scoreText.text = "Score: " + score.ToString();
        ballNumText.text = "Ball: " + ballNum.ToString();

        if(ballNum==0){
            audioManager.PlaySFX(audioManager.timeUp);

            // 调用 ResetGlobalVariables 方法重置全局变量
            GlobalManager.Instance.ResetGlobalVariables();
            SceneManager.LoadScene(2);
        }

    }

}
