using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float aliveSpeed = 20f;
    [SerializeField] private Animator animator;

    private float horizontalMove = 0f;

    private void Start()
    {
        PlayerStateManager.ResetRun();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float moveSpeed = PlayerStateManager.CurrentState == PlayerState.Alive ? aliveSpeed : 0f;
        horizontalMove = horizontalInput * moveSpeed;

        if (animator != null)
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(horizontalMove));
            animator.SetInteger("State", (int)PlayerStateManager.CurrentState);
            animator.SetInteger("VisualState", (int)PlayerStateManager.GetVisualState());
        }

        transform.Translate(Vector2.right * horizontalMove * Time.deltaTime);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput > 0 ? 1 : -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
