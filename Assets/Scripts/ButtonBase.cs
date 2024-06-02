using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
  [SerializeField] protected Button _button;


  private void OnEnable()
  {
    _button.onClick.AddListener(ButtonClick);
  }

  private void OnDisable()
  {
    _button.onClick.RemoveListener(ButtonClick);
  }

  protected abstract void ButtonClick(); 
}