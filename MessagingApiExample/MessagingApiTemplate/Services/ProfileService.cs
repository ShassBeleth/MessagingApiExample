﻿using MessagingApiTemplate.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// プロフィールについてのService
	/// </summary>
	public class ProfileService {
		
		/// <summary>
		/// プロフィール情報取得
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="userId">ユーザID</param>
		/// <returns>プロフィール情報</returns>
		public async Task<GetProfileResponse> GetProfile( string channelAccessToken , string userId ) {

			Trace.TraceInformation( "Start Get Profile" );

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string resultAsString = null;
			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "ProfileUrl" ] +
				userId;

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				resultAsString = await response.Content.ReadAsStringAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get Profile Response is : " + resultAsString );
				return JsonConvert.DeserializeObject<GetProfileResponse>( resultAsString );

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