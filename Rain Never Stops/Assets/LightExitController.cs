using System.Collections;
using UnityEngine;

public class LightExitController : MonoBehaviour
{
    [SerializeField] private SceneManagement sceneManager;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //停止player一切行为，隐藏一切多余的ui
            StartCoroutine(GoToNextScene());
            
        }
            

    }
    IEnumerator GoToNextScene()
    {
        
        yield return new WaitForSeconds(2f);
        sceneManager.GoToNextScene();

        
    }
}
