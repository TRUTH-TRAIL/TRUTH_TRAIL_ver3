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
        // �⺻ ȸ�� �� ����
        defaultRotation = transform.eulerAngles;
        openRotation = new Vector3(defaultRotation.x, defaultRotation.y + doorOpenAngle, defaultRotation.z);
        
        ApplyTextProperties();  // �ؽ�Ʈ ���� ����
        if (txt != null)
            txt.text = "";
    }

    private void Update()
    {
        RotateDoor();  // �� ȸ�� ������Ʈ
    }

    /// <summary>
    /// ���� ���� �Լ� (�ܺο��� ȣ�� ����)
    /// </summary>
    public void OpenDoor()
    {
        isOpen = true;
        UpdateText(openText);
    }

    /// <summary>
    /// ���� �ݴ� �Լ� (�ܺο��� ȣ�� ����)
    /// </summary>
    public void CloseDoor()
    {
        isOpen = false;
        UpdateText(closeText);
    }

    /// <summary>
    /// ���� ���� ������ �ݰ�, ���� ������ ���� �Լ� (��� ���)
    /// </summary>
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateText(isOpen ? openText : closeText);
    }

    /// <summary>
    /// �� ȸ�� ����
    /// </summary>
    private void RotateDoor()
    {
        transform.eulerAngles = isOpen
            ? Vector3.Slerp(transform.eulerAngles, openRotation, Time.deltaTime * smooth)
            : Vector3.Slerp(transform.eulerAngles, defaultRotation, Time.deltaTime * smooth);
    }

    /// <summary>
    /// �� ���¿� ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    /// <param name="text">���� �����ų� ���� �� ǥ���� �ؽ�Ʈ</param>
    private void UpdateText(string text)
    {
        if (txt != null)
        {
            txt.text = text;
        }
    }

    /// <summary>
    /// �ؽ�Ʈ ������Ƽ ����
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
