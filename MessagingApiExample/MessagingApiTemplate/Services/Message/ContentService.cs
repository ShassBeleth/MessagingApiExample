using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Message {

	/// <summary>
	/// ユーザから送られた画像や動画についてのService
	/// </summary>
	public static class ContentService {

		/// <summary>
		/// ユーザから送られた画像や動画取得
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="messageId">メッセージID</param>
		/// <returns>取得した画像や動画のバイナリ</returns>
		public static async Task<byte[]> GetContent( string channelAccessToken , string messageId ) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( messageId == null ) {
				Trace.TraceWarning( "Message Id is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GetContentUrlBefore" ] +
				messageId +
				ConfigurationManager.AppSettings[ "GetContentUrlAfter" ];

			byte[] response = await MessagingApiSender.SendMessagingApi<string>( channelAccessToken , requestUrl );

			Trace.TraceInformation( "End" );

			return response;

		}

	}
	
}