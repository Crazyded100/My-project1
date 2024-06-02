using UnityEngine.Events;

internal class ResumeButton :ButtonBase
{
  public event UnityAction OnButtonClick;
  
  protected override void ButtonClick()
  {
    OnButtonClick?.Invoke();
  }
}