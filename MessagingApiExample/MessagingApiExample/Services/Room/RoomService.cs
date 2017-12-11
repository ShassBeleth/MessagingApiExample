using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Response.Group;
using Newtonsoft.Json;
using MessagingApiExample.Models.Response.Room;

namespace MessagingApiExample.Services.Room {

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
		public static async Task<UserProfileInRoomMemberResponse> GetUserProfileInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string userId
		) {

			Trace.TraceInformation( "Start Get User Profile In Room Member" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomProfileUrl" ] +
				userId;

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Profile In Room Member Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<UserProfileInRoomMemberResponse>( resultAsString );

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
		public static async Task<UserIdInRoomMemberRespoonse> GetUserIdInRoomMember(
			string channelAccessToken ,
			string roomId ,
			string next = null
		) {

			Trace.TraceInformation( "Start Get User Id In Room Member" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomIdUrl" ] +
				( next == null ? "" : ( "?start=" + next ) );

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Id In Room Member Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<UserIdInRoomMemberRespoonse>( resultAsString );

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
		public static async Task LeaveGroup(
			string channelAccessToken ,
			string roomId
		) {

			Trace.TraceInformation( "Start Leave Room" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RoomUrl" ] +
				roomId +
				ConfigurationManager.AppSettings[ "RoomLeaveUrl" ];

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Leave Room Response is : OK" );

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