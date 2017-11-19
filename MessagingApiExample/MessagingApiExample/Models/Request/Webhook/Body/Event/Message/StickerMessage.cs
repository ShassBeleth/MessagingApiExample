namespace MessagingApiExample.Models.Webhook.Body.Event.Message {

	/// <summary>
	/// スタンプメッセージ
	/// </summary>
	public class StickerMessage : MessageBase {

		/// <summary>
		/// メッセージID
		/// </summary>
		public string id;

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