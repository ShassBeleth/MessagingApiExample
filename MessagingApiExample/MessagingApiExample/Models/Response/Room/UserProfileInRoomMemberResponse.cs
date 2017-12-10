namespace MessagingApiExample.Models.Response.Room {

	/// <summary>
	/// トークルームメンバーのプロフィール取得Response
	/// </summary>
	public class UserProfileInRoomMemberResponse {

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