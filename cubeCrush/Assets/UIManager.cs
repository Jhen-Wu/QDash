using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    GameManager gm;
    private void Start() {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActiveateGameOverUI);
    }

    public  void PlayButtonHandler() {
        gm.StartGame();
    }

    public void ActiveateGameOverUI() {
        gameOverUI.SetActive(true);
    }
    private void OnGUI() {
        scoreUI.text = gm.PrettyScroe();
    }
}


