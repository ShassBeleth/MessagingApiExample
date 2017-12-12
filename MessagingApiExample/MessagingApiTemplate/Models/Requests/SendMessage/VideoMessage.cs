namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 動画メッセージ
	/// </summary>
	public class VideoMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "video";

		/// <summary>
		/// 動画ファイルURL
		/// </summary>
		public string originalContentUrl;

		/// <summary>
		/// プレビュー画像URL
		/// </summary>
		public string previewImageUrl;

	}

}