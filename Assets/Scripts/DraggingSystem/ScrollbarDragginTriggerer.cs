using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ScrollbarDragginTriggerer : DraggingTriggerer
{ 
    private bool isDragging;
    public override bool IsDragging()
    {   return isDragging;  }

    private float prevScrollbarValue;
    Scrollbar scrollbar;

    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        isDragging = false;
        prevScrollbarValue = scrollbar.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevScrollbarValue != scrollbar.value)
        { isDragging = true;}
        prevScrollbarValue = scrollbar.value;
    }
}
