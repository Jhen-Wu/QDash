using UnityEngine;              
using UnityEngine.Events;       

public class GameManager : MonoBehaviour 
{
    #region Singleton  
    public static GameManager Instance;   // 靜態實例，讓其他腳本能全域存取 GameManager

    private void Awake()
    {
        // 確保場景中只會有一個 GameManager
        if (Instance == null) Instance = this;
    }
    #endregion

    public float currentScore = 0f;       // 當前遊戲分數
    public Data data;                     // 存放存檔資料（最高分）
    public bool isPlaying = false;        // 是否正在遊戲中

    public UnityEvent onPlay = new UnityEvent();       // 遊戲開始事件
    public UnityEvent onGameOver = new UnityEvent();   // 遊戲結束事件

    private void Start()
    {
        // 嘗試從硬碟讀取存檔
        string loadedData = SaveSystem.Load("save");

        if (loadedData != null)
        {
            // 若有存檔，將 JSON 轉回 Data 物件
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else
        {
            // 若沒有存檔，建立新資料
            data = new Data();    
        }    
    }    

    private void Update() 
    {
        // 若遊戲進行中，分數隨時間累加
        if (isPlaying) 
        {
            currentScore += Time.deltaTime;
        }

        // 偵測鍵盤 K 鍵（測試用）
        if (Input.GetKeyDown("k")) 
        {
            isPlaying = true;
        }
    }

    public void StartGame() 
    {
        AudioManager.Instance.StopStartSound(); // 停止開頭背景音樂
        AudioManager.Instance.StopGameOver();   // 確保結束音效停止
        onPlay.Invoke();                        // 觸發「開始遊戲」事件
        isPlaying = true;                       // 設定遊戲狀態為進行中
        currentScore = 0;                       // 分數歸零
    }

    public void GameOver() 
    {
        // 若本次分數高於最高分，更新存檔
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
            string saveString = JsonUtility.ToJson(data); // 轉為 JSON
            SaveSystem.Save("save", saveString);          // 寫入檔案
        }

        AudioManager.Instance.PlayGameOver(); // 播放 GameOver 音效
        isPlaying = false;                    // 停止遊戲
        onGameOver.Invoke();                  // 觸發遊戲結束事件
    }

    public string PrettyScroe() 
    {
        // 將分數四捨五入後轉成字串
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscroe() 
    {
        // 將最高分四捨五入後轉成字串
        return Mathf.RoundToInt(data.highscore).ToString();
    }

    public void BackToMenu()
    {
        isPlaying = false;                        // 停止遊戲
        currentScore = 0;                        // 分數歸零
        AudioManager.Instance.PlayStartSound();  // 播放主選單音樂
        AudioManager.Instance.StopGameOver();    // 停止結束音效
    }
}




