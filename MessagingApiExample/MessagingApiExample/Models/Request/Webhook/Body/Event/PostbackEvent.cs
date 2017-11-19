using MessagingApiExample.Models.Webhook.Event.Postback;

namespace MessagingApiExample.Models.Webhook.Event {

	/// <summary>
	/// ポストバック時イベント
	/// </summary>
	public class PostbackEvent : EventBase {
		
		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

		/// <summary>
		/// ポストバック
		/// </summary>
		public PostbackData postback;

	}

}