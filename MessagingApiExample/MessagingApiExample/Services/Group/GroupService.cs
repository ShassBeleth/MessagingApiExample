using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Response.Group;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.Group {

	/// <summary>
	/// グループに関するService
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
		public static async Task<UserProfileInGroupMemberResponse> GetUserProfileInGroupMember( 
			string channelAccessToken ,
			string groupId , 
			string userId 
		) {

			Trace.TraceInformation( "Start Get User Profile In Group Member" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl = 
				ConfigurationManager.AppSettings[ "BaseUrl" ] + 
				ConfigurationManager.AppSettings[ "GroupUrl" ] + 
				groupId + 
				ConfigurationManager.AppSettings[ "GroupProfileUrl" ] + 
				userId;

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Profile In Group Member Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<UserProfileInGroupMemberResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get User Profile In Group Member is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get User Profile In Group Member is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get User Profile In Group Member is Unexpected Exception" );
				client.Dispose();
				return null;
			}

		}

		// TODO 未確認
		/// <summary>
		/// グループメンバーのIdを取得する
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="groupId">グループID</param>
		/// <param name="next">ユーザIDに続きがある場合に必要なキー</param>
		/// <returns></returns>
		public static async Task<UserIdInGroupMemberResponse> GetUserIdInGroupMember(
			string channelAccessToken ,
			string groupId ,
			string next = null
		) {

			Trace.TraceInformation( "Start Get User Id In Group Member" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GroupUrl" ] +
				groupId +
				ConfigurationManager.AppSettings[ "/members/ids" ] +
				( next == null ? "" : ( "?start=" + next ) );
			
			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get User Id In Group Member Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<UserIdInGroupMemberResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get User Id In Group Member is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get User Id In Group Member is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get User Id In Group Member is Unexpected Exception" );
				client.Dispose();
				return null;
			}

		}


	}
	
}