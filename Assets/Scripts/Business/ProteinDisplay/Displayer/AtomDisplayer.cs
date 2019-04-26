using PolymerModel.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtomDisplayer : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>该原子位于蛋白质中的哪个氨基酸</summary>
    public AminoacidInProtein AminoacidInProtein {
        get; set;
    }

    /// <summary>该原子位于氨基酸中的哪个原子</summary>
    public AtomInAminoacid AtomInAminoacid {
        get; set;
    }

    // 鼠标操作
    //private void OnMouseEnter() {
    //    CoreAPI.SendCommand<ProteinDisplayModule, ShowInfoInBoardCommand>(new ShowInfoInBoardCommand(this));
    //}
    //private void OnMouseExit() {
    //    CoreAPI.SendCommand<ProteinDisplayModule, ShowInfoInBoardCommand>(new ShowInfoInBoardCommand(null));
    //}

    // HTC手柄操作
    public void OnDrop(PointerEventData data)
    {
        //
    }
    public void OnPointerEnter(PointerEventData data)
    {
        CoreAPI.SendCommand<ProteinDisplayModule, ShowInfoInBoardCommand>(new ShowInfoInBoardCommand(this));
    }
    public void OnPointerExit(PointerEventData data)
    {
        CoreAPI.SendCommand<ProteinDisplayModule, ShowInfoInBoardCommand>(new ShowInfoInBoardCommand(null));
    }
    // 使用data的例子：
    //private Sprite GetDropSprite(PointerEventData data)
    //{
    //    var originalObj = data.pointerDrag;
    //    if (originalObj == null)
    //        return null;

    //    var dragMe = originalObj.GetComponent<DragImage>();
    //    if (dragMe == null)
    //        return null;

    //    var srcImage = originalObj.GetComponent<Image>();
    //    if (srcImage == null)
    //        return null;

    //    return srcImage.sprite;
    //}
}
