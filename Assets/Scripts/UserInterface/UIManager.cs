using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Canvas playerHUD;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = playerHUD.GetComponent<CanvasGroup>();
    }

    public void TogglePlayerHUD(bool state)
    {
        if (state)
            _canvasGroup.alpha = 1;
        else
            _canvasGroup.alpha = 0;
    }
}
