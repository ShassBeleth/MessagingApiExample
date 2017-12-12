namespace MessagingApiTemplate.Models.Requests.Webhook.Event.Message {

	/// <summary>
	/// ファイルメッセージ
	/// </summary>
	public class FileMessage : MessageBase {
		
		/// <summary>
		/// ファイル名
		/// </summary>
		public string fileName;

		/// <summary>
		/// ファイルのバイト数
		/// </summary>
		public string fileSize;

	}

}