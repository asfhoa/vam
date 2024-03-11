using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameValue gameValue;
    [SerializeField] FollowCamera followCam;
    [SerializeField] Spawner spawner;
    [SerializeField] Player[] players;

    public float gameTime;
    public int gameLevel;

    bool isGameClear;
    bool isPauseGame;

    IEnumerator Start()
    {
        gameTime = 0f;
        AudioManager.Instance.PlayBGM();

        // 캐릭터를 생성한다.
        //스크립터블 오브젝트인 GameValue의 characterIndex값에 따라서 캐릭터를 불러온다
        Player player = Instantiate(players[gameValue.characterIndex]); 
        player.transform.position = Vector3.zero;
        player.Setup();
        switch(gameValue.characterIndex)
        {
            //캐릭터에 따라서 초기 아이템을 설정
            case 0:
                player.AddItem(ItemManager.Instance.GetItem("ITWE0001"));
                break;
            case 1:
                player.AddItem(ItemManager.Instance.GetItem("ITWE0003"));
                break;
        }

        yield return null;
        followCam.Setup(player.transform);
        spawner.SwitchSpawner(true);
    }


    private void Update()
    {
        if (isPauseGame)
            return;

        gameTime += Time.deltaTime;
        gameLevel = (int)(gameTime / MAX_GAME_TIME / MAX_LEVEL);    //현재 플레이 시간에 따라서 게임 레벨을 변경

        //현재 시간이 설정한 시간을 넘기면 게임을 클리어시킨다
        if(!isGameClear && gameTime >= MAX_GAME_TIME)
            isGameClear = true;
    }

    public void OnDeadPlayer()  //플레이어가 죽었을때의 추가처리작업
    {
        AudioManager.Instance.PlayBGM(AUDIO_STATE.STOP);
        MiddleUI.Instance.OpenResult(isGameClear);
    }
    public void SwitchPause(bool isPause)   //게임을 일시정지 시키거나 정지를 해재한다
    {
        isPauseGame = isPause;
        onPauseGame?.Invoke(isPause);
    }
    public void GoTitle()   //씬을 Title씬으로 교체
    {
        SceneManager.LoadScene("Title");
    }



    public event Action<bool> onPauseGame;      // 일시정지 이벤트.

    public const float MAX_GAME_TIME = 15f;
    public const int MAX_LEVEL = 30;
}
