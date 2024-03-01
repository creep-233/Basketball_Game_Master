using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{ 
    public Animator transition;
    float transitionTime = 1f;
    AudioManager audioManager;
    public ParticleSystem particleEffect; // 按钮点击时播放的粒子效果

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void PlaySound()
    {     
        if(audioManager != null)
        {
            audioManager.PlaySFX(audioManager.clickBtn);
        }
        else
        {
            Debug.LogWarning("AudioManager is not initialized.");
        }

        // 播放粒子效果
        if (particleEffect != null)
        {
            // 实例化粒子效果并设置位置为按钮的位置
            ParticleSystem particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
            
            // 播放粒子效果
            particle.Play();
            
            // 5秒后销毁粒子效果
            Destroy(particle.gameObject, 5f);
        }
        else
        {
            Debug.LogWarning("Particle effect is not assigned.");
        }

    }

    // IEnumerable TransitionAni()
    // {
    //     transition.SetTrigger("Start");
    //     yield return new WaitForSeconds(transitionTime);
    // }
}
