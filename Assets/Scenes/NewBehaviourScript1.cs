using UnityEngine;

public class SimpleAnimationControl : MonoBehaviour
{
    public AnimationClip animClip; // �����Ķ���Ƭ�α���
    public Animation anim;


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        // ����Ƿ������ض�����������ո��
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ������Ϊ"1r"�Ķ���
           anim.Play(animClip.name);
        }
    }
}
