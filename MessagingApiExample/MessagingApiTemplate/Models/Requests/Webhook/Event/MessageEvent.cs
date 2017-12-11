using MessagingApiTemplate.Models.Requests.Webhook.Event.Message;

namespace MessagingApiTemplate.Models.Requests.Webhook.Event {

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