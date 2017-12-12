namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// スタンプメッセージ
	/// </summary>
	public class StickerMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "sticker";

		/// <summary>
		/// パッケージID
		/// </summary>
		public string packageId;

		/// <summary>
		/// スタンプID
		/// </summary>
		public string stickerId;

	}

}