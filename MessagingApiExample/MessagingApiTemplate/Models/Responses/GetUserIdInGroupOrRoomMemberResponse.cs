namespace MessagingApiTemplate.Models.Responses {

	/// <summary>
	/// グループまたはトークルームからユーザID取得Response
	/// </summary>
	public class GetUserIdInGroupOrRoomMemberResponse {

		/// <summary>
		/// グループメンバーのユーザID
		/// </summary>
		public string[] memberIds;

		/// <summary>
		/// memberIdsに続きのユーザがある場合に返される
		/// </summary>
		public string next;

	}

}