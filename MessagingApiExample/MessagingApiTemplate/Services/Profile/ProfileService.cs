using MessagingApiTemplate.Models.Responses.Profile;
using MessagingApiTemplate.Utils;
using System.Configuration;
using System.Threading.Tasks;

namespace MessagingApiTemplate.Services.Profile {

	/// <summary>
	/// プロフィールについてのService
	/// </summary>
	public static class ProfileService {
		
		/// <summary>
		/// プロフィール情報取得
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="userId">ユーザID</param>
		/// <returns>プロフィール情報</returns>
		public static async Task<GetProfileResponse> GetProfile( string channelAccessToken , string userId ) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
				return null;
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id is Null" );
				return null;
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "ProfileUrl" ] +
				userId;

			GetProfileResponse response = await MessagingApiSender.SendMessagingApi<string,GetProfileResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;
			
		}

	}

}