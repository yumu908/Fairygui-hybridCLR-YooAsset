using MK.Client.Loading;using Core.UI;

namespace MK.Client
{
	[FUIEvent(PanelId.LoadingPanel, "Loading", "LoadingPanel")]
	public class LoadingPanelHandler: UIFormBase
	{
		public FUI_LoadingPanel FUILoadingPanel
		{
			get =>(FUI_LoadingPanel)this._gameObject;
		}

		public override void RegisterUIEvent()
		{
		}

		public override void OnShow()
		{
		}
	}
}