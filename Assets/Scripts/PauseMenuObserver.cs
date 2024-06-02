using UnityEngine;

public class PauseMenuObserver : MonoBehaviour
{
  [SerializeField] private ResumeButton _resumeButton;
  [SerializeField] private ExitButton _exitButton;
  [SerializeField] private GameObject _gameCanvas;

  private void OnEnable()
  {
    _resumeButton.OnButtonClick += OnResumeButtonClick;
    _exitButton.OnButtonClick += OnExitButtonClick;
  }
  
  private void OnDisable()
  {
    _resumeButton.OnButtonClick -= OnResumeButtonClick;
    _exitButton.OnButtonClick -= OnExitButtonClick;
  }

  private void OnResumeButtonClick()
  {
    Time.timeScale = 1;
    _gameCanvas.SetActive(true);
    gameObject.SetActive(false);
  }

  private void OnExitButtonClick()
  {
    Application.Quit();
  }
}