using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Success_Timeline : MonoBehaviour
{
    [SerializeField] private GameObject Timeline_Obj;
   // [SerializeField] private GameObject TimelinePlay_Obj;
    [SerializeField] private GameObject Play_Obj;
    [SerializeField] private GameObject AI_Obj;
    [SerializeField] private PlayableDirector playableDirector;  // Ÿ�Ӷ��ο� ����� PlayableDirector
    void Start()
    {
        Timeline_Obj.SetActive(true);
  //      TimelinePlay_Obj.SetActive(true);
        Play_Obj.SetActive(false);
        AI_Obj.SetActive(true);
        // ���� Ȱ��ȭ�� �� ��������
       // Scene activeScene = SceneManager.GetActiveScene();
        playableDirector.gameObject.SetActive(true);
        playableDirector.stopped += OnTimelineStopped;
       // OnSceneLoaded(activeScene, LoadSceneMode.Single);  // �������� ȣ��
    }

    // void OnEnable()
    // {
    //     // ���� �ε�Ǿ��� �� �̺�Ʈ ����
    //     //SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // // MonoBehaviour�� Start �޼��忡�� �̺�Ʈ�� �ڵ鷯�� ����մϴ�.
    // void Start()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    //     playableDirector.stopped += OnTimelineStopped;
    // }

    // ���� �ε�� �� ȣ��Ǵ� �̺�Ʈ �ڵ鷯�Դϴ�.
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // �ε�� ���� �̸��� Ȯ���մϴ�.
    //     if (scene.name == "Exorcism")
    //     {
    //         // Ư�� ���� �ε�Ǿ��� �� ������ �ڵ带 ���⿡ �ۼ��մϴ�.
    //         Debug.Log("Ư�� ���� �ε�Ǿ����ϴ�!");
    //         Timeline_Obj.SetActive(true);
    //         Play_Obj.SetActive(false);
    //         AI_Obj.SetActive(false);
    //         // ��: ���� ������Ʈ Ȱ��ȭ, ���� �ʱ�ȭ ��
    //     }
    // }

    // MonoBehaviour�� OnDestroy �޼��忡�� �̺�Ʈ �ڵ鷯�� �����մϴ�.
    void OnDestroy()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        playableDirector.stopped -= OnTimelineStopped;
    }
    void OnTimelineStopped(PlayableDirector director)
    {
        Debug.Log("Timeline�� �������ϴ�!");
        //Timeline_Obj.SetActive(false);
        // Play_Obj.SetActive(true);
        // AI_Obj.SetActive(true);
        // Timeline�� ������ �� ������ ������ ���⿡ �߰�
    }
}
