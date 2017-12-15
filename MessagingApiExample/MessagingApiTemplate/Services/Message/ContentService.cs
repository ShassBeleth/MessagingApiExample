using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Message {

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

			System.Diagnostics.Trace.TraceInformation( "Start Get Content" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				System.Diagnostics.Trace.TraceWarning( "Channel Access Token Of Get Content is Null" );
				return null;
			}
			if( messageId == null ) {
				System.Diagnostics.Trace.TraceWarning( "Message Id Of Get Content is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "GetContentUrlBefore" ] +
				messageId +
				ConfigurationManager.AppSettings[ "GetContentUrlAfter" ];

			return await MessagingApiSender.SendMessagingApi<string>( channelAccessToken , requestUrl );
			
		}

	}
	
}