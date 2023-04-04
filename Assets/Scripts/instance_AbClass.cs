using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
abstract public class instance_AbClass : MonoBehaviour
{
    protected GameObject model;
    private string model_name;
    private float model_size;
    private int model_count;
    private GameObject parent;

    private float area;

    /// <summary>
    /// �߻�Ŭ���� ������
    /// </summary>
    public instance_AbClass(GameObject _model, string _model_name, float _model_size, int _model_count, float _area, GameObject parent)
    {
        if (_model == null)
        {
            Debug.LogError("�ν��Ͻ� �׸� ���� ����ֽ��ϴ�.");

        }
        else if (_model_name == "")
        {
            Debug.LogError("�ν��Ͻ� �׸� �̸��� ����ֽ��ϴ�.");
        }
        else
        {
            this.model = _model;
            this.model_name = _model_name;
            this.model_size = _model_size;
            this.model_count = _model_count;
            this.area = _area;
            this.parent = parent;
            //Debug.Log("�ν��Ͻ� �������");
        }
    }

    /// <summary>
    /// �������� �ν��Ͻ��Ѵ�.
    /// </summary>
    protected void create_instance(Vector3 pos)
    {
        if (model_count != 1)
        {
            //������ Ȯ���Ͽ� �������� �����ϰ� ����.
            for(int i=0; i<model_count; i++)
            {
                GameObject obj = Instantiate(model, new Vector3(UnityEngine.Random.Range(pos.x-area, pos.x+area), pos.y, UnityEngine.Random.Range(pos.z - area, pos.z + area)), Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f,360f),0f)));
                obj.transform.localScale = new Vector3(model_size, model_size, model_size);
                obj.transform.parent = parent.transform;
            }

        }
        else //�ϳ��� ���� Ŭ���� ��ġ�� ����
        {
            GameObject obj = Instantiate(model, pos, Quaternion.identity);
            obj.transform.localScale = new Vector3(model_size, model_size, model_size);
            obj.transform.parent = parent.transform;
        }
    }

    /// <summary>
    /// �� �����պ��� �ٸ� �ִϸ� �����Ѵ�. �߻�޼��� *�ڽ�Ŭ�������� ���������ڸ� ������ �� ����.
    /// </summary>
    abstract public void play_Ani();


    /// <summary>
    /// �����ϰ� ���� ���� �޾ƿ´�.
    /// </summary>
    /// <param name="item">0: MODEL_NAME 1: MODEL_SIZE: 2. AREA 3. DENSITY 4. MODEL_COUNT</param>
    /// <returns></returns>
    protected string getData(ITEM item)
    {
        string result = "";

        switch (item)
        {
            case ITEM.MODEL_NAME:
                result = model_name;
                break;
            case ITEM.MODEL_SIZE:
                result = ITEM.MODEL_SIZE.ToString() + ":" + model_size;
                break;
            case ITEM.AREA:
                result = ITEM.AREA.ToString() + ":" + area;
                break;
            case ITEM.MODEL_COUNT:
                result = ITEM.MODEL_COUNT.ToString() + ":" + model_count;
                break;
            case ITEM.PARENT:
                result = ITEM.PARENT.ToString() + ":" + parent.name;
                break;
            default:
                result = "��ȿ���� �ʴ� �Ӽ��Դϴ�.!";
                break;
        }

        return result;
    }

    /// <summary>
    /// parent ������Ʈ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    protected GameObject getData()
    {
        return parent;
    }

    protected void setData(instantStr str)
    {
        model_name = str.model_name;
        model_size = str.model_size;
        model_count = str.model_count;
        area = str.area;
    }
}

