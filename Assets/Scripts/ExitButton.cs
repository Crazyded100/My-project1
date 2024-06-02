using UnityEngine.Events;

public class ExitButton : ButtonBase
{
  public event UnityAction OnButtonClick;
  protected override void ButtonClick()
  {
    OnButtonClick?.Invoke();
  }
}