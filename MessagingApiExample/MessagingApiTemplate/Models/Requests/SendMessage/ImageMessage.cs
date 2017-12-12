namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 画像メッセージ
	/// </summary>
	internal class ImageMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		internal string type = "image";

		/// <summary>
		/// 画像URL
		/// </summary>
		internal string originalContentUrl;

		/// <summary>
		/// プレビュー画像URL
		/// </summary>
		internal string previewImageUrl;

	}

}