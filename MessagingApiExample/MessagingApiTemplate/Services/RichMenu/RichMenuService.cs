using System.Configuration;
using System.Threading.Tasks;
using MessagingApiTemplate.Models.Requests.RichMenu;
using MessagingApiTemplate.Models.Responses.RichMenu;
using MessagingApiTemplate.Utils;

namespace MessagingApiTemplate.Services.RichMenu {

	public class RichMenuService {
		
		/// <summary>
		/// リッチメニューの取得
		/// </summary>
		/// <param name="channelAccessToken">ChannelAccessToken</param>
		/// <param name="richMenuId">リッチメニューID</param>
		public static async Task<GetRichMenuResponse> GetRichMenu(
			string channelAccessToken ,
			string richMenuId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( richMenuId == null ) {
				Trace.TraceWarning( "Rich Menu Id is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RichMenuUrl" ] +
				richMenuId;
			GetRichMenuResponse response = await MessagingApiSender.SendMessagingApi<string , GetRichMenuResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;

		}

		public static async Task CreateRichMenu(
			string channelAccessToken ,	
			CreateRichMenuRequest request			
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( request == null ) {
				Trace.TraceWarning( "Request is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RichMenuUrl" ];
			await MessagingApiSender.SendMessagingApi<CreateRichMenuRequest , string>(
				channelAccessToken ,
				requestUrl ,
				request ,
				"post"
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

		}

		public static async Task DeleteRichMenu(
			string channelAccessToken ,
			string richMenuId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( richMenuId == null ) {
				Trace.TraceWarning( "Rich Menu Id is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "RichMenuUrl" ] +
				richMenuId;
			await MessagingApiSender.SendMessagingApi<CreateRichMenuRequest , string>(
				channelAccessToken ,
				requestUrl ,
				null ,
				"delete"
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

		}

		public static async Task<GetUserRichMenuResponse> GetUserRichMenu(
			string channelAccessToken ,
			string userId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "UserUrl" ] +
				userId +
				ConfigurationManager.AppSettings[ "RichOnlyUrl" ];
			GetUserRichMenuResponse response = await MessagingApiSender.SendMessagingApi<string , GetUserRichMenuResponse>(
				channelAccessToken ,
				requestUrl
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );

			return response;

		}

		public static async Task LinkUserAndRichMenu(
			string channelAccessToken ,
			string userId ,
			string richMenuId
		) {

			Trace.TraceInformation( "Start" );

			// 引数のnullチェック
			if( channelAccessToken == null ) {
				Trace.TraceWarning( "Channel Access Token is Null" );
			}
			if( userId == null ) {
				Trace.TraceWarning( "User Id is Null" );
			}
			if( richMenuId == null ) {
				Trace.TraceWarning( "Rich Menu Id is Null" );
			}

			string requestUrl =
				ConfigurationManager.AppSettings[ "BaseUrl" ] +
				ConfigurationManager.AppSettings[ "UserUrl" ] +
				userId +
				ConfigurationManager.AppSettings[ "RichOnlyUrl" ] +
				"/" +
				richMenuId;
			await MessagingApiSender.SendMessagingApi<string , GetUserRichMenuResponse>(
				channelAccessToken ,
				requestUrl ,
				null ,
				"post"
			).ConfigureAwait( false );

			Trace.TraceInformation( "End" );
			
		}

	}
	
}