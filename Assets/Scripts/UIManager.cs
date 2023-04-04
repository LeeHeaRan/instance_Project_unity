using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

/// <summary>
/// UI를 관리한다.
/// </summary>
public class UIManager : MonoBehaviour
{
    public instanceDataManager DataManager;
    public GameObject items;
    bool isItemsOpen = false;

    private void Start()
    {
        DataManager.AddClassAll(); //클래스 생성 후
        setItems(); //UI를 셋팅
    }

    /// <summary>
    /// 상단의 itemUI를 눌렀을때 실행. 메뉴가 열린다.
    /// </summary>
    public void opencloseItemUI()
    {
        if (!isItemsOpen)
        {
            isItemsOpen = true;
            iTween.ValueTo(this.gameObject, iTween.Hash("from", items.GetComponent<RectTransform>().anchoredPosition.y, "to", 0f, "time", 1f, "onupdate", "positionMove", "easetype", iTween.EaseType.easeOutQuart));
        }
        else
        {
            iTween.ValueTo(this.gameObject, iTween.Hash("from", items.GetComponent<RectTransform>().anchoredPosition.y, "to", 450f, "time", 1f, "onupdate", "positionMove", "easetype", iTween.EaseType.easeOutQuart));
            isItemsOpen = false;
        }
    }



    void positionMove(float val)
    {
        items.GetComponent<RectTransform>().anchoredPosition = new Vector2(items.GetComponent<RectTransform>().anchoredPosition.x, val);
    }


    /// <summary>
    /// 새로고침 버튼을 눌렀을때 셋팅
    /// </summary>
    public void restart_Button()
    {
        //UI정리
        for (int i = 0; i < items.transform.childCount; i++)
        {
            if (items.transform.GetChild(i).name.Contains("item"))
            {
                items.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        //기존 데이터들을 지운 다음에 다시 클래스 생성을 실행.
        //리스트정리
        DataManager.DeletClassAll();
        DataManager.AddClassAll();
        //UI를 셋팅한다.
        setItems();
    }

    /// <summary>
    /// 아이템들의 UI를 셋팅한다.
    /// </summary>
    void setItems()
    {
        int count = 0;
        //인스펙터의 리스트가 몇개인지 확인하고 그 갯수 만큼 UI를 보이게 한다.
        for (int i = 0; i < DataManager.instant_infos.Count; i++)
        {
            if (items.transform.GetChild(count).name.Contains("item"))
            {
                //Debug.Log(itemsUI.transform.GetChild(count).name);
                items.transform.GetChild(count).gameObject.SetActive(true);
                items.transform.GetChild(count).GetChild(0).gameObject.GetComponent<Text>().text = DataManager.instant_infos[i].model_name;
            }
            else
            {
                i--;
            }
            count++;
        }
    }

}
