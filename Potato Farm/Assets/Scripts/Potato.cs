using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    // 设置两个状态机
    public enum State
    {
        walk, run
    }

    // 储存当前状态机的变量
    public State currentState;

    public float moveSpeed;

    private Rigidbody2D rb2D;

    // 土豆的移动目标
    private Vector2 target;

    private bool isArrived = false;

    // 用来接传过来的machine
    private ChipsMachine machine;

    // Start is called before the first frame update
    void Start()
    {
        // 调取自己的刚体组件
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 常用的移动物体的方法，Time.deltaTime用于smooth the movement
        // transform.position += transform.right * moveSpeed * Time.deltaTime;

        // 如果土豆在四处乱晃的状态下到达了上一个目的地，那么给它设立下一个目的地
        if (currentState == State.walk && isArrived)
        {
            float x = Random.Range(-3.0f, 3.0f);
            float y = Random.Range(-3.0f, 3.0f);
            target = new Vector2(x, y) + (Vector2)transform.position;
            isArrived = false;
        }
        else if (currentState == State.run)
        {

        }

        // 土豆向目的地移动
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // 当土豆到达目的地，但isArrived的标记还没设置成true时，把isArrived设置成true
        if (Vector2.Distance(transform.position, target) < 0.1f && !isArrived)
        {
            isArrived = true;

            // 如果土豆是在去机器的路上这一状态下，那么把自己传输给机器的PotatoArrived方法
            if (currentState == State.run)
            {
                machine.PotatoArrived(gameObject);
            }
            else if(currentState == State.walk)
            {

            }

        }

    }


    private void FixedUpdate()
    {
        // 刚体相关的内容要放在FixedUpdate里
        // rb2D.velocity = transform.right * moveSpeed + rb2D.velocity.y * transform.up;
    }

    // （当machine空闲时）调用这个方法，并且把自己的transform传过来（让土豆走到machine那里）
    public void GoDie(Transform machine)
    {
        // 把土豆的状态改为向机器移动，并且把目标设置为机器的地址
        currentState = State.run;
        target = machine.position;

        // 传过来的machine时Transform类，从中调取ChipsMachine并且赋值给Potato类中的machine
        this.machine = machine.GetComponent<ChipsMachine>();
    }
}
