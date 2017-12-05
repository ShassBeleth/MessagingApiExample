using System;
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

		/// <summary>
		/// ChannelAccessToken
		/// </summary>
		private string channelAccessToken;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		public GroupService( string channelAccessToken )
			=> this.channelAccessToken = channelAccessToken;

		// TODO 未確認
		/// <summary>
		/// グループメンバーのプロフィールを取得する
		/// </summary>
		/// <param name="groupId">グループID</param>
		/// <param name="userId">ユーザID</param>
		/// <returns></returns>
		public async Task<UserProfileInGroupMemberResponse> GetUserProfileInGroupMember( string groupId , string userId ) {

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + this.channelAccessToken + "}" );

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( "https://api.line.me/v2/bot/group/" + groupId + "/member/" + userId );
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

	}

}