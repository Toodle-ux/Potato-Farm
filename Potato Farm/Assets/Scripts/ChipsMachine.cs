using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsMachine : MonoBehaviour
{
    [Header("bool")]

    // SerializedField使变量在unity里能直接看见
    [SerializeField] private bool hasPotato;
    [SerializeField] private float timer;

    // 用于储存GlobalController
    private GlobalController global;

    // 用于储存传过来的土豆
    [SerializeField] private GameObject potato;

    // Start is called before the first frame update
    void Start()
    {
        // 找到名为“Global"的game object并且调取Global Controller组件
        global = GameObject.Find("Global").GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果机器里没有土豆，把自己的transform发送给global controller的GetPotato方法（去要一颗土豆）
        if (!hasPotato)
        {
            // 如果土豆列表里有土豆的话，那么把机器状态更新为有土豆，否则一直是没有土豆，反复向土豆列表索要土豆
            if (global.GetPotato(transform))
            {
                hasPotato = true;
            }
        }

        // 计时器，如果倒计时大于0，那么就减去两帧之间的时间
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            // 如果已经传来了一个土豆（土豆已经到达机器），那么把土豆关掉，并且把机器状态更新为没土豆
            if (potato != null)
            {
                potato.SetActive(false);
                hasPotato = false;

                potato = null;
            }
        }
    }

    // 如果传过来了一个土豆，那么用ChipsMachine里的土豆变量把土豆装起来，并把倒计时设为10s
    public void PotatoArrived(GameObject potato)
    { 
        this.potato = potato;
        timer = 2.0f;

        print(potato.name);
    }
}
