namespace MessagingApiTemplate.Models.Responses.Profile {

	/// <summary>
	/// プロフィール取得Response
	/// </summary>
	public class GetProfileResponse {

		/// <summary>
		/// 表示名
		/// </summary>
		public string displayName;

		/// <summary>
		/// ユーザID
		/// </summary>
		public string userId;

		/// <summary>
		/// 画像のURL
		/// </summary>
		public string pictureUrl;

		/// <summary>
		/// ステータスメッセージ
		/// </summary>
		public string statusMessage;

	}

}