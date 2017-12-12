using MessagingApiTemplate.Models.Requests.SendMessage;

namespace MessagingApiTemplate.Models.Requests.ReplyMessage {

	/// <summary>
	/// リプライメッセージリクエスト
	/// </summary>
	public class ReplyMessageRequest {

		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

		/// <summary>
		/// メッセージ
		/// </summary>
		public MessageBase[] messages;

	}

}