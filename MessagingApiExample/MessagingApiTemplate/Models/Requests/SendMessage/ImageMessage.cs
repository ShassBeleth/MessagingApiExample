namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 画像メッセージ
	/// </summary>
	public class ImageMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "image";

		/// <summary>
		/// 画像URL
		/// </summary>
		public string originalContentUrl;

		/// <summary>
		/// プレビュー画像URL
		/// </summary>
		public string previewImageUrl;

	}

}