namespace MessagingApiTemplate.Models.Requests.Authentication {

	/// <summary>
	/// チャンネルアクセストークン発行時Request
	/// </summary>
	public class IssueChannelAccessTokenRequest {

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