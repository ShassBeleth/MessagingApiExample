namespace MessagingApiExample.Models.Webhook.Event.Message {

	/// <summary>
	/// ファイルメッセージ
	/// </summary>
	public class FileMessage : MessageBase {

		/// <summary>
		/// メッセージID
		/// </summary>
		public string id;
		
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