using UnityEngine;

public class WindowRainAnimation : MonoBehaviour
{
    [Header("animation")]
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // 获取Animator
    }
}
