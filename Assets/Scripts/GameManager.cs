using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject RestartUI;

    public Text KillCountText;

    public Image HealthBar;

    public GameObject PausePanelUI;

    int _killCount = 0;
    bool _paused = false;

    public static GameManager instance;

    void Start() {
        instance = this;    
    }

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            PauseGame();
        }
    }

    public void PauseGame() {
        PausePanelUI.SetActive(!PausePanelUI.activeSelf);
        if (!_paused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }

        _paused = !_paused;
    }

    // Start is called before the first frame update
    public void EndGame() {
        RestartUI.SetActive(true);
    }

    public void UpdateScore() {
        _killCount += 1;

        KillCountText.text = _killCount.ToString();
    }

    public void UpdateHealth(float health) {
        HealthBar.fillAmount = health;
    }
}
