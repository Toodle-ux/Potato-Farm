using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButton : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject potatoPrefab;
    [SerializeField] private Transform potatoGroup;

    private GlobalController global;

    // Start is called before the first frame update
    void Start()
    {
        potatoGroup = GameObject.Find("Potatoes").transform;
        global = GameObject.Find("Global").GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 按下鼠标左键时
        if (Input.GetMouseButtonDown(0))
        {
            // 向鼠标位置发送一道射线，把信息储存在ray里
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 在layerMask层上，从ray的起始点开始，以ray的方向，打出无限长的一条射线，信息储存在hit中
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 999f, layerMask);

            // 如果射线检测击中了某个物体
            if (hit)
            {
                // 如果击中的碰撞体的gameobject就是自己这个按钮本身
                // 因为每个种植按钮都有一个script，因此会有n条射线，但只有自己的script打中自己的按钮时才进行土豆种植
                if (hit.collider.gameObject == gameObject)
                {
                    // 生成一个土豆prefab，放在potatoGroup这个空物体下
                    GameObject temp = Instantiate(potatoPrefab, potatoGroup);

                    // 把新的土豆的位置设置在土豆种植点
                    temp.transform.position = transform.position;

                    // 把新的土豆增加到土豆列表里
                    global.potatoes.Add(temp);
                }
            }
        }
    }
}
