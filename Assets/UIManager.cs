using UnityEngine;     
using TMPro;          

// UI 管理器：負責所有畫面顯示與按鈕操作
public class UIManager : MonoBehaviour 
{
    // 畫面左上角（或遊戲中）顯示即時分數的文字
    [SerializeField] private TextMeshProUGUI scoreUI;

    // 開始選單 UI（Play 按鈕所在畫面）
    [SerializeField] private GameObject startMenuUI;

    // Game Over 畫面 UI
    [SerializeField] private GameObject gameOverUI;

    // Game Over 畫面中的本局分數文字
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;

    // Game Over 畫面中的最高分文字
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;

    // GameManager 參考，用來取得分數與呼叫遊戲流程
    GameManager gm;

    private void Start() 
    {
        // 取得 GameManager 的 Singleton 實例
        gm = GameManager.Instance;

        // 訂閱「遊戲結束」事件，當 GameOver 發生時自動顯示 GameOver UI
        gm.onGameOver.AddListener(ActiveateGameOverUI);

        // 遊戲一開始顯示開始選單
        startMenuUI.SetActive(true);

        // 一開始不顯示 GameOver 畫面
        gameOverUI.SetActive(false);
    }

    // Play 按鈕事件（由 Button 的 OnClick 呼叫）
    public void PlayButtonHandler() 
    {
        // 關閉開始選單
        startMenuUI.SetActive(false);

        // 確保 GameOver 畫面關閉
        gameOverUI.SetActive(false);

        // 通知 GameManager 開始遊戲
        gm.StartGame();
    }

    // 顯示 GameOver UI（由 GameManager 的 onGameOver 事件呼叫）
    public void ActiveateGameOverUI() 
    {
        // 顯示 GameOver 畫面
        gameOverUI.SetActive(true);

        // 顯示本局分數（轉成整數字串）
        gameOverScoreUI.text = "Score: " + gm.PrettyScroe();

        // 顯示最高分
        gameOverHighscoreUI.text = "Highscore: " + gm.PrettyHighscroe();
    }

    private void OnGUI() 
    {
        // 每一幀更新遊戲中顯示的分數
        //（即時反映 currentScore）
        scoreUI.text = gm.PrettyScroe();
    }

    // Back To Menu 按鈕事件（由 GameOver UI 的按鈕呼叫）
    public void BackToMenuButtonHandler()
    {
        // 關閉 GameOver 畫面
        gameOverUI.SetActive(false);

        // 顯示開始選單
        startMenuUI.SetActive(true);

        // 通知 GameManager 回到主選單狀態
        gm.BackToMenu();
    }
}

