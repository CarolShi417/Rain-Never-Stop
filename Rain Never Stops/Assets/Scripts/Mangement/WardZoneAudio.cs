using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WardZoneAudio : MonoBehaviour
{
    //public GameObject dialoguePanel;
    public AudioSource teleAudioSource;//电话铃声

    


    void Start()
    {
        //dialoguePanel.SetActive(false);
        //StartCoroutine(ShowPanelAfterDelay());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowPanelAfterDelay()
    {
        // 等2秒
        yield return new WaitForSeconds(2f);

        // 锁定玩家
        PlayerStateManagement.isTeleTrigger = true;

        // 播放电话铃声（循环）
        teleAudioSource.loop = true;
        teleAudioSource.Play();

        // 再等3秒
        yield return new WaitForSeconds(6f);

        // 显示UI
        dialoguePanel.SetActive(true);

        // 停止铃声（掐断）
        teleAudioSource.Stop();

        // 解锁玩家
        PlayerStateManagement.isTeleTrigger = false;
    }

    
}
