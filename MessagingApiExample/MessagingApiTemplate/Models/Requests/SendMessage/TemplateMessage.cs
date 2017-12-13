using MessagingApiTemplate.Models.Requests.SendMessage.Template;

namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// テンプレートメッセージ
	/// </summary>
	public class TemplateMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "type";

		/// <summary>
		/// 代替テキスト
		/// </summary>
		public string altText;

		/// <summary>
		/// テンプレート
		/// </summary>
		public TemplateBase template;
		
	}
	
}