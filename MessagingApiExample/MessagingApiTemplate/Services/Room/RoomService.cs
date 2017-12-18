using MessagingApiTemplate.Models.Responses;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Room {
	
	/// <summary>
	/// トークルームについてのService
	/// </summary>
	public static class RoomService {

		// TODO 未確認
		/// <summary>
		/// トークルームメンバーのプロフィールを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		public static async Task<GetUserProfileInGroupOrRoomMemberResponse> GetUserProfileInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string userId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Room Id is Null" );
				return null;
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomProfileUrl" ] +
				userId;
			GetUserProfileInGroupOrRoomMemberResponse response = await MessagingApiSender.SendMessagingApi<string , GetUserProfileInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;
			
		}
		
		// TODO 未確認
		/// <summary>
		/// トークルームメンバーのIdを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		/// <param name="next">ユーザIDに続きがある場合に必要なキー</param>
		/// <returns></returns>
		public static async Task<GetUserIdInGroupOrRoomMemberResponse> GetUserIdInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string next = null
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Group Id is Null" );
				return null;
			}
			
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomIdUrl" ] +
				( next == null ? "" : ( "?start=" + next ) );

			GetUserIdInGroupOrRoomMemberResponse response = await MessagingApiSender.SendMessagingApi<string , GetUserIdInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;

		}

		// TODO 未確認
		/// <summary>
		/// トークルームから退出する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		public static async Task LeaveRoom(
			string channelAccessToken ,
			string roomId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Group Id is Null" );
			}
			
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomLeaveUrl" ];
			await MessagingApiSender.SendMessagingApi<string , string>(
				channelAccessToken ,
				requestUrl ,
				null ,
				false
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );
			
		}

	}

}