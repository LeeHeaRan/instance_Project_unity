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
/// ����ڰ� �ν����Ϳ��� ���� �ִ� ����ü
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
    /// instance_infos�� �ִ� �����͸� ��� �ش� Ŭ������ �ν��Ͻ� �Ѵ�.
    /// </summary>
    public void AddClassAll()
    {
        //�� �����ۺ� �θ������Ʈ�� �������ش�.
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
        //������ �ִ� ������Ʈ�� �״�� �ΰ�. 
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
    /// �����Ǿ��ִ� Ŭ���� �ν��Ͻ����� ��� �����Ѵ�.
    /// </summary>
    public void DeletClassAll()
    {
        grassList.Clear();
        flowerList.Clear();
    }

    /// <summary>
    /// instance_info�� ���� �ְ� flowerList, grassList���� ��ġ�ϴ� ���� ã����� list�� �ε����� ��ȯ���ش�.
    /// </summary>
    /// <param name="selectItem"></param>
    /// <returns></returns>
    public int getItemDataIndex(int selectItem)
    {
        //instant_finfos���� ���� �ִ��� Ȯ���ϰ� ���� �ִٸ� �ε��� ���� �����ش�.
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
    /// ���� �����ϴ� �Լ��� ȣ���Ѵ�. ȣ��� �Բ� inspectorâ�� ���� �� ������ ������Ʈ���ش�.
    /// </summary>
    /// <param name="pos"></param> Ŭ���� ���콺�� hit�� ������.
    /// <param name="isFlower"></param> ������ ����.
    /// <param name="index"></param> ������ �����ۿ��� �ش��ϴ� �������� grassList, flowerList���� ã�Ƴ� �ε���
    /// <param name="selectIndex"></param> ������ �������� instant_infos�� �ε���
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

