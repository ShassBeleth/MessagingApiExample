namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Source {

	/// <summary>
	/// 送信元がトークルーム
	/// </summary>
	public class RoomSource : SourceBase {

		/// <summary>
		/// ユーザID
		/// </summary>
		public string userId;

		/// <summary>
		/// ルームID
		/// </summary>
		public string roomId;

	}

}