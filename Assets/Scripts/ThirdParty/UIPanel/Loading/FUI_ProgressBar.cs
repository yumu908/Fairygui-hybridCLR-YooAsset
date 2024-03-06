/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MK.Client.Loading
{
	public partial class FUI_ProgressBar: GProgressBar
	{
		public Controller c1;
		public GTextField title;
		public const string URL = "ui://us0guk45jx396";
		public static FUI_ProgressBar CreateInstance()
		{
			return (FUI_ProgressBar)UIPackage.CreateObject("Loading", "ProgressBar");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			c1 = GetControllerAt(0);
			title = (GTextField)GetChildAt(5);
		}
	}
}
