namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 動画メッセージ
	/// </summary>
	internal class VideoMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		internal string type = "video";

		/// <summary>
		/// 動画ファイルURL
		/// </summary>
		internal string originalContentUrl;

		/// <summary>
		/// プレビュー画像URL
		/// </summary>
		internal string previewImageUrl;

	}

}