using MK.Client.Dialog;using Core.UI;

namespace MK.Client
{
	[FUIEvent(PanelId.DialogPanel, "Dialog", "DialogPanel")]
	public class DialogPanelHandler: UIFormBase
	{
		public FUI_DialogPanel FUIDialogPanel
		{
			get =>(FUI_DialogPanel)this._gameObject;
		}

		public override void RegisterUIEvent()
		{
		}

		public override void OnShow()
		{
		}
	}
}