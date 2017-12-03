using MessagingApiExample.Models.Request.Webhook.Body.Event.Postback;

namespace MessagingApiExample.Models.Request.Webhook.Body.Event {

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