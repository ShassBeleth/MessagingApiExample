namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// 音声メッセージ
	/// </summary>
	internal class AudioMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		internal string type = "audio";

		/// <summary>
		/// 音声ファイルのURL
		/// </summary>
		internal string originalContentUrl;

		/// <summary>
		/// 音声ファイルの長さ
		/// </summary>
		internal int duration;

	}

}