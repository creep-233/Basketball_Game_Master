using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        SceneManager.LoadScene(1);
    }
    public void Exit(){
        Application.Quit();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
