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
    /// 추상클래스 생성자
    /// </summary>
    public instance_AbClass(GameObject _model, string _model_name, float _model_size, int _model_count, float _area, GameObject parent)
    {
        if (_model == null)
        {
            Debug.LogError("인스턴스 항목에 모델이 비어있습니다.");

        }
        else if (_model_name == "")
        {
            Debug.LogError("인스턴스 항목에 이름이 비어있습니다.");
        }
        else
        {
            this.model = _model;
            this.model_name = _model_name;
            this.model_size = _model_size;
            this.model_count = _model_count;
            this.area = _area;
            this.parent = parent;
            //Debug.Log("인스턴스 정상생성");
        }
    }

    /// <summary>
    /// 프리팹을 인스턴스한다.
    /// </summary>
    protected void create_instance(Vector3 pos)
    {
        if (model_count != 1)
        {
            //영역을 확인하여 갯수별로 랜덤하게 생성.
            for(int i=0; i<model_count; i++)
            {
                GameObject obj = Instantiate(model, new Vector3(UnityEngine.Random.Range(pos.x-area, pos.x+area), pos.y, UnityEngine.Random.Range(pos.z - area, pos.z + area)), Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f,360f),0f)));
                obj.transform.localScale = new Vector3(model_size, model_size, model_size);
                obj.transform.parent = parent.transform;
            }

        }
        else //하나일 경우는 클릭한 위치에 생성
        {
            GameObject obj = Instantiate(model, pos, Quaternion.identity);
            obj.transform.localScale = new Vector3(model_size, model_size, model_size);
            obj.transform.parent = parent.transform;
        }
    }

    /// <summary>
    /// 각 프리팹별로 다른 애니를 실행한다. 추상메서드 *자식클래스에서 접근지정자를 변경할 수 없다.
    /// </summary>
    abstract public void play_Ani();


    /// <summary>
    /// 참조하고 싶은 값을 받아온다.
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
                result = "유효하지 않는 속성입니다.!";
                break;
        }

        return result;
    }

    /// <summary>
    /// parent 오브젝트를 반환한다.
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

