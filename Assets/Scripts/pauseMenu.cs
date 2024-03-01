using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerCameraController cameraController; // 引用 PlayerCameraController 脚本
    public Slider speedSlider; // 引用进度条

    private bool isPaused = false;
    public Movement playerMovement; // 引用 Movement 脚本

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        // 禁用/启用 PlayerCameraController 组件
        if (cameraController != null)
        {
            cameraController.enabled = !isPaused;
        }
        else
        {
            Debug.LogError("PlayerCameraController not assigned!");
        }

        // 禁用/启用玩家运动
        if (playerMovement != null)
        {
            playerMovement.enabled = !isPaused;
        }

        // 设置进度条的交互状态
        speedSlider.interactable = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        // 禁用/启用 PlayerCameraController 组件
        if (cameraController != null)
        {
            cameraController.enabled = !isPaused;
        }
        // 控制鼠标光标的锁定和可见性
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // 当恢复游戏时，重新启用 PlayerCameraController 和 Movement 组件
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
