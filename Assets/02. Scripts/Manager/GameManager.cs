using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    public class GameManager : BaseManager
    {
        public static GameManager Instance;

        public GameObject AI;
        public GameObject KillAI;
        public GameObject Light;
        public GameObject CursorObject;
        public Camera MainCemera;
        
        public CanvasGroup gameOverCanvasGroup;
        public CanvasGroup curseOverCanvasGroup;
        public float fadeDuration = 1.0f;

        public string CurrentSceneName;
        public string MenuSceneName = "MainMenu";
        public string ExorcismSceneName = "Exorcism";

        private Player player;
        private PlayerSound playerSound;


        private void Awake()
        {
            player = FindObjectOfType<Player>();
            playerSound = FindObjectOfType<PlayerSound>();
            Instance = this;
        }

        private void Start()
        {
            Light ??= GameObject.FindGameObjectWithTag("Lights");
            CursorObject ??= GameObject.FindGameObjectWithTag("InteractionCanvas");
        }

        public void ReGame()
        {
            player.IsDead = false;
            SceneSwitchManager.Instance.ChangeScene(CurrentSceneName);
        }

        public void GoMenu()
        {
            SceneSwitchManager.Instance.ChangeScene(MenuSceneName);
        }
        
        /// AI ���
        public void GameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // �������� �� �����°� ���� ġġ�� ���������ϸ� ������ ���� �߰�
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(DeathCutScene());    // ����ƾ� ����� �ڷ�ƾ(�ó׸ӽ� ī�޶� �ٿ��� �ҵ�)
        }

        /// ���� ���
        public void CurseGameOver()
        {
            AI.SetActive(false);
            Light.SetActive(false);     // �������� �� �����°� ���� ġġ�� ���������ϸ� ������ ���� �߰�
            CursorObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerSound.StopSound();
            playerSound.PlaySound("GameOver", false);
            player.Dead();

            StartCoroutine(FadeInCanvas(curseOverCanvasGroup));
        }

        public void NextExorcismScene()
        {
            SaveExorcismProgress.SaveProgress();
            // ���� ����
            if (player.isEqiupKey)
            {
                MainGameSoundManager.Instance.PlaySFX("SFX_Key");
                SceneSwitchManager.Instance.ChangeScene(ExorcismSceneName);
            }
            else
            {
                MainGameSoundManager.Instance.PlaySFX("SFX_LockedDoor");
            }
        }


        /// ��� �ƾ�
        private IEnumerator DeathCutScene()
        {
            MainGameSoundManager.Instance.AiSoundPlay("SFX_DeadCutSceneAI");

            Vector3 killAiPosi = AI.transform.position;
            killAiPosi.y = killAiPosi.y - 3f;
            KillAI.transform.position = killAiPosi;
            KillAI.transform.LookAt(player.transform);
            killAiPosi.y = killAiPosi.y + 2f;
            MainCemera.transform.LookAt(killAiPosi);
            
            yield return new WaitForSeconds(3f);
            Light.SetActive(true);
            KillAI.SetActive(true);
            MainCemera.GetComponent<CameraShake>().OnCameraShake(5f);

            yield return new WaitForSeconds(5f);
            StartCoroutine(FadeInCanvas(gameOverCanvasGroup));
        }
        
        private IEnumerator FadeInCanvas(CanvasGroup c)
        {
            float elapsedTime = 0f;
            Light.SetActive(false);

            c.alpha = 0f;
            c.gameObject.SetActive(true);

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                c.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            c.interactable = true;
            c.blocksRaycasts = true;
            c.alpha = 1f;
        } 
    }
}