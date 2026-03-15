using UnityEngine;
using UnityEngine.Events;

public class TimeCircling : MonoBehaviour
{
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float humidity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        humidity += Time.deltaTime;
        PlayerStateManagement.humidity = humidity;

        PlayerStateManagement.ChangeState();
        PlayerStateManagement.ChangeBehaviorState();
        
    }
}
