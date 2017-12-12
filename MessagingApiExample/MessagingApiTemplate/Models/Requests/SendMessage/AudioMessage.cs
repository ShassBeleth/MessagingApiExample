namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 音声メッセージ
	/// </summary>
	public class AudioMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "audio";

		/// <summary>
		/// 音声ファイルのURL
		/// </summary>
		public string originalContentUrl;

		/// <summary>
		/// 音声ファイルの長さ
		/// </summary>
		public int duration;

	}

}