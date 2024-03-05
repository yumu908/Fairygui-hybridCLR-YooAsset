/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MK.Client.Dialog
{
	public partial class FUI_DialogPanel: GComponent
	{
		public GTextField message;
		public MK.Client.Common.FUI_Button1 confirm;
		public MK.Client.Common.FUI_Button1 cancel;
		public const string URL = "ui://u3v734rejx390";
		public static FUI_DialogPanel CreateInstance()
		{
			return (FUI_DialogPanel)UIPackage.CreateObject("Dialog", "DialogPanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			message = (GTextField)GetChildAt(1);
			confirm = (MK.Client.Common.FUI_Button1)GetChildAt(2);
			cancel = (MK.Client.Common.FUI_Button1)GetChildAt(3);
		}
	}
}
