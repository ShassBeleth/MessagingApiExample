namespace MessagingApiExample.Models.Response.Group {

	/// <summary>
	/// グループメンバーのプロフィール取得Response
	/// </summary>
	public class UserProfileInGroupMemberResponse {

		/// <summary>
		/// 表示名
		/// </summary>
		public string displayName;

		/// <summary>
		/// ユーザID
		/// </summary>
		public string userId;

		/// <summary>
		/// プロフィール画像のURL
		/// </summary>
		public string pictureUrl;

	}

}