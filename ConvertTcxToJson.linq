<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Entity.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Client.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Extensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

void Main()
{
	// convert a TCX to a JSON file
	var xml = XDocument.Load(@"C:\Users\frankp\Desktop\fietsfiles\frank_lansdowne.tcx");
	//var tcx = new TCXTrack(xml.Descendants(TCXBase.TCX_NS+"Track").First());
	//tcx.Dump();
	//new JavaScriptSerializer().Serialize(tcx).Dump();
	var list = JsonConvert.SerializeObject(
		RemoveAllNamespaces(xml.Root),
		Newtonsoft.Json.Formatting.None,
		new JsonSerializerSettings() {
			ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
		}
	);
list.Dump();
//Content(list, "application/json").Dump();
}

// Define other methods and classes here

public static XElement RemoveAllNamespaces(XElement e)
{
	return new XElement(e.Name.LocalName,
		(from n in e.Nodes()
			select ((n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
				(e.HasAttributes) ? 
					(from a in e.Attributes() 
						where (!a.IsNamespaceDeclaration)  
						select new XAttribute(a.Name.LocalName, a.Value)) : null);
}