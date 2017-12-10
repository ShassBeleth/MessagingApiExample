using MessagingApiExample.Models.Request.ReplyMessage.Message;

namespace MessagingApiExample.Models.Request.PushMessage {

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
