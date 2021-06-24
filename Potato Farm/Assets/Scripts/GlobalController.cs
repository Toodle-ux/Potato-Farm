using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // 所有土豆的列表
    public List<GameObject> potatoes;


    // Start is called before the first frame update
    void Start()
    {
        // 初始化土豆列表
        potatoes = new List<GameObject>();

        // 找到potatoGroup这个空物体下的transform组件
        Transform potatoGroup = GameObject.Find("Potatoes").transform;

        // 把potatoGroup下的每一个子物体加到列表里
        for (int i = 0; i < potatoGroup.childCount; i++)
        {
            potatoes.Add(potatoGroup.GetChild(i).gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 机器会调取这个方法，向土豆列表索要一个土豆
    public bool GetPotato(Transform machine)
    {
        // 如果没土豆的话，直接返回，不进行其他操作
        if (potatoes.Count == 0)
        {
            return false;
        }

        // 有土豆的话，抓住第一个土豆，调取Potato类中的GoDie方法，再把machine的transform发送过去
        potatoes[0].GetComponent<Potato>().GoDie(machine);

        // 把这个土豆从列表中抹除
        potatoes.RemoveAt(0);

        return true;
    }
}
