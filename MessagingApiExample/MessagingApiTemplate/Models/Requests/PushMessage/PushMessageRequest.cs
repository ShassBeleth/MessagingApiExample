using MessagingApiTemplate.Models.Requests.SendMessage;

namespace MessagingApiTemplate.Models.Requests.PushMessage {

	/// <summary>
	/// プッシュRequest
	/// </summary>
	public class PushMessageRequest {

		/// <summary>
		/// 送信先ID
		/// </summary>
		public string to;

		/// <summary>
		/// メッセージ
		/// </summary>
		public MessageBase[] messages;

	}

}