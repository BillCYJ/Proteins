using PolymerModel.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

public class ProteinDisplayController : Controller
{
    /// <summary>显示蛋白质</summary>
    public void ShowProtein()
    {
        ProteinDisplayModel model = GetModel<ProteinDisplayModel>();
        Protein protein = CoreAPI.PostCommand<PdbLoaderModule, GetProteinDataCommand, Protein>(new GetProteinDataCommand());
        if (model.DisplayedProteinData != null)
        {
            if (model.DisplayedProteinData.ID == protein.ID)
            {
                return;
            }
            else
                DestroyProtein();
        }
        model.DisplayedProteinData = protein;
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        view.ShowProtein(protein);
    }

    public void ShowDisplayView()
    {
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        view.scaleSlider.onValueChanged.AddListener(ChangeModelScale);
        view.resetScaleBtn.onClick.AddListener(ResetScale);

        view.toggle_ball_stick.onValueChanged.AddListener(OnBallStickToggleChanged);
        view.toggle_ball.onValueChanged.AddListener(OnBallToggleChanged);
        view.toggle_stick.onValueChanged.AddListener(OnStickToggleChanged);
    }

    public void ChangeModelScale(float value)
    {
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        // slider.value的值范围是[0,1]，相要的scale范围是[0.25,4]
        if (value <= 0.5) //缩小
        {
            view.Displayer3DRoot.transform.localScale = new Vector3(value * 1.5f + 0.25f, value * 1.5f + 0.25f, value * 1.5f + 0.25f);
        }
        else //放大
        {
            view.Displayer3DRoot.transform.localScale = new Vector3(value * 6f - 2f, value * 6f - 2f, value * 6f - 2f);
        }
    }

    public void ResetScale()
    {
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        view.Displayer3DRoot.transform.localScale = new Vector3(1, 1, 1);
        view.scaleSlider.value = 0.5f;
    }

    public void OnBallStickToggleChanged(bool value)
    {
        if (value == false) return;
        ProteinDisplayModel model = GetModel<ProteinDisplayModel>();
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        Protein protein = model.DisplayedProteinData;
        view.displayMode = DisplayMode.BallStick;
        DestroyProtein();
        ResetScale();
        view.ShowProtein(protein);
    }

    public void OnBallToggleChanged(bool value)
    {
        if (value == false) return;
        ProteinDisplayModel model = GetModel<ProteinDisplayModel>();
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        Protein protein = model.DisplayedProteinData;
        view.displayMode = DisplayMode.Spacefill;
        DestroyProtein();
        ResetScale();
        view.ShowProtein(protein);
    }

    public void OnStickToggleChanged(bool value)
    {
        if (value == false) return;
        ProteinDisplayModel model = GetModel<ProteinDisplayModel>();
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        Protein protein = model.DisplayedProteinData;
        view.displayMode = DisplayMode.Stick;
        DestroyProtein();
        ResetScale();
        view.ShowProtein(protein);
    }

    //public void OnBallStickToggleChanged(bool value)
    //{
    //    ProteinDisplayModel model = GetModel<ProteinDisplayModel>();
    //    ProteinDisplayView view = GetView<ProteinDisplayView>();
    //    Protein protein = model.DisplayedProteinData;
    //    view.displayMode = value ? DisplayMode.BallStick : DisplayMode.Spacefill;
    //    DestroyProtein();
    //    view.ShowProtein(protein);
    //}

    public void ShowInfoInBoard(AtomDisplayer atomDisplayer)
    {
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        view.SetBoardInfo(atomDisplayer);
    }

    /// <summary>销毁蛋白质分子模型</summary>
    private void DestroyProtein()
    {
        ProteinDisplayView view = GetView<ProteinDisplayView>();
        view.DestroyProtein();
    }
}