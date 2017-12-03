namespace MessagingApiExample.Models.Request.Authentication {

	/// <summary>
	/// チャンネルアクセストークンリクエスト
	/// </summary>
	public class ChannelAccessTokenRequest {

		/// <summary>
		/// グラント種別
		/// </summary>
		public string grant_type;

		/// <summary>
		/// チャンネルID
		/// </summary>
		public string client_id;

		/// <summary>
		/// シークレットID
		/// </summary>
		public string client_secret;

	}

}