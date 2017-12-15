using MessagingApiTemplate.Models.Responses.Profile;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// プロフィールについてのService
	/// </summary>
	public class ProfileService {
		
		/// <summary>
		/// プロフィール情報取得
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="userId">ユーザID</param>
		/// <returns>プロフィール情報</returns>
		public async Task<GetProfileResponse> GetProfile( string channelAccessToken , string userId ) {

			System.Diagnostics.Trace.TraceInformation( "Start Get Profile" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				System.Diagnostics.Trace.TraceWarning( "Channel Access Token Of Get Profile is Null" );
				return null;
			}
			if( userId == null ) {
				System.Diagnostics.Trace.TraceWarning( "User Id Of Get Profile is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "ProfileUrl" ] +
				userId;
			return await MessagingApiSender.SendMessagingApi<string,GetProfileResponse>(
				channelAccessToken ,
				requestUrl
			);
			
		}

	}

}