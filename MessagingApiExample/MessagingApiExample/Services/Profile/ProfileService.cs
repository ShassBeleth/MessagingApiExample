using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagingApiExample.Models.Response.Profile;
using Newtonsoft.Json;

namespace MessagingApiExample.Services.Profile {

	/// <summary>
	/// プロフィール用Service
	/// </summary>
	public class ProfileService {

		/// <summary>
		/// ChannelAccessToken
		/// </summary>
		private string channelAccessToken;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		public ProfileService( string channelAccessToken ) {
			Trace.TraceInformation( "constructor" );
			this.channelAccessToken = channelAccessToken;
		}
		
		/// <summary>
		/// プロフィール情報取得
		/// </summary>
		/// <param name="userId">ユーザID</param>
		/// <returns>プロフィール情報</returns>
		public async Task<ProfileResponse> GetProfile( string userId ) {

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + this.channelAccessToken + "}" );

			string resultAsString = null;

			try {

				HttpResponseMessage response = await client.GetAsync( "https://api.line.me/v2/bot/profile/" + userId );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get Profile Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<ProfileResponse>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get Profile is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get Profile is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get Profile is Unexpected Exception" );
				client.Dispose();
				return null;
			}

		}

	}

}