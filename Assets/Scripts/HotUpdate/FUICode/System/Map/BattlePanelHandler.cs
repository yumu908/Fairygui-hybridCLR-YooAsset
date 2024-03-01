using Core.UI;
using MK.Client.Map;

namespace MK.Client
{
    [FUIEvent(PanelId.BattlePanel, "Map", "BattlePanel")]
	public class BattlePanelHandler: UIFormBase
	{
		public FUI_BattlePanel FUIBattlePanel
		{
			get =>(FUI_BattlePanel)this._gameObject;
		}

		public override void RegisterUIEvent()
		{
			FUIBattlePanel.AddBtn.AddListner(AddNum);
		}

		public override void OnShow()
		{
		}

		private void AddNum()
		{
			int i =int.Parse(FUIBattlePanel.NumText.text)+1;
			FUIBattlePanel.NumText.text = i.ToString();
			BattleTest.Instance.CreateCube();
		}
	}
}