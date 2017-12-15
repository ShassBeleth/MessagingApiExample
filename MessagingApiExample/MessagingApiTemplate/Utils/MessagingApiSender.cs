using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Utils {

	/// <summary>
	/// API送信用クラス
	/// </summary>
	internal static class MessagingApiSender {

		/// <summary>
		/// API送信
		/// </summary>
		/// <typeparam name="RequestT">リクエストの型</typeparam>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="url">URL</param>
		/// <param name="request">リクエスト</param>
		/// <param name="isGetRequest">GETリクエストかどうか</param>
		/// <param name="contentType">Content-Type</param>
		/// <returns>バイナリ</returns>
		internal static async Task<byte[]> SendMessagingApi<RequestT>(
			string channelAccessToken ,
			string url ,
			RequestT request = default(RequestT) ,
			bool isGetRequest = true ,
			string contentType = "application/json"
		)
			where RequestT : class
		{

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( url == null ) {
				Trace.TraceWarning( "Url is Null" );
				return null;
			}

			Trace.TraceInformation( "Channel Access Token is " + channelAccessToken );
			Trace.TraceInformation( "Url is " + url );
			Trace.TraceInformation( "Request is " + ( request == null ? "Null" : "Not Null" ) );
			Trace.TraceInformation( "Get Request is " + isGetRequest );
			Trace.TraceInformation( "Content Type is " + contentType );

			// リクエストがあればcontentを作成
			StringContent content = null;
			if( request != null ) {

				string jsonRequest = JsonConvert.SerializeObject( request );
				Trace.TraceInformation( "Json Request is " + jsonRequest );

				content = new StringContent( jsonRequest );
				content.Headers.ContentType = new MediaTypeHeaderValue( contentType );

			}

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( contentType ) );
			if( channelAccessToken != null )
				client.DefaultRequestHeaders.Add( "Authorization" , "Bearer {" + channelAccessToken + "}" );
			
			try {

				HttpResponseMessage response = (
					isGetRequest ?
					await client.GetAsync( url ).ConfigureAwait( false ) :
					await client.PostAsync( url , content ).ConfigureAwait( false )
				);
				byte[] resultAsBinary = await response.Content.ReadAsByteArrayAsync();
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Send Messaging Api is OK" );
				Trace.TraceInformation( "End" );
				return resultAsBinary;

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Send Messaging Api is Argument Null Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Send Messaging Api is Http Request Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Send Messaging Api is Unexpected Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}
			
		}

		/// <summary>
		/// API送信
		/// </summary>
		/// <typeparam name="RequestT">リクエストの型</typeparam>
		/// <typeparam name="ResponseT">レスポンスの型</typeparam>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="url">URL</param>
		/// <param name="request">リクエスト</param>
		/// <param name="isGetRequest">GETリクエストかどうか</param>
		/// <param name="contentType">Content-Type</param>
		/// <returns>レスポンス</returns>
		internal static async Task<ResponseT> SendMessagingApi<RequestT, ResponseT>(
			string channelAccessToken ,
			string url ,
			RequestT request = default( RequestT ) ,
			bool isGetRequest = true ,
			string contentType = "application/json"
		)
			where RequestT : class
			where ResponseT : class 
		{

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return default( ResponseT );
			}
			if( url == null ) {
				Trace.TraceWarning( "Url is Null" );
				return default( ResponseT );
			}

			Trace.TraceInformation( "Request is " + ( request == null ? "Null" : "Not Null" ) );
			Trace.TraceInformation( "Get Request is " + isGetRequest );
			Trace.TraceInformation( "Content Type is " + contentType );
			Trace.TraceInformation( "Url is " + url );

			// リクエストがあればcontentを作成
			StringContent content = null;
			if( request != null ) {

				string jsonRequest = JsonConvert.SerializeObject( request );
				Trace.TraceInformation( "Json Request is " + jsonRequest );

				content = new StringContent( jsonRequest );
				content.Headers.ContentType = new MediaTypeHeaderValue( contentType );

			}

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( contentType ) );
			client.DefaultRequestHeaders.Add( "Authorization" , "Bearer " + channelAccessToken );

			try {

				HttpResponseMessage response =
					isGetRequest ?
					await client.GetAsync( url ).ConfigureAwait( false ) :
					await client.PostAsync( url , content ).ConfigureAwait( false );

				string resultAsString = await response?.Content.ReadAsStringAsync();
				Trace.TraceInformation( "Response is " + resultAsString );
				response.Dispose();
				client.Dispose();
				Trace.TraceInformation( "Send Messaging Api is OK" );

				bool isStringResponseType = ( typeof( ResponseT ) == typeof( string ) );
				Trace.TraceInformation( "String Response Type is " + isStringResponseType );
				Trace.TraceInformation( "End" );
				return isStringResponseType ? null : JsonConvert.DeserializeObject<ResponseT>( resultAsString );

			}
			catch( ArgumentNullException ) {
				Trace.TraceError( "Send Messaging Api is Argument Null Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}
			catch( HttpRequestException ) {
				Trace.TraceError( "Send Messaging Api is Http Request Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}
			catch( Exception ) {
				Trace.TraceError( "Send Messaging Api is Unexpected Exception" );
				client.Dispose();
				Trace.TraceInformation( "End" );
				return null;
			}

		}
		
	}

}