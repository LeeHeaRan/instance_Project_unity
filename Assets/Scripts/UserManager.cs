using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ����ڰ� ���ϴ� �ൿ�� ���� ����. ����ڰ� �����Ͽ� ����Ѵ�.
/// </summary>
public class UserManager : MonoBehaviour
{
    instanceDataManager _dataManager;

    int selectItem = 0;
    Vector3 pos = Vector3.zero;

    void Start()
    {
        _dataManager = this.GetComponent<instanceDataManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name.Contains("Stage"))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        clickGround(hit.point);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ����ڰ� �������� ����������. selectItem�� �����Ѵ�.
    /// </summary>
    /// <param name="index"></param>
    public void clickItemsButton(int index)
    {
        selectItem = index;
    }

    /// <summary>
    /// ���� Ŭ��������. ���� �����Ѵ�. �ش� list�� create�Լ��� �ҷ��´�.
    /// </summary>
    void clickGround(Vector3 _pos)
    {
        //�ش縮��Ʈ���� ���� ����� �ε����� �޴´�.
        int selectItemclassIndex = _dataManager.getItemDataIndex(selectItem);

        if (selectItemclassIndex != -1)
        {
            //Debug.Log("index:" + selectItemclassIndex);
            bool selectItemisFlower = _dataManager.instant_infos[selectItem].isFlower;
            _dataManager.createItem(_pos, selectItemisFlower, selectItemclassIndex, selectItem);
        }
        else
        {
            //�ùٸ� �����͸� �ҷ����� ����.
            Debug.LogError("������ �������� �����͸� �ùٸ��� �������� ���Ͽ����ϴ�.");
        }

    }
}


