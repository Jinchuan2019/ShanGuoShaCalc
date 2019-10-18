using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ConditionPanelController : MonoBehaviour, IDropHandler
{
    public int GetConditionFlag()
    {
        int rlt = 0b0;
        foreach(Transform cardTrans in transform)
        {
            int idx = Manager._instance.cardList.FindIndex((g)=>(g==cardTrans.gameObject.GetComponent<Card>().gameObject));
            rlt += (0b1 << idx);
        }
        return rlt;
    }
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform rectTransform = transform as RectTransform;
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {

            if (Manager._instance.DragItem != null)
            {
                Manager._instance.DragItem.transform.SetParent(this.transform);
                Manager._instance.DragItem.GetComponent<Image>().raycastTarget = true;
                Manager._instance.DragItem = null;
            }
        }
    }
}
