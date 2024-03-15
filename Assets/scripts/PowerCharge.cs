using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public float maxPower = 10f; // 最大蓄力值
    private float currentPower = 0f; // 当前蓄力值
    private bool isCharging = false; // 是否正在蓄力
    private float chargeRate = 1f; // 蓄力速率

    // 更新函数中检查是否正在蓄力
    void Update()
    {
        // 按下按键开始蓄力
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCharging = true;
        }

        // 按键释放时执行操作
        if (Input.GetKeyUp(KeyCode.F))
        {
            isCharging = false;
            PerformShot(); // 执行射击动作
            currentPower = 0f; // 重置蓄力值
        }

        // 如果正在蓄力，则增加蓄力值
        if (isCharging)
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Min(currentPower, maxPower); // 限制蓄力值不超过最大值
        }
    }

    // 执行射击动作，这里可以根据蓄力值调整射击力度等
    void PerformShot()
    {
        Debug.Log("射击! 蓄力值: " + currentPower);
        // 根据currentPower来调整投篮力量或其他效果
    }
}
