using MessagingApiTemplate.Models.Requests.SendMessage.Template.Action;

namespace MessagingApiTemplate.Models.Requests.SendMessage.Template {

	/// <summary>
	/// 確認テンプレート
	/// </summary>
	public class ConfirmTemplate : TemplateBase {

		/// <summary>
		/// テンプレート種別
		/// </summary>
		public string type = "confirm";

		/// <summary>
		/// メッセージテキスト
		/// </summary>
		public string text;

		/// <summary>
		/// タップ時のアクション
		/// </summary>
		public TemplateActionBase[] actions;

	}

}