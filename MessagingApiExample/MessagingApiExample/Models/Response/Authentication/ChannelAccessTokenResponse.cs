namespace MessagingApiExample.Models.Response.Authentication {

	/// <summary>
	/// チャンネルアクセストークン発行時Response
	/// </summary>
	public class ChannelAccessTokenResponse {

		/// <summary>
		/// チャンネルアクセストークン
		/// </summary>
		public string access_token;

		/// <summary>
		/// 有効期限が切れるまでの秒数
		/// </summary>
		public long expires_in;

		/// <summary>
		/// トークン種別 Bearer固定
		/// </summary>
		public string token_type;

	}

}