using UnityEngine;

public class PageItemInteraction : NormalItemInteraction
{    
    [Header("气泡UI")]
    [SerializeField] private PagePanel pagePanel;


    protected override void Start()
    {
        base.Start();
        pagePanel.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        PlayerLockState.isMovementLocked = pagePanel.gameObject.activeSelf ? true : false;
    }

    protected override void OnItemClicked()
    {
        if (!pagePanel.gameObject.activeSelf)
        {
            pagePanel.gameObject.SetActive(true);
        }
    }

    protected override void OnPlayerExit()
    {
        pagePanel.gameObject.SetActive(false);
    }


}
