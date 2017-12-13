namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.Action {

	/// <summary>
	/// ポストバックアクション
	/// </summary>
	public class TemplatePostbackAction : TemplateActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "postback";

		/// <summary>
		/// ラベル
		/// </summary>
		public string label;

		/// <summary>
		/// データ
		/// </summary>
		public string data;

		/// <summary>
		/// アクション実行時に送信されるテキスト
		/// </summary>
		public string text;

	}

}