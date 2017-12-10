namespace MessagingApiExample.Models.Response.Room {

	/// <summary>
	/// トークルームメンバーのId取得Response
	/// </summary>
	public class UserIdInRoomMemberRespoonse {

		/// <summary>
		/// トークルームメンバーのユーザID
		/// </summary>
		public string[] memberIds;

		/// <summary>
		/// memberIdsに続きのユーザがある場合に返される
		/// </summary>
		public string next;

	}

}