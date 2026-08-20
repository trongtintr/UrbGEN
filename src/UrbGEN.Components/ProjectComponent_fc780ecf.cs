using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_fc780ecf : ProjectComponent_Base
  {
    static readonly string s_scriptDataId = "fc780ecf-bc3e-422f-ae07-99bb43ecb500";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAA/VJREFUSEullstvG1UUxr87Tz9aq62bpJGMoiphQRWCkElk6MJShWMjImCBkQqIFGyPPfGM48SPOHQxCXYiikCIp9RtC0JiBVIXCFVtFNgXyoYdgn+gacQOqbc6NzPpeOKUCo50PZ6J5/zu+e537g0AaABkADoAFYBiGFB3V3H273Vk/+vYXcfZdBohjI0hlJlC1E1OV41zSHwN29zBL+L6KKOHbd51v9O1i+1PctCRHsIRScLPx6N40q1GEQAH2//08AJ3oPzbcOw3Uo3F8lbbKn3FL0PlNxEikADkEwgD4LEIcq5Uaj4P7e4a5n+vYYruOQdr14uvXmwa4wgE55CX7PL3pmnyatXkq7VChn8L7QAgomOWAKkEwrqMDzIZRBMJhFMphOvV8lV62bYqdz5cPn8yAGBNu9y1bfH3HWepMPFQQHoMIbqPh/ESAMm2oS9a5S2aoW1VuNMpTPgBHqRTuzBNycW9HzA3iogPoHiAmC4kk+hZo1ZMkgzthbdfo2RBQDD6KziDIy4gS3onXaAfIF7ikB8lOUUfgLwqMfx2PIIXKeFhgP2XORjB/M+C0QegBKqMLzQZXzoOpHzKddUegHpD/Ei8yMFWbKNYt8pbreo7z/Ql5WIye9+DgMlhjDCGnZCCdnICZEV+VBMVUTX7s6UFpoV2F/yWV4njFE4s10vNllXIiwqDAPqIhTHDGO5SchpxHecADHvJKciidrW8Q5ZdrpnfiYbkkBarxpWFBZOTVTvmhemBANL7sSGMqzJ6ER0fT57GCDWcH0BBzSaayV1w0Wg144qoyjb5u9ZbM4MA9GORbOIUhlQZnzOGO6qMS+l0/yIPCpKouVxZb9lG8TCJaDFZLIYTEsOvjOE2Y/hLYvgpf+ZgFYPC76wggD7ELFWGDUnCNTsH/dkpDL8yjXguB93V+qHW9EcQQE5RnDw0kiURxSTJQp3NGP405hBpWOVPabsIWvOwGASQ3a2Cp5/CsZCCi56bOtXX5yzLFNYkSJ8UPu/7IwigxpLc7Zq/OYVoPIyXo2FUqTd6RnaUrEmQpl35zLNmq1ootuulJi2wl5gc1jCLhau97KgfQHsRSUJ7kQCQ7rqKTU3B144DJWjN9lKxRJakqhqLxZbYpxwo9kLpD+qRhmXcohPOX4F2cm8v4iNxnAtp6EgMu5dmMfeg8AdBHUsAaq6GJQASvwmFzgOCLtWMnf0j07WodvqYOL2E7iTN9Xn8wHtY4RvIHBibmL2+MvPRtc5zl/n7yHrPb6w+/d43red/vN0dt/0SEYB2VeVoCPM0+8dP4Yl7azB5Fz2+js1DR3fAsw332kPFA9ABQ84QI5kUQJn+dfm/g5S5D8zPMPtMl1M+AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("fc780ecf-bc3e-422f-ae07-99bb43ecb500");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_fc780ecf() : base(GetResource(s_scriptDataId), s_scriptIconData,
        name: "UrbGEN_PopulateRegion",
        nickname: "UrbGEN PopulateRegion",
        description: @"UrbGEN_PopulateRegion component",
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
