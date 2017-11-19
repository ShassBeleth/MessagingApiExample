namespace MessagingApiExample.Models.Webhook.Event {

	/// <summary>
	/// トークルームまたはグループ参加時イベント
	/// </summary>
	public class JoinEvent : EventBase {
		
		/// <summary>
		/// リプライトークン
		/// </summary>
		public string replyToken;

	}

}