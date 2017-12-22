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
    [Tooltip("主角是谁")]
    public Transform player;
    [Space(15)]
    [Tooltip("调整对话框的位置")]
    public Vector3 DialogueCaseOffset;
    [Space(15)]
    [Tooltip("最后一个值是空白属正常现象")]
    public List<string> dialogueBox;

    private Object dialogueCase;
    private GameObject pressTips;
    //对话框实例
    private GameObject dcInstance;
    private bool isTalking;

    private bool isCanTalk;
    private int diaIndex;
	// Use this for initialization
	void Start () {
        GetComponent<PolygonCollider2D>().isTrigger = true;

        dialogueCase = Resources.Load("Prefabs/DialogueCase", typeof(GameObject));

        pressTips = GameObject.Find("pressTips");

        pressTips.SetActive(false);

        dialogueBox.Add(" ");
        isTalking = false;
        isCanTalk = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (dcInstance!=null)
        {
            dcInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;

        }
        //dui hua kai shi
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueType == DialogueType.soliloquize)
            {
                SoliloquizeMode();
            }
            else if (dialogueType == DialogueType.conversation)
            {

            }
        }
    }
     
    private void SoliloquizeMode()
    {
        if (isCanTalk)
        {
            pressTips.SetActive(false);
            GameObject go = Instantiate(dialogueCase) as GameObject;
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            dcInstance = go;
            go.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;
            dcInstance.GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
            isCanTalk = false;
            isTalking = true;
        }
        if (isTalking)
        {
            dcInstance.GetComponentInChildren<Text>().text = dialogueBox[diaIndex];
            if (diaIndex < dialogueBox.Count - 1)
            {
                diaIndex++;
            }
            else if (diaIndex >= dialogueBox.Count - 1)
            {
                Destroy(dcInstance);
                diaIndex = 0;
                isCanTalk = false;
                isTalking = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D character)
    {
        pressTips.SetActive(true);
        isCanTalk = true;
    }
    private void OnTriggerExit2D(Collider2D charactre)
    {
        isCanTalk = false;
        isTalking = false;
        pressTips.SetActive(false);
        if (dcInstance != null)
        {
            Destroy(dcInstance);
        }
        diaIndex = 0;
    }
}
