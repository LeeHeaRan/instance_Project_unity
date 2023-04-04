using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public enum ITEM
{
    MODEL_NAME,
    MODEL_SIZE,
    AREA,
    MODEL_COUNT,
    PARENT
}

/// <summary>
/// 사용자가 인스펙터에서 값을 넣는 구조체
/// </summary>
[Serializable]
public struct instantStr
{
    public GameObject model;
    public string model_name;
    [Tooltip("limit from 1 to 3")]
    [Range(1, 3)] public int model_size;
    [Tooltip("limit from 1 to 10")]
    [Range(1, 10)] public int model_count;
    [Tooltip("limit from 0.1 to 1")]
    [Range(0.1f, 1)] public float area;

    [Tooltip("uncheck: grass check: flower")]
    public bool isFlower; //0 grass or 1 flower
}

public class instanceDataManager : MonoBehaviour
{
    public List<instantStr> instant_infos = new List<instantStr>();
    List<grass_Class> grassList = new List<grass_Class>();
    List<flower_Class> flowerList = new List<flower_Class>();
    public GameObject instanceObjects;
    List<GameObject> parentList = new List<GameObject>();

    /// <summary>
    /// instance_infos에 있는 데이터를 모두 해당 클래스로 인스턴스 한다.
    /// </summary>
    public void AddClassAll()
    {
        //각 아이템별 부모오브젝트를 생성해준다.
        createParentObj();

        for (int i = 0; i < instant_infos.Count; i++)
        {
            //is grass
            if (!instant_infos[i].isFlower)
            {
                grassList.Add(new grass_Class(instant_infos[i].model,
                    instant_infos[i].model_name, instant_infos[i].model_size,
                    instant_infos[i].model_count, instant_infos[i].area, findParentObj(instant_infos[i].model_name)));
            }
            else  //is flower
            {
                flowerList.Add(new flower_Class(instant_infos[i].model,
                    instant_infos[i].model_name, instant_infos[i].model_size,
                    instant_infos[i].model_count, instant_infos[i].area, findParentObj(instant_infos[i].model_name)));
            }
        }
    }


    void createParentObj()
    {
        //기존에 있는 오브젝트를 그대로 두고. 
        for (int index = 0; index < instant_infos.Count; index++)
        {
            if (!instanceObjects.transform.Find(instant_infos[index].model_name + "Parent"))
            {
                parentList.Add(new GameObject(instant_infos[index].model_name + "Parent"));
                parentList[parentList.Count - 1].transform.parent = instanceObjects.transform;
            }
        }
    }

    GameObject findParentObj(string name)
    {
        for (int i = 0; i < instanceObjects.transform.childCount; i++)
        {
            if (instanceObjects.transform.GetChild(i).name == name + "Parent")
            {
                return instanceObjects.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 생성되어있는 클래스 인스턴스들을 모두 제거한다.
    /// </summary>
    public void DeletClassAll()
    {
        grassList.Clear();
        flowerList.Clear();
    }

    /// <summary>
    /// instance_info에 값이 있고 flowerList, grassList에서 일치하는 값을 찾을경우 list의 인덱스를 반환해준다.
    /// </summary>
    /// <param name="selectItem"></param>
    /// <returns></returns>
    public int getItemDataIndex(int selectItem)
    {
        //instant_finfos에서 값이 있는지 확인하고 값이 있다면 인덱스 값을 보내준다.
        if (instant_infos[selectItem].isFlower && flowerList != null) //is flower
        {
            for (int i = 0; i < flowerList.Count; i++)
            {
                if (instant_infos[selectItem].model_name.Equals(flowerList[i].getData(ITEM.MODEL_NAME)))
                {
                    return i;
                }
            }
        }
        else if (!instant_infos[selectItem].isFlower && grassList != null) //is grass
        {
            for (int i = 0; i < grassList.Count; i++)
            {
                if (instant_infos[selectItem].model_name.Equals(grassList[i].getData(ITEM.MODEL_NAME)))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// 모델을 생성하는 함수를 호출한다. 호출과 함께 inspector창에 수정 된 정보를 업데이트해준다.
    /// </summary>
    /// <param name="pos"></param> 클릭한 마우스가 hit된 포지션.
    /// <param name="isFlower"></param> 꽃인지 구분.
    /// <param name="index"></param> 선택한 아이템에서 해당하는 아이템을 grassList, flowerList에서 찾아낸 인덱스
    /// <param name="selectIndex"></param> 선택한 아이템의 instant_infos의 인덱스
    public void createItem(Vector3 pos, bool isFlower, int index, int selectIndex)
    {
        if (!isFlower)
        {
            grassList[index].setData(instant_infos[selectIndex]);
            grassList[index].create_Grass(pos);
        }
        else
        {
            flowerList[index].setData(instant_infos[selectIndex]);
            flowerList[index].create_Flower(pos);
        }
    }
}

