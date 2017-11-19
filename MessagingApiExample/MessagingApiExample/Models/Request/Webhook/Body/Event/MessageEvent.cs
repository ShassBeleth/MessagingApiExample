using MessagingApiExample.Models.Webhook.Event.Message;

namespace MessagingApiExample.Models.Webhook.Event {

	/// <summary>
	/// メッセージイベント
	/// </summary>
	public class MessageEvent : EventBase {
		
		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

		/// <summary>
		/// メッセージ
		/// </summary>
		public MessageBase message;

	}

}