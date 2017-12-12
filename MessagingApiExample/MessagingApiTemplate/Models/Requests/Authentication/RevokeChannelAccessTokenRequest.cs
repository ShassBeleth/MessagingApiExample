namespace MessagingApiTemplate.Models.Requests.Authentication {

	/// <summary>
	/// チャンネルアクセストークンの取り消しRequest
	/// </summary>
	public class RevokeChannelAccessTokenRequest {

		/// <summary>
		/// チャンネルアクセストークン
		/// </summary>
		public string access_token;

	}

}