namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// テキストメッセージ
	/// </summary>
	internal class TextMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		internal string type = "text";

		/// <summary>
		/// テキスト本文
		/// </summary>
		internal string text;

	}

}