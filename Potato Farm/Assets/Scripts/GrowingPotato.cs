using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingPotato : MonoBehaviour
{

    [Header("Image")]
    public Image[] slots;

    [Header("Sprite")]
    public Sprite heart;

    [Header("LayerMask")]
    [SerializeField] private LayerMask layerMask;

    [Header("Timer")]
    [SerializeField] private float timer = 999f;

    [Header("Prefabs")]
    [SerializeField] private GameObject potatoPrefab;
    [SerializeField] private GameObject electronicPrefab;

    // 以下将在start中寻找
    private GameObject canvas;
    private Transform potatoGroup;
    private GlobalController global;

    // Start is called before the first frame update
    void Start()
    {

        // 找到对应的组件
        canvas = transform.GetChild(0).gameObject;
        potatoGroup = GameObject.Find("Potatoes").transform;
        global = GameObject.Find("Global").GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 点生长中的土豆 出现种植面板
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 999f, layerMask);

            if (hit)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    canvas.SetActive(true);
                }
            }
        }

        // 倒计时
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        // 如果时间到了，根据肥料的组合生成一个土豆
        else
        {
            if (slots[0].sprite == heart)
            {
                GameObject temp = Instantiate(electronicPrefab, potatoGroup);
                temp.transform.position = transform.position;
                global.potatoes.Add(temp);
            }
            else
            {
                GameObject temp = Instantiate(potatoPrefab, potatoGroup);
                temp.transform.position = transform.position;
                global.potatoes.Add(temp);
            }

            // 把生长中的土豆关掉
            gameObject.SetActive(false);
        }
    }

    // 点击肥料的按钮，对应肥料会出现在slot里
    public void FertilizerButton(Sprite sprite)
    {
        // slots[0].sprite = sprite;

        for (int i = 0; i < slots.Length; i++)
        {
            
            if(slots[i].sprite == null)
            {
                slots[i].sprite = sprite;
                return;
            }
        }
    }

    // 点击种植按钮，开始倒计时
    public void PlantButton()
    {
        timer = 2f;

    }
}
