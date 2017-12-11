using MessagingApiTemplate.Models.Requests.Webhook.Event.Postback;

namespace MessagingApiTemplate.Models.Requests.Webhook.Event {
	
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