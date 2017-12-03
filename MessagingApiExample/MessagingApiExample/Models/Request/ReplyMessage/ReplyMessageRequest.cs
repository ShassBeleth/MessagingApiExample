using MessagingApiExample.Models.Request.ReplyMessage.Message;

namespace MessagingApiExample.Models.Request.ReplyMessage {

	/// <summary>
	/// リプライメッセージリクエスト
	/// </summary>
	public class ReplyMessageRequest {

		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

		public MessageBase[] messages;

	}

}