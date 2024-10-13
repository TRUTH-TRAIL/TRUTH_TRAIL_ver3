using TMPro;
using UnityEngine;

// public enum DoorType
// {
//     Normal = 0,
//     Restroom = 1,
//     EntryFoyer = 2,
//     Basement = 3,
// }

public class Timeline_Door : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField] private float smooth = 2.0f;
    [SerializeField] private float doorOpenAngle = 90.0f;
    private Vector3 defaultRotation;
    private Vector3 openRotation;

    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private string openText = "Door is Opening...";
    [SerializeField] private string closeText = "Door is Closing...";
    [SerializeField] private TMP_FontAsset font;
    [SerializeField] private RectTransform txtTransform;
    [SerializeField] private Color textColor;
    [SerializeField] private int fontSize = 24;
    [SerializeField] private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

    public DoorType doorType = DoorType.Normal;

    private void Start()
    {
        // 기본 회전 값 설정
        defaultRotation = transform.eulerAngles;
        openRotation = new Vector3(defaultRotation.x, defaultRotation.y + doorOpenAngle, defaultRotation.z);
        
        ApplyTextProperties();  // 텍스트 설정 적용
        if (txt != null)
            txt.text = "";
    }

    private void Update()
    {
        RotateDoor();  // 문 회전 업데이트
    }

    /// <summary>
    /// 문을 여는 함수 (외부에서 호출 가능)
    /// </summary>
    public void OpenDoor()
    {
        isOpen = true;
        UpdateText(openText);
    }

    /// <summary>
    /// 문을 닫는 함수 (외부에서 호출 가능)
    /// </summary>
    public void CloseDoor()
    {
        isOpen = false;
        UpdateText(closeText);
    }

    /// <summary>
    /// 문이 열려 있으면 닫고, 닫혀 있으면 여는 함수 (토글 방식)
    /// </summary>
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateText(isOpen ? openText : closeText);
    }

    /// <summary>
    /// 문 회전 로직
    /// </summary>
    private void RotateDoor()
    {
        transform.eulerAngles = isOpen
            ? Vector3.Slerp(transform.eulerAngles, openRotation, Time.deltaTime * smooth)
            : Vector3.Slerp(transform.eulerAngles, defaultRotation, Time.deltaTime * smooth);
    }

    /// <summary>
    /// 문 상태에 따른 텍스트 업데이트
    /// </summary>
    /// <param name="text">문이 열리거나 닫힐 때 표시할 텍스트</param>
    private void UpdateText(string text)
    {
        if (txt != null)
        {
            txt.text = text;
        }
    }

    /// <summary>
    /// 텍스트 프로퍼티 적용
    /// </summary>
    private void ApplyTextProperties()
    {
        if (txt != null)
        {
            txt.font = font;
            txt.alignment = alignment;
            txt.color = textColor;
            txt.fontSize = fontSize;
            if (txtTransform != null)
            {
                txtTransform.localPosition = txt.transform.localPosition;
            }
        }
    }
}
