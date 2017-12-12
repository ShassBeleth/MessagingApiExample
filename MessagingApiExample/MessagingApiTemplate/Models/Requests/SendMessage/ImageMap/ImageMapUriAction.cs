namespace MessagingApiTemplate.Models.Requests.SendMessage.ImageMap {

	/// <summary>
	/// タップ時にURIにユーザがリダイレクトされるアクション
	/// </summary>
	public class ImageMapUriAction : ImageMapActionBase {

		/// <summary>
		/// アクション種別
		/// </summary>
		public string type = "uri";

		/// <summary>
		/// リダイレクト先URL
		/// </summary>
		public string linkUri;
		
	}

}