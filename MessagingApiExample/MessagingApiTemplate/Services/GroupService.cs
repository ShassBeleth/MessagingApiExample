using MessagingApiTemplate.Models.Responses;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// グループについてのService
	/// </summary>
	public class GroupService {

		// TODO 未確認
		/// <summary>
		/// グループメンバーのプロフィールを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		public async Task<GetUserProfileInGroupOrRoomMemberResponse> GetUserProfileInGroupMember(
			string channelAccessToken ,
			string groupId ,
			string userId
		) {

			Trace.TraceInformation( "Start Get User Profile In Group Member" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Get User Profile In Group Member is Null" );
				return null;
			}
			if( groupId == null ) {
				Trace.TraceWarning( "Group Id Of Get User Profile In Group Member is Null" );
				return null;
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id Of Get User Profile In Group Member is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "GroupProfileUrl" ] +
				userId;
			return await MessagingApiSender.SendMessagingApi<string,GetUserProfileInGroupOrRoomMemberResponse> (
				channelAccessToken ,
				requestUrl
			);
			
		}

		// TODO 未確認
		/// <summary>
		/// グループメンバーのIdを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		/// <param name="next">ユーザIDに続きがある場合に必要なキー</param>
		/// <returns></returns>
		public async Task<GetUserIdInGroupOrRoomMemberResponse> GetUserIdInGroupMember(
			string channelAccessToken ,
			string groupId ,
			string next = null
		) {

			Trace.TraceInformation( "Start Get User Id In Group Member" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Get User Id In Group Member is Null" );
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
			return await MessagingApiSender.SendMessagingApi<string , GetUserIdInGroupOrRoomMemberResponse>(
				channelAccessToken ,
				requestUrl
			);

		}

		// TODO 未確認
		/// <summary>
		/// グループから退出する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		public async Task LeaveGroup(
			string channelAccessToken ,
			string groupId
		) {

			Trace.TraceInformation( "Start Leave Group" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Leave Group Member is Null" );
			}
			if( groupId == null ) {
				Trace.TraceWarning( "Group Id Of Leave Group Member is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "GroupLeaveUrl" ];
			await MessagingApiSender.SendMessagingApi<string,string>(
				channelAccessToken ,
				requestUrl
			);

		}

	}

}