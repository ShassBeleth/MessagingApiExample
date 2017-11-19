using MessagingApiExample.Models.Webhook.Body.Event.Message;

namespace MessagingApiExample.Models.Webhook.Body.Event {

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