namespace MessagingApiExample.Models.Response.Group {

	/// <summary>
	/// グループメンバーのId取得Response
	/// </summary>
	public class UserIdInGroupMemberResponse {

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