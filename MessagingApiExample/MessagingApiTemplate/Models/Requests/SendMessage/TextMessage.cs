namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// テキストメッセージ
	/// </summary>
	public class TextMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "text";

		/// <summary>
		/// テキスト本文
		/// </summary>
		public string text;

	}

}