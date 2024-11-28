using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneLoad_Timelne : MonoBehaviour
{
    [SerializeField] private GameObject Player_Obj;
    [SerializeField] private GameObject Timeline_Obj;
    [SerializeField] private GameObject Play_Obj;
    [SerializeField] private GameObject AI_Obj;
    [SerializeField] private PlayableDirector playableDirector;  // Ÿ�Ӷ��ο� ����� PlayableDirector
    void Start()
    {
        // ���� Ȱ��ȭ�� �� ��������
        Scene activeScene = SceneManager.GetActiveScene();
        playableDirector.stopped += OnTimelineStopped;
        OnSceneLoaded(activeScene, LoadSceneMode.Single);  // �������� ȣ��   
    }

    void OnEnable()
    {
        // ���� �ε�Ǿ��� �� �̺�Ʈ ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // // MonoBehaviour�� Start �޼��忡�� �̺�Ʈ�� �ڵ鷯�� ����մϴ�.
    // void Start()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    //     playableDirector.stopped += OnTimelineStopped;
    // }

    // �� �ε� �̺�Ʈ �ڵ鷯
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Exorcism")
        {
            Debug.Log("Ư�� ���� �ε�Ǿ����ϴ�!");
            Timeline_Obj.SetActive(true);
            Play_Obj.SetActive(false);
            AI_Obj.SetActive(false);
        }
    }

    // MonoBehaviour�� OnDestroy �޼��忡�� �̺�Ʈ �ڵ鷯�� ����
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        playableDirector.stopped -= OnTimelineStopped;
    }

    // TimeLine ����
    void OnTimelineStopped(PlayableDirector director)
    {
        Debug.Log("Timeline�� �������ϴ�!");
        Timeline_Obj.SetActive(false);
        Play_Obj.SetActive(true);
        AI_Obj.SetActive(true);

        Play_Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        Player_Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
