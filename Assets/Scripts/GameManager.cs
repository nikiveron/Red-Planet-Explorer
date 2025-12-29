using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Глобальный менеджер игры, отвечающий:
/// - за перезапуск игровой сцены
/// - отображение панелей победы/поражения
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _incorrectLandingPanel;
    [SerializeField] private GameObject _lackOfScorePanel;
    [SerializeField] private float _winDelay = 1f;
    [SerializeField] private float _loseDelay = 2f;
    [SerializeField] private float _temporaryDelay = 5f;

    private void Awake()
    {
        HidePanels();
    }

    /// <summary>
    /// Перезапускает текущую сцену
    /// Используется для рестарта игры после победы или поражения
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Обрабатывает событие победы
    /// Активирует сопрограмму отображения панели победы
    /// </summary>
    public void OnWin()
    {
        StartCoroutine(ShowPanel(_winPanel, _winDelay));
    }

    /// <summary>
    /// Обрабатывает событие поражения
    /// Активирует сопрограмму отображения панели поражения
    /// </summary>
    public void OnLose()
    {
        StartCoroutine(ShowPanel(_losePanel, _loseDelay));
    }

    /// <summary>
    /// Сопрограмма для отображения панели с задержкой
    /// Ждёт указанное время, затем активирует панель
    /// </summary>
    /// <param name="panel">Панель для отображения</param>
    /// <param name="delay">Задержка перед отображением (в секундах)</param>
    private IEnumerator ShowPanel(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
    }

    public void OnIncorrectLanding()
    {
        StartCoroutine(ShowTemporaryPanel(_incorrectLandingPanel, _temporaryDelay));
    }

    public void OnLackOfScore()
    {
        StartCoroutine(ShowTemporaryPanel(_lackOfScorePanel, _temporaryDelay));
    }

    private IEnumerator ShowTemporaryPanel(GameObject panel, float delay)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(delay);
        panel.SetActive(false);
    }

    private void HidePanels()
    {
        _winPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);
        _incorrectLandingPanel.gameObject.SetActive(false);
        _lackOfScorePanel.gameObject.SetActive(false);
    }
}