using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    [SerializeField]
    private float messageTime;

    [SerializeField]
    private GameObject messageInteractObject;

    [SerializeField]
    private GameObject messageInspectObject;

    [SerializeField]
    private GameObject messageCancelObject;

    [SerializeField]
    private GameObject messageTextInteractObject;

    [SerializeField]
    private GameObject messageTextInspectObject;

    [SerializeField]
    private GameObject messageTextCancelObject;


    private Text interactText;
    private Text inspectText;
    private Text cancelText;

    void Start()
    {
        interactText = messageTextInteractObject.GetComponent<Text>();
        inspectText = messageTextInspectObject.GetComponent<Text>();
        cancelText = messageTextCancelObject.GetComponent<Text>();

        messageInteractObject.SetActive(false);
        messageInspectObject.SetActive(false);
        messageCancelObject.SetActive(false);

    }

    void FixedUpdate()
    {
        clearMessage();
    }

    public void showInteract(string message)
    {
        interactText.text = message;
        messageInteractObject.SetActive(true);
    }

    public void showInspect(string message)
    {
        inspectText.text = message;
        messageInspectObject.SetActive(true);
    }

    public void showCancel(string message)
    {
        cancelText.text = message;
        messageCancelObject.SetActive(true);
    }

    public void clearMessage()
    {
        if (messageInteractObject.activeSelf)
        {
            messageInteractObject.SetActive(false);
        }else
        if (messageInspectObject.activeSelf)
        {
            messageInspectObject.SetActive(false);
        }else
        if (messageCancelObject.activeSelf)
        {
            messageCancelObject.SetActive(false);
        }

    }

}
