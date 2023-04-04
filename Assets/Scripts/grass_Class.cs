using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// flowerŬ������ �ִϸ� �ٸ� instance_class 
/// </summary>
/// 
public class grass_Class : instance_AbClass
{

    //�θ�Ŭ������ ������ ȣ��
    public grass_Class(GameObject _model, string _model_name, float _model_size, int _model_count, float _area, GameObject parent)
        : base(_model, _model_name, _model_size, _model_count, _area, parent)
    {
        Debug.Log("grass_Class�� �θ� �����ڰ� ȣ��Ǿ����ϴ�.");
    }

    public override void play_Ani()
    {
        //�ش��ϴ� �ִϸ��̼��� �����Ѵ�.
        //base.model.GetComponent<Animator>().Play("Animation", -1, 0f); ...sample
    }


    public void create_Grass(Vector3 pos)
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
