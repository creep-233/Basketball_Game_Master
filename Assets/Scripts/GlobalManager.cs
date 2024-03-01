using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;

    // 设置初始球的数量和分数
    public int initialBallNum = 5;
    public int initialScore = 0;
    // 全局变量
    private int score = 0;
    private int ballNum = 5;

    public static GlobalManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GlobalManager";
                    instance = obj.AddComponent<GlobalManager>();
                }
            }
            return instance;
        }
    }

    // 可以通过公共方法来访问和修改全局变量
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public void IncreaseScore(){
        score+=5;
    }

    public int BallNum
    {
        get { return ballNum; }
        set { ballNum = value; }
    }

    // 在这里可以添加其他方法和功能
    // 减少球的数量
    public void ResetGlobalVariables()
    {
        // 重新设置所有全局变量的初始值
        ballNum = initialBallNum;
        score = initialScore;
        // 其他全局变量也可以在这里重置
    }
    public void DecreaseBallNum()
    {
        if (ballNum >= 0)
        {
            ballNum -= 1;
        }
        else
        {
            Debug.LogWarning("Attempted to decrease ballNum below 0!");
            ballNum = 0;
        }
    }    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
