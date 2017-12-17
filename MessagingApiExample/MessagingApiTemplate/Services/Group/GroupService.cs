using MessagingApiTemplate.Models.Responses;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Group {

	/// <summary>
	/// グループについてのService
	/// </summary>
	public static class GroupService {
		
		/// <summary>
		/// グループメンバーのプロフィールを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		public static async Task<GetUserProfileInGroupOrRoomMemberResponse> GetUserProfileInGroupMember(
			string channelAccessToken ,
			string groupId ,
			string userId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( groupId == null ) {
				Trace.TraceWarning( "Group Id is Null" );
				return null;
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "GroupProfileUrl" ] +
				userId;
			GetUserProfileInGroupOrRoomMemberResponse response = await MessagingApiSender.SendMessagingApi<string , GetUserProfileInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;
			
		}

		// TODO 認証済みLINE@アカウントまたは公式アカウントでないと確認できない
		/// <summary>
		/// グループメンバーのIdを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		/// <param name="next">ユーザIDに続きがある場合に必要なキー</param>
		public static async Task<GetUserIdInGroupOrRoomMemberResponse> GetUserIdInGroupMember(
			string channelAccessToken ,
			string groupId ,
			string next = null
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( groupId == null ) {
				Trace.TraceWarning( "Group Id Of Get User Id In Group Member is Null" );
				return null;
			}
			
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "GroupIdUrl" ] +
				( next == null ? "" : ( "?start=" + next ) );
			GetUserIdInGroupOrRoomMemberResponse response = await MessagingApiSender.SendMessagingApi<string , GetUserIdInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;

		}
		
		/// <summary>
		/// グループから退出する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		public static async Task LeaveGroup(
			string channelAccessToken ,
			string groupId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( groupId == null ) {
				Trace.TraceWarning( "Group Id is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "GroupLeaveUrl" ];
			await MessagingApiSender.SendMessagingApi<string,string>(
				channelAccessToken ,
				requestUrl ,
				null ,
				false
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

		}

	}

}