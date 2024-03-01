using UnityEngine;

public class ProjectileTrajectory : MonoBehaviour
{
    public int numberOfPoints = 10; // 控制辅助线上的点的数量
    public float timeBetweenPoints = 0.1f; // 控制辅助线上点之间的时间间隔
    public Vector3 initialVelocity; // 初始速度

    private LineRenderer lineRenderer;

    private void Start()
    {
        // 获取 LineRenderer 组件
        lineRenderer = GetComponent<LineRenderer>();
        // 设置线的位置点数量
        lineRenderer.positionCount = numberOfPoints;
        // 设置辅助线的起始位置为物体的位置
        lineRenderer.SetPosition(0, transform.position);
    }

    private void Update()
    {
        // 检查标签是否为 "Basketball"，以及 lineRenderer 是否存在
        if (gameObject.CompareTag("Basketball") && lineRenderer != null)
        {
            // 启用 LineRenderer
            lineRenderer.enabled = true;
            // 绘制弹道路径
            DrawProjectilePath();
        }
        else
        {
            // 如果不是 "Basketball" 标签或 lineRenderer 不存在，则禁用 LineRenderer
            lineRenderer.enabled = false;
        }
    }

    private void DrawProjectilePath()
    {
        Vector3[] positions = new Vector3[numberOfPoints];

        float timeStep = timeBetweenPoints;
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = initialVelocity;

        for (int i = 0; i < numberOfPoints; i++)
        {
            // 计算当前时间步骤下的位置
            Vector3 nextPosition = CalculateNextPosition(currentPosition, currentVelocity, timeStep);

            // 更新 positions 数组
            positions[i] = nextPosition;

            // 更新当前位置和速度
            currentPosition = nextPosition;
            currentVelocity += Physics.gravity * timeStep;
        }

        lineRenderer.SetPositions(positions);
    }

    // 根据当前位置、速度和时间步骤计算下一个位置
    private Vector3 CalculateNextPosition(Vector3 currentPosition, Vector3 currentVelocity, float timeStep)
    {
        // 使用物体的运动方程来计算下一个位置
        Vector3 nextPosition = currentPosition + currentVelocity * timeStep + 0.5f * Physics.gravity * timeStep * timeStep;
        return nextPosition;
    }
}
