using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public static Manager _instance;
    bool ResetCard = true;
    public GameObject cardPrefab;
    public List<GameObject> cardList = new List<GameObject>();
    public GameObject setCardPanel;
    public Text cardNumText;
    public CardAlgo algo;
    public Text ResultText;
    public Text ResetCardButtonText;
    public ConditionPanelController Left;
    public ConditionPanelController Right;
    int cardNum = 4;
    [HideInInspector]
    public GameObject DragItem;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Screen.SetResolution(1920, 1080, false);
    }
    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            AddANewCard();
        }
    }
    public void CardNumUp()
    {
        if (cardNum + 1 <= 8)
        {
            cardNum += 1;
            cardNumText.text = cardNum.ToString();
            AddANewCard();
        }
    }
    public void CardNumDown()
    {
        if (cardNum - 1 >= 4)
        {
            cardNum -= 1;
            cardNumText.text = cardNum.ToString();
            DeleteACard();
        }
    }
    public void UpDateResult()
    {
        algo.cardList.Clear();
        algo.extraFlag0 = Left.GetConditionFlag();
        algo.extraFlag1 = Right.GetConditionFlag();
        for (int i=0;i< cardList.Count; i++)
        {
            algo.cardList.Add(cardList[i].GetComponent<Card>().cardData);
        }
        algo.Go();
        ResultText.text = algo.OutPutString;
    }
    public void AddANewCard()
    {
        GameObject card = Instantiate<GameObject>(cardPrefab);
        card.transform.SetParent(setCardPanel.transform);
        cardList.Add(card);
    }
    public void DeleteACard()
    {
        if (cardList.Count > 0)
        {
            Destroy(cardList[cardList.Count - 1]);
            cardList.RemoveAt(cardList.Count - 1);
        }
    }

    public void SetCard()
    {
        if (ResetCard)
        {
            foreach(var cardObj in cardList)
            {
                cardObj.GetComponent<Card>().OnChangeCardOn();
            }
            ResetCardButtonText.text = "Conform";
        }
        else
        {
            foreach (var cardObj in cardList)
            {
                cardObj.GetComponent<Card>().OnChangeCardConfirm();
            }
            ResetCardButtonText.text = "Edit Card";
        }
        ResetCard = !ResetCard;
    }
    
}
