namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Message {

	/// <summary>
	/// 位置情報メッセージ
	/// </summary>
	public class LocationMessage : MessageBase {

		/// <summary>
		/// タイトル
		/// </summary>
		public string title;

		/// <summary>
		/// 住所
		/// </summary>
		public string address;

		/// <summary>
		/// 緯度
		/// </summary>
		public decimal latitude;

		/// <summary>
		/// 経度
		/// </summary>
		public decimal longitude;

	}

}