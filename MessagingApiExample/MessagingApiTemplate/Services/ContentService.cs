using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// ユーザから送られた画像や動画についてのService
	/// </summary>
	public class ContentService {

		/// <summary>
		/// ユーザから送られた画像や動画取得
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="messageId">メッセージID</param>
		/// <returns>取得した画像や動画のバイナリ</returns>
		public async Task<byte[]> GetContent( string channelAccessToken , string messageId ) {

			Trace.TraceInformation( "Start Get Content" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token Of Get Content is Null" );
				return null;
			}
			if( messageId == null ) {
				Trace.TraceWarning( "Message Id Of Get Content is Null" );
				return null;
			}

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GetContentUrlBefore" ] +
				messageId +
				ConfigurationManager.AppSettings[ "GetContentUrlAfter" ];

			try {

				HttpResponseMessage response = await client.GetAsync( requestUrl );
				byte[] resultAsBinary = await response.Content.ReadAsByteArrayAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Get Content Response is OK" );
				return resultAsBinary;

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Get Content is Argument Null Exception" );
				client.Dispose();
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Get Content is Http Request Exception" );
				client.Dispose();
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Get Content is Unexpected Exception" );
				client.Dispose();
				return null;
			}

		}

	}
	
}