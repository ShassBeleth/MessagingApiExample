using MessagingApiTemplate.Models.Requests.SendMessage.ImageMap;

namespace MessagingApiTemplate.Models.Requests.SendMessage {

	/// <summary>
	/// イメージマップメッセージ
	/// </summary>
	public class ImageMapMessage : MessageBase {

		/// <summary>
		/// メッセージ種別
		/// </summary>
		public string type = "imagemap";

		/// <summary>
		/// 画像のベースURL
		/// </summary>
		public string baseUrl;

		/// <summary>
		/// 代替テキスト
		/// </summary>
		public string altText;

		/// <summary>
		/// 画像サイズ
		/// </summary>
		public BaseSize baseSize;

		/// <summary>
		/// イメージマップアクションオブジェクト
		/// </summary>
		public ImageMapActionBase[] actions;

	}

}