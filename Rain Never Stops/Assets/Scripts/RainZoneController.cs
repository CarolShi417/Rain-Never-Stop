using UnityEngine;
using UnityEngine.Events;

public class TimeCircling : MonoBehaviour
{
    [SerializeField] private float timer = 0.0f;
    public float rainDuration_stateToHalfwet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= rainDuration_stateToHalfwet && PlayerStateManager.currentState == PlayerState.Normal)
        {
            PlayerStateManager.ChangeState(PlayerState.HalfWet);
        }
    }
}
