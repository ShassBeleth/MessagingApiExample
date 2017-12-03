using MessagingApiExample.Models.Request.Webhook.Body.Event.Message;

namespace MessagingApiExample.Models.Request.Webhook.Body.Event {

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