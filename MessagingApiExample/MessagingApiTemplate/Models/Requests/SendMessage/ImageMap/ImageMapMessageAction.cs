namespace MessagingApiTemplate.Models.Requests.SendMessage.ImageMap {

	/// <summary>
	/// 指定するメッセージを送信するアクション
	/// </summary>
	public class ImageMapMessageAction : ImageMapActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "message";

		/// <summary>
		/// 送信するメッセージ
		/// </summary>
		public string text;

	}

}