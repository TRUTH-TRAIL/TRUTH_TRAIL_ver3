using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Success_Timeline : MonoBehaviour
{
    [SerializeField] private GameObject Timeline_Obj;
   // [SerializeField] private GameObject TimelinePlay_Obj;
    [SerializeField] private GameObject Play_Obj;
    [SerializeField] private GameObject AI_Obj;
    [SerializeField] private PlayableDirector playableDirector;  // 타임라인에 연결된 PlayableDirector
    void Start()
    {
        Timeline_Obj.SetActive(true);
  //      TimelinePlay_Obj.SetActive(true);
        Play_Obj.SetActive(false);
        AI_Obj.SetActive(true);
        // 현재 활성화된 씬 가져오기
       // Scene activeScene = SceneManager.GetActiveScene();
        playableDirector.gameObject.SetActive(true);
        playableDirector.stopped += OnTimelineStopped;
       // OnSceneLoaded(activeScene, LoadSceneMode.Single);  // 수동으로 호출
    }

    // void OnEnable()
    // {
    //     // 씬이 로드되었을 때 이벤트 구독
    //     //SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // // MonoBehaviour의 Start 메서드에서 이벤트에 핸들러를 등록합니다.
    // void Start()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    //     playableDirector.stopped += OnTimelineStopped;
    // }

    // 씬이 로드될 때 호출되는 이벤트 핸들러입니다.
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // 로드된 씬의 이름을 확인합니다.
    //     if (scene.name == "Exorcism")
    //     {
    //         // 특정 씬이 로드되었을 때 실행할 코드를 여기에 작성합니다.
    //         Debug.Log("특정 씬이 로드되었습니다!");
    //         Timeline_Obj.SetActive(true);
    //         Play_Obj.SetActive(false);
    //         AI_Obj.SetActive(false);
    //         // 예: 게임 오브젝트 활성화, 변수 초기화 등
    //     }
    // }

    // MonoBehaviour의 OnDestroy 메서드에서 이벤트 핸들러를 해제합니다.
    void OnDestroy()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        playableDirector.stopped -= OnTimelineStopped;
    }
    void OnTimelineStopped(PlayableDirector director)
    {
        Debug.Log("Timeline이 끝났습니다!");
        //Timeline_Obj.SetActive(false);
        // Play_Obj.SetActive(true);
        // AI_Obj.SetActive(true);
        // Timeline이 끝났을 때 실행할 로직을 여기에 추가
    }
}
