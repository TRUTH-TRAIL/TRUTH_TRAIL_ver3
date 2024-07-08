using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool trig, open;
    [SerializeField] private float smooth = 2.0f;
    [SerializeField] private float doorOpenAngle = 90.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;

    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private string openText = "Press E to Open";
    [SerializeField] private string closeText = "Press E to Close";
    [SerializeField] private TMP_FontAsset font;
    [SerializeField] private RectTransform txtTransform;
    [SerializeField] private Color textColor;
    [SerializeField] private int fontSize;
    [SerializeField] private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

    public TextMeshProUGUI TXT
    {
        get => txt;
        set => txt = value;
    }

    public RectTransform RectTransform
    {
        get => txtTransform;
        set => txtTransform = value;
    }
    
    public string OpenText
    {
        get => openText;
        set => openText = value;
    }

    public string CloseText
    {
        get { return closeText; }
        set { closeText = value; }
    }

    public Vector3 TextPosition
    {
        get { return txt != null ? txtTransform.localPosition : Vector3.zero; }
        set
        {
            if (txt != null) txtTransform.localPosition = value; txt.transform.localPosition = value; 
        }
    }

    public Color TextColor
    {
        get { return txt != null ? textColor : Color.white; }
        set { if (txt != null) textColor = value; txt.color = value; }
    }

    public int TextSize
    {
        get { return txt != null ? fontSize : 14; }
        set { if (txt != null) fontSize = value;
            txt.fontSize = value;
        }
    }

    public TMP_FontAsset Font
    {
        get { return font; }
        set
        {
            font = value;
            if (txt != null) txt.font = font;
        }
    }

    public TextAlignmentOptions Alignment
    {
        get { return alignment; }
        set
        {
            alignment = value;
            if (txt != null) txt.alignment = alignment;
        }
    }

    private void Start()
    {
        if (txt == null)
        {
            txt = GetComponentInChildren<TextMeshProUGUI>();
        }

        ApplyTextProperties();
        txt.text = "";
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + doorOpenAngle, defaultRot.z);
    }

    private void Update()
    {
        RotateDoor();
        HandleInput();
        UpdateText();
    }

    private void RotateDoor()
    {
        transform.eulerAngles = open 
            ? Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth) 
            : Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && trig)
        {
            open = !open;
        }
    }

    private void UpdateText()
    {
        if (trig)
        {
            txt.text = open ? closeText : openText;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            txt.text = open ? openText : closeText;
            trig = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            txt.text = "";
            trig = false;
        }
    }

    private void ApplyTextProperties()
    {
        if (txt != null)
        {
            txt.font = font;
            txt.alignment = alignment;
            txt.transform.localPosition = TextPosition;
            txt.color = textColor;
            txt.fontSize = fontSize;
        }
    }


    private void OnValidate()
    {
        ApplyTextProperties();
    }
}
