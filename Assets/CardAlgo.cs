using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAlgo : MonoBehaviour
{
    public List<int> cardList = new List<int>();
    List<int> combinationFlagList = new List<int>();

    public string OutPutString = string.Empty;
    public int extraFlag0 = 0;
    public int extraFlag1 = 0;
    public void Go()
    {
        OutPutString = string.Empty;
        combinationFlagList.Clear();
        cardList.Sort();
        int totalCount = (0b1 << cardList.Count) - 1;
        for(int i = 1; i < totalCount+1; i++)
        {
            combinationFlagList.Add(i);
        }
        combinationFlagList.Sort((a, b) => { return FlagToSum(a) - FlagToSum(b); });
        for(int i = 0; i < totalCount-1; i++)
        {
            if (FlagToSum(combinationFlagList[i]) > FlagToSum(combinationFlagList[combinationFlagList.Count - 1] )/ 2){
                break;
            }
            for(int j = i + 1; FlagToSum(combinationFlagList[i]) == FlagToSum(combinationFlagList[j]); j++)
            {
                if ((combinationFlagList[i] & combinationFlagList[j])==0)
                {
                    if (((combinationFlagList[i] & extraFlag0) == extraFlag0) &&
                        ((combinationFlagList[j] & extraFlag1) == extraFlag1))
                    {
                        OutPutString +=  FlagDetail(combinationFlagList[i]);
                        OutPutString += "\n";
                        OutPutString += FlagDetail(combinationFlagList[j]);
                        OutPutString += "\n-------------------------------\n";
                    }
                }
            }
        }
    }

    int firstOneLeft(int n)
    {
        int cnt = -1;
        while (n != 0b0)
        {
            cnt++;
            n = n >> 1;
        }
        return cnt;
    }
    int FlagToSum(int flag)
    {
        int rlt = 0;
        for(int i = 0; i < cardList.Count; i++)
        {
            rlt += (flag & (0b1 << i))!=0 ? cardList[i] : 0;
        }
        return rlt;
    }
    string FlagDetail(int flag)
    {
        string output = string.Empty;
        for(int i = 0; i < cardList.Count; i++)
        {
            if((flag & (0b1 << i)) != 0)
            {
                output += cardList[i].ToString()+" ";
            }
        }
        return output;
    }
}
