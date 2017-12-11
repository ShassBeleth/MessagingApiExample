using MessagingApiTemplate.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomProfileUrl" ] +
				userId;
			
			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Profile In Room Member Response is " + resultAsString );
				return JsonConvert.DeserializeObject<GetUserProfileInGroupOrRoomMemberResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get User Profile In Room Member is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get User Profile In Room Member is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get User Profile In Room Member is Unexpected Exception" );
				client.Dispose();
				return null;
			}

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

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomIdUrl" ] +
				( next == null ? "" : ( "?start=" + next ) );

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Id In Room Member Response is " + resultAsString );
				return JsonConvert.DeserializeObject<GetUserIdInGroupOrRoomMemberResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get User Id In Room Member is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get User Id In Room Member is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get User Id In Room Member is Unexpected Exception" );
				client.Dispose();
				return null;
			}

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

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomLeaveUrl" ];
			
			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				string resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Leave Room Response is OK" );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Leave Room is Argument Null Exception" );
				client.Dispose();
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Leave Room is Http Request Exception" );
				client.Dispose();
			}
			catch( Exception ) {
				Trace.TraceError( "Leave Room is Unexpected Exception" );
				client.Dispose();
			}

		}

	}

}