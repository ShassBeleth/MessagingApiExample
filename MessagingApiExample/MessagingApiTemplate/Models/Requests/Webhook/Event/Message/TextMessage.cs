namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Message {

	/// <summary>
	/// テキストメッセージ
	/// </summary>
	public class TextMessage : MessageBase {
		
		/// <summary>
		/// メッセージ本文
		/// </summary>
		public string text;

	}

}