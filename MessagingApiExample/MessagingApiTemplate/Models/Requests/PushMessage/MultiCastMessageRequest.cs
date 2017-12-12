using MessagingApiTemplate.Models.Requests.SendMessage;

namespace MessagingApiTemplate.Models.Requests.PushMessage {

	/// <summary>
	/// 複数人同時プッシュ通知Requeset
	/// </summary>
	public class MultiCastMessageRequest {

		/// <summary>
		/// 送信先ID
		/// </summary>
		public string[] to;

		/// <summary>
		/// メッセージ
		/// </summary>
		public MessageBase[] messages;

	}

}