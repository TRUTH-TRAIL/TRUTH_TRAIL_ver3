using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool trig, open;
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    private Vector3 defaulRot;
    private Vector3 openRot;
    public TextMeshProUGUI txt;
    
    private void Start()
    {
        txt.text = "";
        defaulRot = transform.eulerAngles;
        openRot = new Vector3(defaulRot.x, defaulRot.y + DoorOpenAngle, defaulRot.z);
    }

    private void Update()
    {
        if (open)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaulRot, Time.deltaTime * smooth);
        }
        if (Input.GetKeyDown(KeyCode.E) && trig)
        {
            open = !open;
        }
        if (trig)
        {
            if (open)
            {
                if (txt != null)
                {
                    txt.text = "Close E";
                }
                
            }
            else
            {
                if (txt != null)
                {
                    txt.text = "Open E";
                }
            }
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (!open)
            {
                if (txt != null)
                {
                    txt.text = "Close E ";
                }
            }
            else
            {
                if (txt != null)
                {
                    txt.text = "Open E";
                }
            }
            trig = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (txt != null)
            {
                txt.text = " ";
            }

            trig = false;
        }
    }
}
