/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MK.Client.Loading
{
	public partial class FUI_LoadingPanel: GComponent
	{
		public MK.Client.Loading.FUI_ProgressBar progress;
		public const string URL = "ui://us0guk45jx390";
		public static FUI_LoadingPanel CreateInstance()
		{
			return (FUI_LoadingPanel)UIPackage.CreateObject("Loading", "LoadingPanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			progress = (MK.Client.Loading.FUI_ProgressBar)GetChildAt(1);
		}
	}
}
