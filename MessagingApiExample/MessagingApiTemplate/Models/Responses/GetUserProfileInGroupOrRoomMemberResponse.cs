namespace MessagingApiTemplate.Models.Responses {

	/// <summary>
	/// グループまたはトークルームからユーザのプロフィール取得Response
	/// </summary>
	public class GetUserProfileInGroupOrRoomMemberResponse {

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