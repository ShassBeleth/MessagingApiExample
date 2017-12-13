namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.Action {

	/// <summary>
	/// メッセージアクション
	/// </summary>
	public class TemplateMessageAction : TemplateActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "message";

		/// <summary>
		/// ラベル
		/// </summary>
		public string label;

		/// <summary>
		/// アクション実行時に送信されるテキスト
		/// </summary>
		public string text;

	}
}