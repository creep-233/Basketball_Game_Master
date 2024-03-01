using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loader : MonoBehaviour
{

    public Animator transition;
    public float trainsitionTime=1f;

    public void TrainsitionScene(){
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadNextScene(int sceneIndex){

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(trainsitionTime);
        SceneManager.LoadScene(sceneIndex);

    }
}
