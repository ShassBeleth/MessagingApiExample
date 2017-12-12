namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 位置情報メッセージ
	/// </summary>
	internal class LocationMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		internal string type = "location";

		/// <summary>
		/// タイトル
		/// </summary>
		internal string title;

		/// <summary>
		/// 住所
		/// </summary>
		internal string address;

		/// <summary>
		/// 緯度
		/// </summary>
		internal decimal latitude;

		/// <summary>
		/// 経度
		/// </summary>
		internal decimal longitude;

	}

}