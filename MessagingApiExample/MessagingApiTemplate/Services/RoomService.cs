using MessagingApiTemplate.Models.Responses;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {
	
	/// <summary>
	/// トークルームについてのService
	/// </summary>
	public class RoomService {

		// TODO 未確認
		/// <summary>
		/// トークルームメンバーのプロフィールを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		public async Task<GetUserProfileInGroupOrRoomMemberResponse> GetUserProfileInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string userId
		) {

			Trace.TraceInformation( "Start Get User Profile In Room Member" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Get User Profile In Room Member is Null" );
				return null;
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Room Id Of Get User Profile In Room Member is Null" );
				return null;
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id Of Get User Profile In Room Member is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomProfileUrl" ] +
				userId;
			return await MessagingApiSender.SendMessagingApi<string , GetUserProfileInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			);
			
		}
		
		// TODO 未確認
		/// <summary>
		/// トークルームメンバーのIdを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		/// <param name="next">ユーザIDに続きがある場合に必要なキー</param>
		/// <returns></returns>
		public async Task<GetUserIdInGroupOrRoomMemberResponse> GetUserIdInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string next = null
		) {

			Trace.TraceInformation( "Start Get User Id In Room Member" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Get User Id In Room Member is Null" );
				return null;
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Group Id Of Get User Id In Room Member is Null" );
				return null;
			}
			
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomIdUrl" ] +
				( next == null ? "" : ( "?start=" + next ) );
			return await MessagingApiSender.SendMessagingApi<string , GetUserIdInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			);
			
		}

		// TODO 未確認
		/// <summary>
		/// トークルームから退出する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="roomId">トークルームID</param>
		public async Task LeaveGroup(
			string channelAccessToken ,
			string roomId
		) {

			Trace.TraceInformation( "Start Leave Room" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Leave Room Member is Null" );
			}
			if( roomId == null ) {
				Trace.TraceWarning( "Group Id Of Leave Room Member is Null" );
			}
			
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomLeaveUrl" ];
			await MessagingApiSender.SendMessagingApi<string , string>(
				channelAccessToken ,
				requestUrl
			);
			
		}

	}

}