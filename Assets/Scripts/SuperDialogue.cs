using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PolygonCollider2D))]
public class SuperDialogue : MonoBehaviour {
    [Space(15)]
    [Tooltip("对话的类型")]
    public DialogueType dialogueType;
    [Space(15)]
    [Tooltip("按键类型")]
    public KeyCode key;
    [Space(15)]
    [Tooltip("是否显示按键提示")]
    [Space(15)]
    public bool isPressTips;
    [Tooltip("主角是谁")]
    public Transform player;
    [Tooltip("是不是主角先说话")]
    public bool playerTalkFirst;
    [Space(15)]
    [Tooltip("调整对话框的位置")]
    public Vector3 DialogueCaseOffset;
    [Space(15)]
    [Tooltip("最后一个值是空白属正常现象")]
    public List<string> dialogueBox;
    [Space(15)]
    private Object dialogueCase;
    private GameObject pressTips;
    //对话框实例
    public GameObject[] dcInstance;
    private bool isTalking;

    private bool isCanTalk;
    private int diaIndex;
	// Use this for initialization
	void Start () {
        dcInstance = new GameObject[2];
        GetComponent<PolygonCollider2D>().isTrigger = true;
        key = KeyCode.E;
        dialogueCase = Resources.Load("Prefabs/DialogueCase", typeof(GameObject));
        if (isPressTips)
        {
            pressTips = GameObject.Find("pressTips");

            pressTips.SetActive(false);
        }
        else
        {
            pressTips = null;
        }

        dialogueBox.Add(" ");
        isTalking = false;
        isCanTalk = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (dcInstance[0]!=null)
        {
            dcInstance[0].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;
        }
        //dui hua kai shi
        if (Input.GetKeyDown(key))
        {
            if (dialogueType == DialogueType.soliloquize)
            {
                SoliloquizeMode();
            }
            else if (dialogueType == DialogueType.conversation)
            {
                SoliloquizeMode();
            }
        }
    }
     
    private void SoliloquizeMode()
    {
        if (isCanTalk)
        {
            if (isPressTips)
            {
                pressTips.SetActive(false);
            }
            if (dialogueType == DialogueType.soliloquize)
            {
                GameObject go = Instantiate(dialogueCase) as GameObject;
                go.transform.SetParent(GameObject.Find("Canvas").transform, false);
                dcInstance[0] = go;
                go.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;
                dcInstance[0].GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
            }
            else
            {
                if (playerTalkFirst)
                {
                    GameObject go1 = Instantiate(dialogueCase) as GameObject;
                    go1.transform.SetParent(GameObject.Find("Canvas").transform,false);
                    dcInstance[0] = go1;
                    go1.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.position) + DialogueCaseOffset;
                    dcInstance[0].GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
                }
                else
                {
                    GameObject go1 = Instantiate(dialogueCase) as GameObject;
                    go1.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    dcInstance[0] = go1;
                    go1.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;
                    dcInstance[0].GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
                }
            }
            isCanTalk = false;
            isTalking = true;
        }
        if (isTalking)
        {
            if (dialogueType == DialogueType.soliloquize)
            {
                dcInstance[0].GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
            }
            else
            {
                dcInstance[0].GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
                if (dcInstance[1]!= null)
                {
                    dcInstance[1].GetComponentInChildren<Text>().text = dialogueBox[diaIndex + 1];
                }
            }

            if (diaIndex < dialogueBox.Count - 1)
            {
                diaIndex++;
            }
            else if (diaIndex >= dialogueBox.Count - 1)
            {
                Destroy(dcInstance[0]);
                diaIndex = 0;
                isCanTalk = false;
                isTalking = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D character)
    {
        if (isPressTips)
        {
            pressTips.SetActive(true);
        }

        isCanTalk = true;
    }
    private void OnTriggerExit2D(Collider2D charactre)
    {
        isCanTalk = false;
        isTalking = false;
        if (isPressTips)
        {
            pressTips.SetActive(false);
        }
        if (dcInstance != null)
        {
            if (dialogueType == DialogueType.soliloquize)
            {
                Destroy(dcInstance[0]);
            }
            else
            {
                Destroy(dcInstance[0]);
                Destroy(dcInstance[1]);
            }
        }
        diaIndex = 0;
    }
}
