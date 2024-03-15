using UnityEngine;

public class PowerCharge : MonoBehaviour
{
    public float maxPower = 10f; // �������ֵ
    private float currentPower = 0f; // ��ǰ����ֵ
    private bool isCharging = false; // �Ƿ���������
    private float chargeRate = 1f; // ��������

    // ���º����м���Ƿ���������
    void Update()
    {
        // ���°�����ʼ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCharging = true;
        }

        // �����ͷ�ʱִ�в���
        if (Input.GetKeyUp(KeyCode.F))
        {
            isCharging = false;
            PerformShot(); // ִ���������
            currentPower = 0f; // ��������ֵ
        }

        // �����������������������ֵ
        if (isCharging)
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Min(currentPower, maxPower); // ��������ֵ���������ֵ
        }
    }

    // ִ�����������������Ը�������ֵ����������ȵ�
    void PerformShot()
    {
        Debug.Log("���! ����ֵ: " + currentPower);
        // ����currentPower������Ͷ������������Ч��
    }
}
