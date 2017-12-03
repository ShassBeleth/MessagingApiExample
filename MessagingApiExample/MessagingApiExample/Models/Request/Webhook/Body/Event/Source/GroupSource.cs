namespace MessagingApiExample.Models.Request.Webhook.Body.Event.Source {

	/// <summary>
	/// 送信元がグループ
	/// </summary>
	public class GroupSource : SourceBase{

		/// <summary>
		/// ユーザID
		/// </summary>
		public string userId;

		/// <summary>
		/// グループID
		/// </summary>
		public string groupId;
		
	}

}