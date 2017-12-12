namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 位置情報メッセージ
	/// </summary>
	public class LocationMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "location";

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