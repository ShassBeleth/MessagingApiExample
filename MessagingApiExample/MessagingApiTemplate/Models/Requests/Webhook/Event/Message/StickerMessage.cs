namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Message {

	/// <summary>
	/// スタンプメッセージ
	/// </summary>
	public class StickerMessage : MessageBase {
		
		/// <summary>
		/// パッケージID
		/// </summary>
		public string packageId;

		/// <summary>
		/// StickerID
		/// </summary>
		public string stickerId;

	}

}