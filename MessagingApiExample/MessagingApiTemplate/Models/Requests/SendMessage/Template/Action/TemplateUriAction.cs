namespace MessagingApiTemplate.Models.Requests.SendMessage.Template.Action {

	/// <summary>
	/// URIアクション
	/// </summary>
	public class TemplateUriAction : TemplateActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "uri";

		/// <summary>
		/// ラベル
		/// </summary>
		public string label;

		/// <summary>
		/// アクション実行時に開かれるURI
		/// </summary>
		public string uri;
		
	}

}