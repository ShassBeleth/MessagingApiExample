using MessagingApiExample.Models.Request.ReplyMessage.Message;

namespace MessagingApiExample.Models.Request.MultiCastMessage {

	/// <summary>
	/// 複数人同時プッシュ通知Request
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