using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingPotato : MonoBehaviour
{

    public Image[] slots;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
