using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerUI : MonoBehaviour
{
    [SerializeField] private Text activePlayerName;
    [SerializeField] private Text playerPoints;

    private void SetUIElements()
    {
        activePlayerName.text = "P" + GameManager.Instance.activePlayer.playerId;
        playerPoints.text = GameManager.Instance.activePlayer.points.ToString();
    }

    public void TogglePlayer()
    {
        GameManager.Instance.TogglePlayer();
        SetUIElements();
    }

    // Update is called once per frame
    private void Update()
    {
        SetUIElements();
    }
}
