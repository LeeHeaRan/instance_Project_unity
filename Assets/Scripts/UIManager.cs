using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

/// <summary>
/// UI�� �����Ѵ�.
/// </summary>
public class UIManager : MonoBehaviour
{
    public instanceDataManager DataManager;
    public GameObject items;
    bool isItemsOpen = false;

    private void Start()
    {
        DataManager.AddClassAll(); //Ŭ���� ���� ��
        setItems(); //UI�� ����
    }

    /// <summary>
    /// ����� itemUI�� �������� ����. �޴��� ������.
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
    /// ���ΰ�ħ ��ư�� �������� ����
    /// </summary>
    public void restart_Button()
    {
        //UI����
        for (int i = 0; i < items.transform.childCount; i++)
        {
            if (items.transform.GetChild(i).name.Contains("item"))
            {
                items.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        //���� �����͵��� ���� ������ �ٽ� Ŭ���� ������ ����.
        //����Ʈ����
        DataManager.DeletClassAll();
        DataManager.AddClassAll();
        //UI�� �����Ѵ�.
        setItems();
    }

    /// <summary>
    /// �����۵��� UI�� �����Ѵ�.
    /// </summary>
    void setItems()
    {
        int count = 0;
        //�ν������� ����Ʈ�� ����� Ȯ���ϰ� �� ���� ��ŭ UI�� ���̰� �Ѵ�.
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
