using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_7d886765 : ProjectComponent_Base
  {
    static readonly string s_scriptDataId = "7d886765-38fa-485c-8cad-6885efc629fc";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAyBJREFUSEullktoE1EUhv877yRtaE1fQqSUqmCpcREr7Wqg0BpQ1E10IxQXxs5MJ1RM00Ihk2gtKIVSfIC7oriJOxEFN6XVhQsXPpZdiN25irooCuqVM5nRNn3YqRf+CZMw5zvn3P+eCQAoAEQAKgAZgJTJQF4tYuhbAWO71eo0hnQdGtrboQ0kEPGC06fCOQReQoU7WOUFVAKLni2hMpeCCr0ZdYKAl40RHPaqkVxAEZUfRYxzB1JgLUAjkAtIxxECwKNhpLxWyek0lO/XMPtpwv2OKtt2zczMNDmOo3HOBbrnZSgbAGEVgwTojSOkirg5MIBIvPob7c22K5fLpSzLqhiGcSufz/fyBX1jBT5Ab4dG97EQTgGgjKTagLVrdHR00DAMTrJte+n1vWT4D+DkXoTXACQfEFXd9gQGmKa5HpDuQp0HOE79TnrAfwGo3+VyWXEcR9oWQF4VGN43hnGCAu4UQIGpHZZlLZqm+cCyrM0BFEAWcUcRcddxIKR7PVdVAeSgTTeZABTMz3zLCgjQ3YJWxvBZk5BP7kcnAeoVtyKqhqy7YQUC0CUawjHG8IWCuy5S0Q+gpTaw7/XAAOr3vmZ0yiKmwipmuzvQSgduXfSq5/tGRkbO0cEKAmDemMD+NjTLIm4zhoos4oauVxOgzIeHh8coCG2obdsfDMMg7QhAm8miUewRGN4yhneMYUVgeJHuouHHhWw2O+E7xVft/VYAurhZygzXBQFP7BTUvgRazvQglkpBJYBpmlO1wTYTWffpXCq6FkBOkZw0FGpLPIJuagudbMbwkWwbBEBVXc4ai7zA1gFEb1Rw/QgaNAmTvpsySYSDAEi2dYnz4l8AHSzBG9f8fAKRWAinIyFYdDYySci7AqypgGYRtYRmkQugvqsyphUJDx16gQQEZDepQGmqziLeGkO/pmBCFPD1/llc8C0aBFBbAVlU6WhAwu87tebVRazwq3jMi7jyqyTl3kwefPRs/OjyTvR8vGe5FkBTVarXMETZH2jDIe5gnpewyAskRlr6GUB8is37AHrB0EBzlUy6QJH+uvyvqDO/AfH2w/2NPldrAAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("7d886765-38fa-485c-8cad-6885efc629fc");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_7d886765() : base(GetResource(s_scriptDataId), s_scriptIconData,
        name: "UrbGEN generator",
        nickname: "UrbGEN generator",
        description: @"UrbGEN generator component",
        category: "UrbGEN",
        subCategory: "UrbGEN"
        )
    {
    }

    protected override void AppendAdditionalComponentMenuItems(SWF.ToolStripDropDown menu)
    {
      base.AppendAdditionalComponentMenuItems(menu);
      if (m_script is null) return;
      m_script.AppendAdditionalMenuItems(this, menu);
    }

    protected override void RegisterInputParams(GH_InputParamManager _) { }

    protected override void RegisterOutputParams(GH_OutputParamManager _) { }

    protected override void BeforeSolveInstance()
    {
      if (m_script is null) return;
      m_script.BeforeSolve(this);
    }

    protected override void SolveInstance(IGH_DataAccess DA)
    {
      if (m_script is null) return;
      m_script.Solve(this, DA);
    }

    protected override void AfterSolveInstance()
    {
      if (m_script is null) return;
      m_script.AfterSolve(this);
    }

    public override void RemovedFromDocument(GH_Document document)
    {
      ProjectComponentPlugin.DisposeScript(this, m_script);
      base.RemovedFromDocument(document);
    }

    public override BoundingBox ClippingBox
    {
      get
      {
        if (m_script is null) return BoundingBox.Empty;
        return m_script.GetClipBox(this);
      }
    }

    public override void DrawViewportWires(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawWires(this, args);
    }

    public override void DrawViewportMeshes(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawMeshes(this, args);
    }
  }
}
