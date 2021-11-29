using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDrawner : MonoBehaviour{
    
    public GameObject LinePrefab;

    public float LinePointsMinDistance;
    public float LineWidth;

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;
    
    private Line currentLine;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();
        
        
        
        if (currentLine != null)
        {
            Draw();
        }
    }

    void BeginDraw()
    {
        currentLine = Instantiate(LinePrefab,transform.position,Quaternion.identity,transform).GetComponent<Line>();
        
        currentLine.SetPointsMinDistance(LinePointsMinDistance);    
        currentLine.SetLineWidth(LineWidth);    
    }
    
    void Draw()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);
        
        foreach (var result in results)
        {
            Vector2 mousePosition =Input.mousePosition;
            currentLine.AddPoint(mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }
    
    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                StartCoroutine(DistributionOfCharacters());
            }
        }
    }

    IEnumerator DistributionOfCharacters()
    {
        Debug.Log("Поставить челиков");
        yield return new WaitForSeconds(0.1f);
        Destroy(currentLine.gameObject);
    }
}
