using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Card : MonoBehaviour,IDragHandler,IEndDragHandler
{
    InputField inputField;
    public Text text;
    public int cardData;

    public void OnChangeCardConfirm()
    {
        inputField.interactable = false;
        if (inputField.text != string.Empty)
        {
            int num = int.Parse(inputField.text);
            if (num > 0 && num < 14)
            {
                cardData = num;
                text.text = cardData.ToString();
            }
            inputField.text = string.Empty;
        }
    }

    public void OnChangeCardOn()
    {
        inputField.interactable = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponentInChildren<InputField>();
        inputField.interactable = false;
        cardData = 1;
        text.text = cardData.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Image image = GetComponent<Image>();
        image.raycastTarget = false;
        Manager._instance.DragItem = this.gameObject;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        Image image = GetComponent<Image>();
    }
}
