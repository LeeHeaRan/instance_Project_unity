using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// flower클래스와 애니만 다른 instance_class 
/// </summary>
public class flower_Class : instance_AbClass
{
    public flower_Class(GameObject _model, string _model_name, float _model_size, int _model_count, float _area, GameObject parent)
       : base(_model, _model_name, _model_size, _model_count, _area, parent)
    {
        Debug.Log("flower_Class의 부모 생성자가 호출되었습니다.");
    }

    public override void play_Ani()
    {
        //해당하는 애니메이션을 실행한다.
        //base.model.GetComponent<Animator>().Play("Animation", -1, 0f); ...sample
    }

    public void create_Flower(Vector3 pos)
    {
        play_Ani();
        base.create_instance(pos);
    }

    public string getData(ITEM _item)
    {
        return base.getData(_item);
    }
    public GameObject getData()
    {
        return base.getData();
    }
    public void setData(instantStr _str)
    {
        base.setData(_str);
    }
}
