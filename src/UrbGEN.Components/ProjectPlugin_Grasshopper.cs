using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Resources;
using SD = System.Drawing;

using Rhino;
using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class AssemblyInfo : GH_AssemblyInfo
  {
    static readonly string s_assemblyIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAABFpJREFUSEullf9PVWUcx18U80tJAuJXBEQk5ZtASiKkNvkSAsoXuYEX4S7wXrrn3C/nXjggtrqVOrO51vqptVpbq9ZYP8Asl9JihRMzUFFAhF2+aKLi5r9Qe56ueLkjF/bezu7u8zzn/XrO57w/zwFYwjzl8fBM4NiTNC+AZxcR11QsExovwX8DzQsw7kLx7Caxy0RV1yH0qw6SA9cEal6ABzo1dVvZCKQrWaT90YDrVgvGwHX+mgXotZDSXsUaIMhvOEjU/aaN8jEXLWVJpAJpQA6Q6nVz5LxOiN/6WZIAYfBrHUVXrJT2K7h8EKHgnnryLjZgO1FE6mdlVCxZRPaHRZSdNvIesK3HjLPLROhs28eSgAEPC+42ogPrB23s8TrY/HMtkZesWM7VYt0eTZYwO5hOQXAwW3dtIDM/To6tHHFibTOwOND4kWYAD3TeBEIn3eRdVqgYdWJuq8RYnkQ2sKnTxH6vkxPbo9kNZADrvjIQe6+FskBTfwUCwoftFNzX0Y/lkgmyVCuBVdcUHLfc2HM3kFmSQOEXFdROt1L/l4cFgab+koAzdhb+6UYFYnvM7JtuxvHd6zKCYUCE+H3YgtppocjrwHVZwXrBgmfQRr2ngBcCTf0lAV0eggdU7MDqfoWddxtp+dpAStgiovcms3npQmK9Gof7VKpu2mnqVTjWXc/e4wUkjmryvkcd7p++xwChezrVdenEFL9IxFQTH2uZZNdtIX/UwbdfllC6L4FtwIqsKOKmW+lo3cUm8XTXrZR7XaQMKFR01pAe2OEzgEmNrV6NXJCZFlnPLEgg/ZNiGsKel+UK/d1Czr0mzONu3D5ATOMrbLtq5fjnZSQP2LB1W4ieEyA0dZiMi05KgS1NO8gfcfBpe42EJpw1oY44OTXuZM+Ei4NaNkm+AAiPKPE0d9yU3tRY7+85C/DwLWK6zTQA0edqOXlDw9JhlDFd90GBTFXMJQtvTLo58n4u8UAksNwHWiYAE43E+nvOAEbdRE3p1PiSk3E0n8qf6mn8voZKkGaxVxSqxzSOjblo+KhI7lSYL/Pds+qJgActGNUsmful/VaUYY0f7+icOZlLIbD2FxOOEQdvD9k5NOrk3eadJISEsLEwUZ5LK4C4YRuGOQHn6wgZ12SjiV1lnMjDIHZtSuVV0cWiRJ48+aIjytOIv9vM2R9qKBGlGlBpFfOiTLdclM0J6FNZM+KQx254xwGOZMfI9t8BJPqgq0X9h+xkio4/lU/UoEJOm4FnRVnaq+W6uDEN05wveUBh1YRGtUjLdRtKr8o7wAYlg51DdlrbjKRM69Tdb5YH3Cy1GVg+7MDVZSa338YBTzHP+c//08kWIm43YY4MIf7lSHmYiY9K2H0dpd/O2j4L5Tf+/cwP+sZI2IUGmTb5339y5iX3qrx2upoKkZa0SOJvN7F/Up/5Ljy1ZvXBb/Vk96qo082YJxplXf+3wkEetzOXsUhmelHg+FNci/8G0n5Ox3WFRioAAAAASUVORK5CYII=";
    static readonly string s_categoryIconData = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAlRJREFUOE+NkN1L03EUhx+jbL0nYVpNTc3IhS/TcszplvNlLZlL5zRt1dK56ebe3NRuagRBGNRFN0E3gVAE3QS9ehHdLFDJ1JkZguaMHBX9D/H7laWLrAfOxfd8z+eBcwAkrEEI1oXb2BbfX8magnfdtIRtGEfsqOP/lvmr4KmbjVEfBiD3fD6KaBBr/IyAKHjvoWzEQdOQhS1AQsTFkVE7hsEGTpZnUmKV0/DIwqn4sIAomHTSOHQO1aKfvFkvbTd01CZvpuBWHfUZO5BrMtg/7cYYHxYQBXM+DC+slEb9OACpBNKHbRhnPJiSJKjumKj81EtRfFhAFIw4qL/fhDr2Y88kYPe8D+OsB+e4C/vbbgIRF8r4sIAomPFSF9IgjQbpKd7DoVftBEJVlJ0tIvtDD9e0OeyLuKj+2Id00kWecKdVgi/9yB+3kgXkhypQX9Sib8wnc8GP5WsfXaos0s0y0p+0YBrtQD/uZecqgcB0N5YWOcoFP1cHdBTd1KO6baR8KcAlzQGkwC4gZcrFsegFcc3fglgvFR4FKZe16EftXAkoUJwuRLbgp3cpSPCEjFQgWbjN6060fwjGHdQDGQ+aMd8zY5Gnknu9hpKabNLmvAw8bKUqbKMDSBuxU7lKsOhn00QnursNGLxKzImJyByF5NiOcjAWoHbKTfaYi72f+ykYrKcg3IFp1s32X4KXISQTTo63F1PdLCc30oVm3otuwYdqeegnCW+6KI0FyFzZFFcYc6IebKT62RmU3/o4vHLgX2wF1gv13ErasE28tvj+j9rwHb+epMD74tOoAAAAAElFTkSuQmCC";

    public static readonly SD.Bitmap PluginIcon = default;
    public static readonly SD.Bitmap PluginCategoryIcon = default;

    static AssemblyInfo()
    {
      if (!s_assemblyIconData.Contains("ASSEMBLY-ICON"))
      {
        using (var aicon = new MemoryStream(Convert.FromBase64String(s_assemblyIconData)))
          PluginIcon = new SD.Bitmap(aicon);
      }

      if (!s_categoryIconData.Contains("ASSEMBLY-CATEGORY-ICON"))
      {
        using (var cicon = new MemoryStream(Convert.FromBase64String(s_categoryIconData)))
          PluginCategoryIcon = new SD.Bitmap(cicon);
      }
    }

    public override Guid Id { get; } = new Guid("7120f040-503b-40c2-a2cb-3e62e407cde1");

    public override string AssemblyName { get; } = "UrbGEN.Components";
    public override string AssemblyVersion { get; } = "0.1.4.39946";
    public override string AssemblyDescription { get; } = @"URBGEN is a generative urban design tool for automatically generating building configurations under urban planning constraints such as Building Coverage Ratio (BCR), Floor Area Ratio (FAR), building height, orientation, and building typology.";
    public override string AuthorName { get; } = "Trong-Tin Tran, Ying Chieh Chan";
    public override string AuthorContact { get; } = "trongtintr@outlook.com";
    public override GH_LibraryLicense AssemblyLicense { get; } = GH_LibraryLicense.unset;
    public override SD.Bitmap AssemblyIcon { get; } = PluginIcon;
  }

  public class ProjectComponentPlugin : GH_AssemblyPriority
  {
    static readonly Guid s_projectId = new Guid("7120f040-503b-40c2-a2cb-3e62e407cde1");
    static readonly dynamic s_projectServer = default;
    static readonly object s_project = default;

    static ProjectComponentPlugin()
    {
      s_projectServer = ProjectInterop.GetProjectServer();
      if (s_projectServer is null)
      {
        RhinoApp.WriteLine($"Error loading Grasshopper plugin. Missing Rhino3D platform");
        return;
      }

      // get project
      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["projectAssembly"] = typeof(ProjectComponentPlugin).Assembly;
      dctx.Inputs["projectId"] = s_projectId;
      dctx.Inputs["projectData"] = GetProjectData();

      object project = default;
      if (s_projectServer.TryInvoke("plugins/v1/deserialize", dctx)
            && dctx.Outputs.TryGet("project", out project))
      {
        // server reports errors
        s_project = project;
      }
    }

    public override GH_LoadingInstruction PriorityLoad()
    {
      if (AssemblyInfo.PluginCategoryIcon is SD.Bitmap icon)
      {
        Grasshopper.Instances.ComponentServer.AddCategoryIcon("UrbGEN", icon);
      }
      Grasshopper.Instances.ComponentServer.AddCategorySymbolName("UrbGEN", "UrbGEN"[0]);

      return GH_LoadingInstruction.Proceed;
    }

    public static bool TryCreateScript(GH_Component ghcomponent, string serialized, out object script)
    {
      script = default;

      if (s_projectServer is null) return false;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["scriptData"] = serialized;

      if (s_projectServer.TryInvoke("plugins/v1/gh/deserialize", dctx))
      {
        return dctx.Outputs.TryGet("script", out script);
      }

      return false;
    }

    public static void DisposeScript(GH_Component ghcomponent, object script)
    {
      if (script is null)
        return;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["script"] = script;

      if (!s_projectServer.TryInvoke("plugins/v1/gh/dispose", dctx))
        throw new Exception("Error disposing Grasshopper script component");
    }

    static string GetProjectData()
    {
      var rm = new ResourceManager("Plugin.Data", Assembly.GetExecutingAssembly());
      return rm.GetString("PROJECT-DATA");
    }
  }
}
