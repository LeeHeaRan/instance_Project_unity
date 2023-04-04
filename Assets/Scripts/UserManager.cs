using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 사용자가 행하는 행동에 대한 반응. 사용자가 접근하여 사용한다.
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
    /// 사용자가 아이템을 선택했을때. selectItem을 갱신한다.
    /// </summary>
    /// <param name="index"></param>
    public void clickItemsButton(int index)
    {
        selectItem = index;
    }

    /// <summary>
    /// 땅을 클릭했을때. 모델을 생성한다. 해당 list의 create함수를 불러온다.
    /// </summary>
    void clickGround(Vector3 _pos)
    {
        //해당리스트에서 값의 존재와 인덱스를 받는다.
        int selectItemclassIndex = _dataManager.getItemDataIndex(selectItem);

        if (selectItemclassIndex != -1)
        {
            //Debug.Log("index:" + selectItemclassIndex);
            bool selectItemisFlower = _dataManager.instant_infos[selectItem].isFlower;
            _dataManager.createItem(_pos, selectItemisFlower, selectItemclassIndex, selectItem);
        }
        else
        {
            //올바른 데이터를 불러오지 못함.
            Debug.LogError("선택한 아이템의 데이터를 올바르게 가져오지 못하였습니다.");
        }

    }
}


