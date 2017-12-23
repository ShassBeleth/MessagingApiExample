using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using MessagingApiTemplate.Services.Webhook;
using MessagingApiTemplate.Models.Config.Webhook;
using MessagingApiTemplate.Utils;
using MessagingApiTemplate.Models.Responses.Profile;
using MessagingApiTemplate.Services;
using MessagingApiTemplate.Models.Requests.Webhook.Event.Source;
using MessagingApiTemplate.Services.Message;
using MessagingApiTemplate.Services.Profile;
using MessagingApiTemplate.Services.Group;
using MessagingApiTemplate.Models.Responses;
using MessagingApiTemplate.Services.Room;
using MessagingApiTemplate.Services.Message.Factory;

namespace MessagingApiExample.Controllers {

	/// <summary>
	/// Messaging APIよりコールされるWebhook API
	/// </summary>
	public class WebhookController : ApiController {
		
		/// <summary>
		/// POSTメソッド
		/// </summary>
		/// <param name="requestToken">リクエストトークン</param>
		/// <returns>常にステータス200のみを返す</returns>
		public async Task<HttpResponseMessage> Post( JToken requestToken ) {
			
			Trace.TraceInformation( "Webhook API Start" );

			// Webhook Serviceの実行
			await WebhookService.Execute(

				// Webhook Serviceの設定
				new WebhookServiceConfig() {

					RequestJToken = requestToken ,
					RequestHeaders = this.Request.Headers ,
					RequestContent = this.Request.Content ,

					// 署名の検証は行わない
					IsExecuteVerifySign = false ,

					// ロングタームチャンネルアクセストークンを使用する
					IsUseLongTermChannelAccessToken = true ,

					// フォローイベント
					FollowEventHandler = async ( channelAccessToken , replyToken ) => await this.ExecuteFollowEvent( channelAccessToken , replyToken ) ,

					// 参加イベント
					JoinEventHandler = async ( channelAccessToken , replyToken ) => await this.ExecuteJoinEvent( channelAccessToken , replyToken ) ,

					// テキストイベント
					TextMessageEventHandler = async ( channelAccessToken , source , replyToken , text ) => await this.ExecuteTextMessageEvent( channelAccessToken , source , replyToken , text )

				}

			).ConfigureAwait( false );

			Trace.TraceInformation( "Webhook API End" );
			
			return new HttpResponseMessage( HttpStatusCode.OK );

		}

		/// <summary>
		/// 友達追加、ブロック解除イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteFollowEvent( 
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Start" );
			await Task.CompletedTask;
			Trace.TraceInformation( "End" );
		}

		/// <summary>
		/// グループ追加イベント実行
		/// </summary>
		/// <param name="channelAccessToken">チャンネルアクセストークン</param>
		/// <param name="replyToken">リプライトークン</param>
		private async Task ExecuteJoinEvent(
			string channelAccessToken ,
			string replyToken
		) {
			Trace.TraceInformation( "Start" );
			await Task.CompletedTask;
			Trace.TraceInformation( "End" );
		}

		/// <summary>
		/// テキストメッセージイベント実行
		/// </summary>
		/// <param name="text">テキスト本文</param>
		private async Task ExecuteTextMessageEvent(
			string channelAccessToken ,
			SourceBase source ,
			string replyToken ,
			string text	
		) {
			Trace.TraceInformation( "Start" );

			// 引数nullチェック
			if( channelAccessToken == null ) {
				Trace.TraceInformation( "Channel Access Token is Null" );
				return;
			}
			if( source == null ) {
				Trace.TraceWarning( "Source is Null" );
				return;
			}
			if( text == null ) {
				Trace.TraceWarning( "Text is Null" );
				return;
			}

			switch( text ) {

				case "プロフィールほしい":

					if( source is GroupSource ) {
						
						string groupId = ( source as GroupSource ).groupId;
						if( groupId == null ) {
							Trace.TraceInformation( "Group Id is Null" );
							return;
						}
						string userId = ( source as GroupSource ).userId;
						if( userId == null ) {
							Trace.TraceInformation( "User Id is Null" );
							return;
						}
						
						GetUserProfileInGroupOrRoomMemberResponse profilesResponse = 
							await GroupService.GetUserProfileInGroupMember( channelAccessToken , groupId , userId )
							.ConfigureAwait( false );
						string messageText = 
							"表示名:" + profilesResponse.displayName +
							"ID:" + profilesResponse.userId +
							"画像:" + profilesResponse.pictureUrl +
							"\n";

						await ReplyMessageService.SendReplyMessage(
							channelAccessToken ,
							replyToken ,
							MessageFactoryService.CreateMessage()
								.AddTextMessage( messageText )
						).ConfigureAwait( false );
						
					}

					else if( source is RoomSource ) {

						string roomId = ( source as RoomSource ).roomId;
						if( roomId == null ) {
							Trace.TraceInformation( "Room Id is Null" );
							return;
						}
						string userId = ( source as RoomSource ).userId;
						if( userId == null ) {
							Trace.TraceInformation( "User Id is Null" );
							return;
						}

						GetUserProfileInGroupOrRoomMemberResponse profilesResponse =
							await RoomService.GetUserProfileInRoomMember( channelAccessToken , roomId , userId )
							.ConfigureAwait( false );
						string messageText =
							"表示名:" + profilesResponse.displayName +
							"ID:" + profilesResponse.userId +
							"画像:" + profilesResponse.pictureUrl +
							"\n";

						await ReplyMessageService.SendReplyMessage(
							channelAccessToken ,
							replyToken ,
							MessageFactoryService.CreateMessage()
								.AddTextMessage( messageText )
						).ConfigureAwait( false );


					}

					else if( source is UserSource ) {

						string userId = ( source as UserSource ).userId;
						if( userId == null ) {
							Trace.TraceInformation( "User Id is Null" );
							return;
						}

						await ReplyMessageService.SendReplyMessage(
							channelAccessToken ,
							replyToken ,
							MessageFactoryService.CreateMessage()
								.AddTextMessage( "リプライ" )
						).ConfigureAwait( false );

						GetProfileResponse profile = await ProfileService.GetProfile( channelAccessToken , userId ).ConfigureAwait( false );
						Trace.TraceInformation( "User Id is " + profile?.userId );
						Trace.TraceInformation( "Display Name is " + profile?.displayName );
						Trace.TraceInformation( "Status Message is " + profile?.statusMessage );
						Trace.TraceInformation( "Picture Url is " + profile?.pictureUrl );
					}

					break;

				case "茜ちゃん帰って":

					if( source is GroupSource ) {

						string groupId = ( source as GroupSource ).groupId;
						if( groupId == null ) {
							Trace.TraceInformation( "Group Id is Null" );
							return;
						}

						await GroupService.LeaveGroup( channelAccessToken , groupId ).ConfigureAwait( false );

					}

					else if( source is RoomSource ) {

						string roomId = ( source as RoomSource ).roomId;
						if( roomId == null ) {
							Trace.TraceInformation( "Room Id is Null" );
							return;
						}

						await RoomService.LeaveRoom( channelAccessToken , roomId ).ConfigureAwait( false );

					}

					break;
					
					

				default:
					Trace.TraceInformation( "Unexpected Text" );
					break;

			}

			Trace.TraceInformation( "End" );
			return;

		}

	}

}