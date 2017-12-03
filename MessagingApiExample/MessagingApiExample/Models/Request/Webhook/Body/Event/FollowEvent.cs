namespace MessagingApiExample.Models.Request.Webhook.Body.Event {

	/// <summary>
	/// 友達追加またはブロック解除時イベント
	/// </summary>
	public class FollowEvent : EventBase {
		
		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

	}

}