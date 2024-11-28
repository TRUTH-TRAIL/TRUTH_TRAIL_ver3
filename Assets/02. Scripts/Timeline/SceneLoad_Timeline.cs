using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneLoad_Timelne : MonoBehaviour
{
    [SerializeField] private GameObject Player_Obj;
    [SerializeField] private GameObject Timeline_Obj;
    [SerializeField] private GameObject Play_Obj;
    [SerializeField] private GameObject AI_Obj;
    [SerializeField] private PlayableDirector playableDirector;  // 타임라인에 연결된 PlayableDirector
    void Start()
    {
        // 현재 활성화된 씬 가져오기
        Scene activeScene = SceneManager.GetActiveScene();
        playableDirector.stopped += OnTimelineStopped;
        OnSceneLoaded(activeScene, LoadSceneMode.Single);  // 수동으로 호출   
    }

    void OnEnable()
    {
        // 씬이 로드되었을 때 이벤트 구독
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // // MonoBehaviour의 Start 메서드에서 이벤트에 핸들러를 등록합니다.
    // void Start()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    //     playableDirector.stopped += OnTimelineStopped;
    // }

    // 씬 로드 이벤트 핸들러
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Exorcism")
        {
            Debug.Log("특정 씬이 로드되었습니다!");
            Timeline_Obj.SetActive(true);
            Play_Obj.SetActive(false);
            AI_Obj.SetActive(false);
        }
    }

    // MonoBehaviour의 OnDestroy 메서드에서 이벤트 핸들러를 해제
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        playableDirector.stopped -= OnTimelineStopped;
    }

    // TimeLine 종료
    void OnTimelineStopped(PlayableDirector director)
    {
        Debug.Log("Timeline이 끝났습니다!");
        Timeline_Obj.SetActive(false);
        Play_Obj.SetActive(true);
        AI_Obj.SetActive(true);

        Play_Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        Player_Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
