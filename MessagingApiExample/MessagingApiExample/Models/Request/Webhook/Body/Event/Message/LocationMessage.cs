namespace MessagingApiExample.Models.Request.Webhook.Body.Event.Message {

	/// <summary>
	/// 位置情報メッセージ
	/// </summary>
	public class LocationMessage : MessageBase {

		/// <summary>
		/// メッセージID
		/// </summary>
		public string id;
		
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
		public string latitude;

		/// <summary>
		/// 経度
		/// </summary>
		public string longitude;
		
	}

}