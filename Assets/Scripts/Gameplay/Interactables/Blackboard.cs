using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public GameObject linePrefab;

    Line activeLine;

    public List<Line> lines;

    RectTransform rectTransform;
    bool isDrawing = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool isMouseInside = rectTransform.rect.Contains(point);

        if (isMouseInside)
        {
            Draw(point);
        }
        else
        {
            if(isDrawing)
            {
                activeLine = null;
                isDrawing = false;
            }
        }
    }

    private void OnDisable()
    {
        if(lines!=null)
        {
            for(short i=0; i<lines.Count; i++)
            {
                Destroy(lines[i].gameObject);
            }

            lines.Clear();
        }
    }

    void Draw(Vector2 point)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();

            if (lines == null)
            {
                lines = new List<Line>
                {
                    activeLine
                };
            }
            else
                lines.Add(activeLine);

            isDrawing = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
            isDrawing = false;
        }

        if (activeLine != null)
        {
            activeLine.UpdateLine(point);
        }
    }
}
