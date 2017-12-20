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
    [Tooltip("调整对话框的位置")]
    public Vector3 DialogueCaseOffset;
    [Space(15)]
    public List<string> dialogueBox;

    private Object dialogueCase;
    private object pressTips;
    //对话框实例
    private GameObject dcInstance;
	// Use this for initialization
	void Start () {
        GetComponent<PolygonCollider2D>().isTrigger = true;

        dialogueCase = Resources.Load("Prefabs/DialogueCase", typeof(GameObject));

        pressTips = Resources.Load("Prefabs/DialogueCase",typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
        if (dcInstance!=null)
        {
            dcInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;

        }

    }
    private void OnTriggerEnter2D(Collider2D character)
    {
        //tips

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject go = Instantiate(dialogueCase) as GameObject;
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            dcInstance = go;
            go.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position) + DialogueCaseOffset;
        }
    }
}
