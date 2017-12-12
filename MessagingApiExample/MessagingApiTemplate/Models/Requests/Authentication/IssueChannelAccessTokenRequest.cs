namespace MessagingApiTemplate.Models.Requests.Authentication {

	/// <summary>
	/// チャンネルアクセストークン発行時Request
	/// </summary>
	internal class IssueChannelAccessTokenRequest {

		/// <summary>
		/// グラント種別
		/// </summary>
		internal string grant_type;

		/// <summary>
		/// チャンネルID
		/// </summary>
		internal string client_id;

		/// <summary>
		/// シークレットID
		/// </summary>
		internal string client_secret;

	}

}