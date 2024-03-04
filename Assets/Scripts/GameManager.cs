using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<bool> onPauseGame;      //�Ͻ����� �̺�Ʈ

    private bool isPauseGame;   //�Ͻ����� ����

    private void SwitchPause()
    {
        isPauseGame = !isPauseGame;
        onPauseGame?.Invoke(isPauseGame);
    }

    public void SwitchPauseForce(bool isPause)
    {
        isPauseGame = !isPause;
        SwitchPause();
    }                                                                                ,
}
